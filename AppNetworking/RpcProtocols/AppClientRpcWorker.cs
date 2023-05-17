using AppNetworking.DTO;
using AppNetworking.RpcProtocols.RpcCommunicationProtocols;
using MPPCSharp.Models;
using MPPCSharp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static AppNetworking.RpcProtocols.RpcCommunicationProtocols.ObjectRequestProtocols;
using static AppNetworking.RpcProtocols.RpcCommunicationProtocols.RPCCommunicationProtocols;

namespace AppNetworking.RpcProtocols
{
    public class AppClientRpcWorker : IAppObserver
    {
        private IAppServices server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter formatter;
        private volatile bool connected;

        public AppClientRpcWorker(IAppServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void ChildReceived(Child child, List<Trial> trials)
        {
            ChildTrialsDTO childTrialsDTO = DTOUtils.getDTO(child, trials);
            Response resp = new NEW_CHILD(childTrialsDTO);
            Console.WriteLine("Child received  " + child);
            try
            {
                sendResponse(resp);
            }
            catch (IOException e)
            {
                throw new AppException("Sending error: " + e);
            }
        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    object request = formatter.Deserialize(stream);
                    object response = handleRequest((Request)request);
                    if (response != null)
                    {
                        sendResponse((Response)response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e);
            }
        }


        private Response handleRequest(Request request)
        {
            Response response = null;

            if(request is LOGIN)
            {
                LOGIN logRequest = (LOGIN)request;
                EmployeeDTO employeeDTO = logRequest.data;
                Employee employee = DTOUtils.getFromDTO(employeeDTO);
                try
                {
                    server.login(employee, this);
                    return new OK();
                }
                catch (AppException e)
                {
                    connected = false;
                    return new ERROR(e.Message);
                }
            }
            if(request is LOGOUT)
            {
                LOGOUT logRequest = (LOGOUT)request;
                EmployeeDTO employeeDTO = logRequest.data;
                Employee employee = DTOUtils.getFromDTO(employeeDTO);
                try
                {
                    server.logout(employee);
                    connected = false;
                    return new OK();

                }
                catch (AppException e)
                {
                    return new ERROR(e.Message);
                }
            }
            if(request is SEND_CHILD)
            {
                SEND_CHILD sendChildRequest = (SEND_CHILD)request;
                ChildTrialsDTO childTrialsDTO = (ChildTrialsDTO)sendChildRequest.data;

                try
                {
                    server.sendChild(DTOUtils.getChildFromChildTrialsDTO(childTrialsDTO),
                            DTOUtils.getTrialsFromChildTrialsDTO(childTrialsDTO));
                    return new OK();
                }
                catch (AppException e)
                {
                    return new ERROR(e.Message) ;
                }
            }
            if(request is GET_ALL_CHILDREN)
            {
                try
                {
                    List<Child> allChildren = server.getAllChildren();
                    List<ChildDTO> childrenDTO = new List<ChildDTO>();
                    allChildren.ForEach(x => childrenDTO.Add(DTOUtils.getDTO(x)));
                    return new SEND_ALL_CHILDREN(childrenDTO);
                }
                catch (AppException e)
                {
                    return new ERROR(e.Message);
                }
            }
            if(request is GET_ALL_TRIALS)
            {
                try
                {
                    List<Trial> allTrials = server.getAllTrials();
                    List<TrialDTO> trialDTOs = new List<TrialDTO>();
                    allTrials.ForEach(x => trialDTOs.Add(DTOUtils.getDTO(x)));
                    return new SEND_ALL_TRIALS(trialDTOs);
                }
                catch (AppException e)
                {
                    return new ERROR(e.Message);
                }
            }
            if(request is GET_CHILD_TRIALS)
            {
                try
                {
                    GET_CHILD_TRIALS childTrialsRequest = (GET_CHILD_TRIALS)request;
                    ChildDTO childDTO = (ChildDTO)childTrialsRequest.data;
                    Child child = DTOUtils.getFromDTO(childDTO);
                    List<Guid> childTrials = server.getChildTrials(child);
                    return new SEND_CHILD_TRIALS(childTrials);
                }
                catch (AppException e)
                {
                    return new ERROR(e.Message);
                }
            }
            return response;
        }

            private void sendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            formatter.Serialize(stream, response);
            stream.Flush();

        }
    }

}

