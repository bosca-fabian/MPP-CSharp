using AppNetworking.DTO;
using AppNetworking.RpcProtocols.RpcCommunicationProtocols;
using MPPCSharp.Models;
using MPPCSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static  AppNetworking.RpcProtocols.RpcCommunicationProtocols.ObjectRequestProtocols;
using static AppNetworking.RpcProtocols.RpcCommunicationProtocols.RPCCommunicationProtocols;

namespace AppNetworking.RpcProtocols
{
    public class AppServicesRpcProxy : IAppServices
    {
        private string host;
        private int port;

        private IAppObserver client;

        private NetworkStream stream;

        private IFormatter formatter;
        private TcpClient connection;

        private Queue<Response> responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;
        public AppServicesRpcProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            responses = new Queue<Response>();
        }

        public List<Child> getAllChildren()
        {
            Request req = new GET_ALL_CHILDREN();
            sendRequest(req);
            Response response = readResponse();
            //        closeConnection();
            if (response is ERROR)
            {
                ERROR eRes  = (ERROR) response;  
                String err = eRes.data;
                throw new AppException(err);
            }
            SEND_ALL_CHILDREN res = (SEND_ALL_CHILDREN)response;
            List<Child> children = new List<Child>();
            List<ChildDTO> readChildren = res.data;
            foreach (ChildDTO childDTO in readChildren
                    )
            {
                children.Add(DTOUtils.getFromDTO(childDTO));
            }
            return children;
        }

        public List<Trial> getAllTrials()
        {
            Request req = new GET_ALL_TRIALS();
            sendRequest(req);
            Response response = readResponse();
            //        closeConnection();
            if (response is ERROR)
            {
                ERROR eRes = (ERROR)response;
                String err = eRes.data;
                throw new AppException(err);
            }
            SEND_ALL_TRIALS res = (SEND_ALL_TRIALS)response;
            List<Trial> trials = new List<Trial>();
            List<TrialDTO> readTrials = res.data;
            foreach (TrialDTO trialDTO in readTrials
                    )
            {
                trials.Add(DTOUtils.getFromDTO(trialDTO));
            }
            return trials;
        }

        public List<Guid> getChildTrials(Child child)
        {
            Request req = new GET_CHILD_TRIALS(DTOUtils.getDTO(child));
            sendRequest(req);
            Response response = readResponse();
            SEND_CHILD_TRIALS res = (SEND_CHILD_TRIALS)response;
            List<Guid> childTrials = (List<Guid>)res.data;
            return childTrials;
        }

        public void login(Employee employee, IAppObserver observer)
        {
            initializeConnection();
            EmployeeDTO employeeDTO = DTOUtils.getDTO(employee);
            Request req = new LOGIN(employeeDTO);
            sendRequest(req);
            Response response = readResponse();
            if (response is OK)
            {
                this.client = observer;
                return;
            }
            if (response is ERROR)
            {
                ERROR res = (ERROR)response;
                String err = res.data;
                closeConnection();
                throw new AppException(err);
            }
        }

        public void logout(Employee employee)
        {
            EmployeeDTO udto = DTOUtils.getDTO(employee);
            Request req = new LOGOUT(udto);
            sendRequest(req);
            Response response = readResponse();
            closeConnection();
            if (response is ERROR)
            {   
                ERROR res = (ERROR)response;
                String err = res.data;
                throw new AppException(err);
            }
        }

        public void sendChild(Child child, List<Trial> trials)
        {
            ChildTrialsDTO mdto = DTOUtils.getDTO(child, trials);
            Request req = new SEND_CHILD(mdto);
            sendRequest(req);
            Response response = readResponse();
            if (response is ERROR)
            {
                ERROR res = (ERROR)response;
                String err = res.data;
                throw new AppException(err);
            }
        }

        private void closeConnection()
        {
            finished = true;
            try
            {
                stream.Close();

                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        private void sendRequest(Request request)
        {
            try
            {
                formatter.Serialize(stream, request);
                stream.Flush();
            }
            catch (Exception e)
            {
                throw new AppException("Error sending object " + e);
            }

        }

        private Response readResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (responses)
                {
                   
                    response = responses.Dequeue();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return response;
        }
        private void initializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                stream = connection.GetStream();
                formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
        private void startReader()
        {
            Thread tw = new Thread(run);
            tw.Start();
        }

        private void handleUpdate(Response response)
        {
            if (response is NEW_CHILD)
            {
                NEW_CHILD res = (NEW_CHILD) response;
                Child child = DTOUtils.getChildFromChildTrialsDTO(res.data);
                List<Trial> trials = DTOUtils.getTrialsFromChildTrialsDTO(res.data);
                try
                {
                    client.ChildReceived(child, trials);
                }
                catch (AppException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public virtual void run()
        {
            while (!finished)
            {
                try
                {
                    object response = formatter.Deserialize(stream);
                    Console.WriteLine("response received " + response);
                    if (response is UPDATE_RESPONSE)
                    {
                        handleUpdate((UPDATE_RESPONSE)response);
                    }
                    else
                    {

                        lock (responses)
                        {
                            responses.Enqueue((Response)response);
                        }
                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }

            }
        }
    }
}
