using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIClasses;
using BankDB;
using DataTier.Models;

namespace DataTier.Controllers
{
    public class TransactionController : ApiController
    {
        private TransactionAccessInterface transferAccess = BankDBClass.getTransactionInterface();

        //create transaction
        [Route("api/Transaction/createTransaction")]
        [HttpPost]
        public uint CreateTransaction(CreateTransaction createTransaction)
        {

            //create transaction
            uint tempTransactionId = transferAccess.CreateTransaction();

            //save the trasnactionn id to the disk
            BankDBClass.saveToDisk();

            //select the account
            transferAccess.SelectTransaction(tempTransactionId);

            //set the sender, receiver and the ammount
            transferAccess.SetSendr(createTransaction.senderAccId);
            transferAccess.SetRecvr(createTransaction.receiverAccId);
            transferAccess.SetAmount(createTransaction.ammount);

            //return transaction id
            return tempTransactionId;
        }

    }
}