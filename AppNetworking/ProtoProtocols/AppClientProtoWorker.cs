using Mpp.Protocol;
using MPPCSharp.Models;
using MPPCSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using AppNetworking.DTO;
using static AppNetworking.RpcProtocols.RpcCommunicationProtocols.ObjectRequestProtocols;
using System.IO;

namespace AppNetworking.ProtoProtocols
{
    public class AppClientProtoWorker : IAppObserver
    {
        private IAppServices server;
        private TcpClient connection;

        private NetworkStream stream;
        private volatile bool connected;

        public AppClientProtoWorker(IAppServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {

                stream = connection.GetStream();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void run()
        {
            while (connected)
            {
                try
                {
                    Console.WriteLine("RUN RUNR UN RUNR URNFFSDJKLFJDKLFLJALSKJADLKASD");
                    Mpp.Protocol.Request request = Mpp.Protocol.Request.Parser.ParseDelimitedFrom(stream);
                    Mpp.Protocol.Respone response = handleRequest(request);
                    if (response != null)
                    {
                        sendResponse(response);
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


        private void sendResponse(Mpp.Protocol.Respone response)
        {
            Console.WriteLine("sending response " + response);
            //formatter.Serialize(stream, response);
            response.WriteDelimitedTo(stream);
            stream.Flush();

        }

        private Respone handleRequest(Request request)
        {
            Respone response = null;
            Request.Types.Type reqType = request.Type;
            switch (reqType)
            {
                case Request.Types.Type.Login:
                    {
                        MPPCSharp.Models.Employee employee = ProtoUtils.getEmployeeFromRequest(request);
                        try
                        {
                            lock (server)
                            {
                                server.login(employee, this);
                            }
                            return ProtoUtils.createOkResponse();
                        }
                        catch (AppException e)
                        {
                            connected = false;
                            return ProtoUtils.createFailResponse(e.Message);
                        }
                    }
                case Request.Types.Type.Logout:
                    {
                        MPPCSharp.Models.Employee employee = ProtoUtils.getEmployeeFromRequest(request);
                        try
                        {
                            server.logout(employee);
                            connected = false;
                            return ProtoUtils.createOkResponse();

                        }
                        catch (AppException e)
                        {
                            return ProtoUtils.createFailResponse(e.Message);
                        }
                    }
                case Request.Types.Type.SendChild:
                    {
                        

                        try
                        {
                            server.sendChild(ProtoUtils.getChildFromChildTrialRequest(request),
                                   ProtoUtils.getTrialsFromChildTrialRequest(request));
                            return ProtoUtils.createOkResponse();
                        }
                        catch (AppException e)
                        {
                            return ProtoUtils.createFailResponse(e.Message);
                        }
                    }
                case Request.Types.Type.GetAllChildren:
                    {
                        try
                        {
                            List<MPPCSharp.Models.Child> allChildren = server.getAllChildren();
                            return ProtoUtils.sendAllChildrenResponse(allChildren);
                        }
                        catch (AppException e)
                        {
                            return ProtoUtils.createFailResponse(e.Message);
                        }
                    }
                case Request.Types.Type.GetAllTrials:
                    {
                        try
                        {
                            List<MPPCSharp.Models.Trial> allTrials = server.getAllTrials();
                            return ProtoUtils.sendAllTrialsResponse(allTrials);
                        }
                        catch (AppException e)
                        {
                            return ProtoUtils.createFailResponse(e.Message);
                        }
                    }
                case Request.Types.Type.GetChildTrials:
                    {
                        try
                        {
                            
                            MPPCSharp.Models.Child child = ProtoUtils.getChildFromRequest(request);
                            List<Guid> childTrials = server.getChildTrials(child);
                            return ProtoUtils.sendChildsTrialsResponse(childTrials);
                        }
                        catch (AppException e)
                        {
                            return ProtoUtils.createFailResponse(e.Message);
                        }
                    }
            }
            return response;
        }

        public void ChildReceived(MPPCSharp.Models.Child child, List<MPPCSharp.Models.Trial> trials)
            {
            Respone resp = ProtoUtils.createNewChildRespone(child, trials);
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
        }
}
