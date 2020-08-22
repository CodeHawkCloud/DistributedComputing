using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIClasses;
using Newtonsoft.Json;
using System.Web.Configuration;

namespace BusinessTier.Controllers
{
    public class BusinessAPIController : ApiController
    {

        String URL = "https://localhost:44360/";
        RestClient client;

        [Route("api/Business/CreateUser")]
        [HttpPost]
        public uint CreateUser(UserData userData)
        {

            client = new RestClient(URL);

            //rewuest to user controller
            RestRequest request = new RestRequest("api/User/CreateUser");
            request.AddJsonBody(userData);

            IRestResponse response = client.Post(request);

            //JSON deserializer
            uint id = JsonConvert.DeserializeObject<uint>(response.Content);

            //request to admin controller
            RestRequest request2 = new RestRequest("api/Admin/Save");
            IRestResponse response2 = client.Get(request2);

            //return the user id
            return id;


        }

        [Route("api/Business/EditUser")]
        [HttpPut]
        public Boolean EditUser(UserDataWithId userDataWithId)
        {

            client = new RestClient(URL);

            //rewuest to user controller
            RestRequest request = new RestRequest("api/User/EditUser");
            request.AddJsonBody(userDataWithId);

            IRestResponse response = client.Put(request);

            //JSON deserializer
            Boolean success = JsonConvert.DeserializeObject<Boolean>(response.Content);

            //request to admin controller
            RestRequest request2 = new RestRequest("api/Admin/Save");
            IRestResponse response2 = client.Get(request2);

            //return the user id
            return success;

        }

        //Add account
        [Route("api/Business/AddAccount/{userId}")]
        [HttpGet]
        public uint AddAccount(uint userId)
        {

            client = new RestClient(URL);

            //request to account controller
            RestRequest request = new RestRequest("api/Account/AddAccount/" + userId.ToString());

            IRestResponse response = client.Get(request);

            //JSON deserializer
            uint accountNumber = JsonConvert.DeserializeObject<uint>(response.Content);

            //request to admin controller
            RestRequest request2 = new RestRequest("api/Admin/Save");
            IRestResponse response2 = client.Get(request2);

            //return the boolean true or false
            return accountNumber;

        }

        //deposit to account
        [Route("api/Business/DepositToAccount")]
        [HttpPost]
        public Boolean DepositToAccount(AccountDepositAndWithdraw accountDepositAndWithdraw)
        {

            client = new RestClient(URL);

            //request to account controller
            RestRequest request = new RestRequest("api/Account/DepositToAccount");
            request.AddJsonBody(accountDepositAndWithdraw);

            IRestResponse response = client.Post(request);

            //JSON deserializer
            Boolean success = JsonConvert.DeserializeObject<Boolean>(response.Content);

            //request to admin controller
            RestRequest request2 = new RestRequest("api/Admin/Save");
            IRestResponse response2 = client.Get(request2);

            //return success or failure
            return success;

        }

        //withdraw from account
        [Route("api/Business/WithdrawFromAccount")]
        [HttpPost]
        public Boolean WithdrawFromAccount(AccountDepositAndWithdraw accountDepositAndWithdraw)
        {

            client = new RestClient(URL);

            //request to account controller
            RestRequest request = new RestRequest("api/Account/WithdrawFromAccount");
            request.AddJsonBody(accountDepositAndWithdraw);

            IRestResponse response = client.Post(request);

            //JSON deserializer
            Boolean success = JsonConvert.DeserializeObject<Boolean>(response.Content);

            //request to admin controller
            RestRequest request2 = new RestRequest("api/Admin/Save");
            IRestResponse response2 = client.Get(request2);

            //return success or failure
            return success;

        }


        //get Accounts of a user by user id
        [Route("api/Business/GetAccByUserId/{userId}")]
        [HttpGet]
        public List<uint> GetAccountsByUserId(uint userId)
        {

            client = new RestClient(URL);

            //request to account controller
            RestRequest request = new RestRequest("api/Account/GetAccountsByUserId/" + userId.ToString());

            IRestResponse response = client.Get(request);

            //JSON deserializer
            List<uint> l1 = JsonConvert.DeserializeObject<List<uint>>(response.Content);


            //return the list
            return l1;

        }

        //create transaction
        [Route("api/Business/CreateTransactionAndProcess")]
        [HttpPost]
        public Boolean CreateTransactionAndProcess(CreateTransaction transaction)
        {
            client = new RestClient(URL);

            //check if the sender account has enough money
            //get the balance of the sender account
            RestRequest request = new RestRequest("api/Account/GetBalanceOfAccount/" + transaction.senderAccId);
            IRestResponse response = client.Get(request);

            //JSON deserializer
            uint senderAccountBalance = JsonConvert.DeserializeObject<uint>(response.Content);

            //check if the balance is greater than the ammount to be sent
            if (transaction.ammount > senderAccountBalance)
            {

                return false;

            }

            //create transaction
            //request to account controller
            RestRequest request2 = new RestRequest("api/Transaction/CreateTransaction");
            request2.AddJsonBody(transaction);

            IRestResponse response2 = client.Post(request2);

            //JSON deserializer
            uint transactionId = JsonConvert.DeserializeObject<uint>(response2.Content);

            RestRequest request3;

            //request to admin controller to process the transaction
            try
            {
                request3 = new RestRequest("api/Admin/ProcessAllTransactions");
                
            }catch(Exception)
            {
                return false;
            }

            IRestResponse response3 = client.Get(request3);
            //JSON deserializer
            Boolean processingFailed = JsonConvert.DeserializeObject<Boolean>(response3.Content);

            //if processing failed return false
            if (processingFailed == false)
            {

                return false;
            }

            //save to disk called after transactionn is completed
            RestRequest request4 = new RestRequest("api/Admin/Save");
            IRestResponse response4 = client.Get(request4);

            //return true after everything is successful
            return true;

        }
    }
}