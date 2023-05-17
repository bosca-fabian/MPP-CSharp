using AppNetworking.NetworkUtils;
using AppNetworking.RpcProtocols;
using MPPCSharp.Models;
using MPPCSharp.Services;
using MPPCSharp.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppNetworking.ProtoProtocols;
using AppPersistence.ORM;

namespace AppServer
{
    public class StartRpcServer
    {

       
        static void Main()
        {

            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", GetConnectionStringByName("mpp"));
            Repository<Trial> trialRepo = new TrialRepository(props);
            Repository<Child> childRepository = new ChildRepoORM(props);
            ChildTrialRepository childTrialRepository = new ChildTrialRepository(props);
            EmployeeRepoInterface employeeRepo = new EmployeeRepository(props);
            IAppServices service = new AppServicesImpl(employeeRepo, childRepository, childTrialRepository, trialRepo);

            //SerialAppServer server = new SerialAppServer("127.0.0.1", 55556, service);
            ProtoAppServer server = new ProtoAppServer("127.0.0.1", 55556, service);
            server.Start();

        }


        public static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }

    public class SerialAppServer : ConcurrentServer
    {
        private IAppServices server;
        private AppClientRpcWorker worker;
        public SerialAppServer(string host, int port, IAppServices server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine("SerialAppServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new AppClientRpcWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }

    public class ProtoAppServer : ConcurrentServer
    {
        private IAppServices server;
        private AppClientProtoWorker worker;
        public ProtoAppServer(string host, int port, IAppServices server)
            : base(host, port)
        {
            this.server = server;
            Console.WriteLine("ProtoChatServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new AppClientProtoWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }


}
