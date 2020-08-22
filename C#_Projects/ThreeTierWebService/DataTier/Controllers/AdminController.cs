using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankDB;
using DataTier.Models;

namespace DataTier.Controllers
{

    public class AdminController : ApiController
    {

        private UserAccessInterface userAcess = BankDBClass.getUserAccessInterface();

        [Route("api/Admin/AllUsers")]
        [HttpGet]
        public List<uint> getAllUsers()
        {

            List<uint> l1 = userAcess.GetUsers();

            return l1;

        }

        [Route("api/Admin/Save")]
        [HttpGet]
        public void Save()
        {

            BankDBClass.saveToDisk();

        }

        [Route("api/Admin/ProcessAllTransactions")]
        [HttpGet]
        public Boolean ProcessAllTransactions()
        {

            Boolean success = BankDBClass.processAllTransactions();

            return success;

        }
    }
}