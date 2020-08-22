using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing.Constraints;

namespace DataTier.Models
{
    public class Transaction
    {

        public uint transactionId;
        public uint senderAccId;
        public uint receiverAccId;
        public float transactionAmt;


    }
}