using APIClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class JobController : ApiController
    {

        //Add to list function
        [Route("api/AddToList/{ipAddress}/{port}")]
        [HttpGet]
        public Boolean AddToList(string ipAddress, uint port)
        {

            //create a new client
            Client c1 = new Client();

            //add client details
            c1.ipAddress = ipAddress;
            c1.port = port;

            //get the list and add the client to the list
            ClientList.l1.Add(c1);

            return true;
        }

        //get others from the list
        [Route("api/GetOtherClients/{ipAddress}")]
        [HttpGet]
        public List<ClientIntermed> GetOtherClients(string ipAddress)
        {

            //get the list of clients
            List<Client> tempList = ClientList.l1;
            List<ClientIntermed> returnList = new List<ClientIntermed>();

            //loop and return other clients only
            int i;
            for(i = 0; i < tempList.Count; i++)
            {

                if(Equals(ipAddress, tempList[i].ipAddress))
                {
                    continue;
                }

                ClientIntermed c1 = new ClientIntermed();
                c1.IPAddress = tempList[i].ipAddress;
                c1.port = tempList[i].port;

                //add to the list
                returnList.Add(c1);

            }

            //return the list of other clients
            return returnList;

        }

        [Route("api/DownloadAndPerformJobs")]
        [HttpPost]
        public void DownloadAndPerfomJobs(List<Client> l1)
        {

            int i = 0;
            for(i = 0; i < l1.Count; i++)
            {

                //Didnt know what would go here>>>

            }


        }
    }
}