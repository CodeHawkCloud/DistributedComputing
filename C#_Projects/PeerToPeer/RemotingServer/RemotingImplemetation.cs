using APIClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RemotingServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = true)]
    class RemotingImplemetation : RemotingServerInterface
    {


        public bool sendJob(byte[] hash, string code)
        {

            //hash the code send
            SHA256 sha256 = SHA256.Create();
            byte[] remotingHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(code));

            //verify
            //turn remoting hash to string
            string remotingHashString  = Encoding.UTF8.GetString(remotingHash, 0, remotingHash.Length);

            //turn hash from WPF to string
            string wpfHashString = Encoding.UTF8.GetString(hash, 0, hash.Length);

            //if both the hashes are equal decode and add the jobs to the list
            if (Equals(remotingHash, wpfHashString))
            {
                //decode code from base 64
                byte[] encodedScriptInBytes = Convert.FromBase64String(code);
                string decodedScript = System.Text.Encoding.UTF8.GetString(encodedScriptInBytes);

                //add job to the class
                Job j1 = new Job();
                j1.script = decodedScript;

                //adding job to the list
                JobList.listOfJobs.Add(j1);

                return true;
            }
            else
            {
                return false;
            }

        }

        //download the job
        public JobIntermed downloadJob()
        {

            List<Job> l1 = JobList.listOfJobs;

            JobIntermed j1 = new JobIntermed();

            //check if jobs exist
            int i;
            for(i = 0; i < l1.Count; i++)
            {

                //if solution is available the job is alreaady completed so continue
                if(!(Equals(l1[i].response, "")))
                {
                    continue;
                }

                j1.pythonCode = l1[i].script;
                break;
            }

            return j1;
        }

        //set the response
        public bool setResponse(string script, string response)
        {
            List<Job> l1 = JobList.listOfJobs;

            //search for the script first
            int i;
            for(i = 0; i <l1.Count; i++)
            {
                //if the solution is found , and if another client has not served the solution yet set the solution
                if(Equals(script, l1[i].script) && !(Equals(l1[i].response, "")))
                {

                    l1[i].response = response;
                    break;

                }
            }

            return false;
        }
    }
}
