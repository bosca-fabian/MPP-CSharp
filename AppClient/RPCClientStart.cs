using MPPCSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppClient;
using AppNetworking;
using AppNetworking.RpcProtocols;
using MPPCSharp.Forms;
using System.Windows.Forms;

namespace AppClient
{
    public class RPCClientStart
    {
        private static int defPort = 55556;
        private static String defaultServer = "localhost";


        [STAThread]
        static void Main()
        {
            IAppServices server = new AppServicesRpcProxy(defaultServer, defPort);
            MainForm mainform = new MainForm();
            Application.Run(new LogInForm(server, mainform));
        }
    }
}
