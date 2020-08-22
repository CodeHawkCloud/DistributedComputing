using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RemotingServer
{
    public class ClientRemoteServers
    {

        public void ClientServerHosting(String ipAddress, uint port)
        {

            Console.WriteLine("Welcome to the Clients Remoting Server !");

            ServiceHost host;

            //tcp binding
            NetTcpBinding tcp = new NetTcpBinding();

            //Bind server to the implementation
            host = new ServiceHost(typeof(RemotingImplemetation));

            host.AddServiceEndpoint(typeof(RemotingServerInterface), tcp, "net.tcp://" + ipAddress + ":" + port.ToString() +  "/ClientHosting");

            //Open host
            host.Open();

            Console.WriteLine("Your server is up and running...");

            //keeping the server running
            Console.ReadLine();


        }
        
    }
}
