using APIClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RemotingServer
{
    [ServiceContract]
    public interface RemotingServerInterface
    {
        [OperationContract]
        Boolean sendJob(byte[] hash, string code);

        [OperationContract]
        JobIntermed downloadJob();

        [OperationContract]
        Boolean setResponse(string script, string response);

    }
}
