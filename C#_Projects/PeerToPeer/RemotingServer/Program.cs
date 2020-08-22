using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RemotingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to the Remoting Server !");
            
            ServiceHost host;
            
            //tcp binding
            NetTcpBinding tcp = new NetTcpBinding();

            //Bind server to the implementation
            host = new ServiceHost(typeof(RemotingImplemetation));

            host.AddServiceEndpoint(typeof(RemotingServerInterface), tcp,"net.tcp://0.0.0.0:8100/DataService");

            //Open host
            host.Open();

            Console.WriteLine("The server is up and running...");

            //keeping the server running
            Console.ReadLine();


        }
    }
}
