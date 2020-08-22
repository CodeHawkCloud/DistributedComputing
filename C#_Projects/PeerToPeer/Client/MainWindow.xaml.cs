using APIClasses;
using Newtonsoft.Json;
using RemotingServer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Client
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //network delegate
        public delegate void NetworkDelegate(String ipAddress);

        //server delegate
        public delegate void ServerDelegate();

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


        }



        private void btn_codeSubmit_Click(object sender, RoutedEventArgs e)
        {
            //get python script
            string pythonScript = txt_pythonCode.Text;

            //encode
            //if text is empty
            if (String.IsNullOrEmpty(pythonScript))
            {

                MessageBox.Show("Please fill in the text box with a python code !", "Soloution Provider");

            }

            byte[] scriptInBytes = System.Text.Encoding.UTF8.GetBytes(pythonScript);
            String encodedScript = Convert.ToBase64String(scriptInBytes);

            //hash the ecoded script
            SHA256 sha256 = SHA256.Create();
            byte[] hashedCode = sha256.ComputeHash(Encoding.UTF8.GetBytes(encodedScript));

            //send the script to the server thread
            //remote connection factory
            ChannelFactory<RemotingServerInterface> remoteConnectionFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            //url set
            string URL = "net.tcp://localhost:8100/DataService";

            //initialize channelFactory
            remoteConnectionFactory = new ChannelFactory<RemotingServerInterface>(tcp, URL);

            //channel creation
            RemotingServerInterface remoteConnnection;
            remoteConnnection = remoteConnectionFactory.CreateChannel();

            //send the hash and encoded script to the remoting server
            remoteConnnection.sendJob(hashedCode, encodedScript);

            //get ip address of the client machine
            string ipAddress = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(1).ToString();

            //instantiate network delegate
            NetworkDelegate netDel;
            netDel = Networking;

            netDel.BeginInvoke(ipAddress, null, null);

        }

        //connect to server
        private void Networking(String ipAdress)
        {

            //encode the ip address
            byte[] ipInBytes = System.Text.Encoding.UTF8.GetBytes(ipAdress);
            String encodedIPAddress = Convert.ToBase64String(ipInBytes);

            //connect to the web server
            string URL = "https://localhost:44355/";

            RestClient client = new RestClient();

            //get the other clients
            RestRequest request = new RestRequest("api/GetOtherClients/" + encodedIPAddress);
            IRestResponse response = client.Get(request);

            //JSON deserilizer
            List<ClientIntermed> l1 = JsonConvert.DeserializeObject<List<ClientIntermed>>(response.Content);

            //send list to the controller to establish connection with .net remoting Server
            RestClient client2 = new RestClient();
            RestRequest request2 = new RestRequest("api/DownloadAndPerformJobs");

            request2.AddJsonBody(l1);
            IRestResponse response2 = client.Post(request2);

        }
    }
}

