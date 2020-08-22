using BankDB;
using DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using APIClasses;
using System.Security.Principal;
using System.Web;
using System.Net.Http;

namespace DataTier.Controllers
{
    public class AccountController : ApiController
    {
        private UserAccessInterface userAcess = BankDBClass.getUserAccessInterface();
        private AccountAccessInterface accountAccess = BankDBClass.getAccountInterface();

        //Add account
        [Route("api/Account/AddAccount/{userId}")]
        [HttpGet]
        public uint AddAccount(uint userId)
        {

            List<uint> l1 = userAcess.GetUsers();
            uint tempAccountId = 0;

            int i;

            for (i = 0; i < l1.Count; i++)
            {

                if (l1[i] == userId)
                {

                    tempAccountId = accountAccess.CreateAccount(userId);

                }
            }

            return tempAccountId;

        }

        //deposit money to account
        [Route("api/Account/DepositToAccount")]
        [HttpPost]
        public Boolean Deposit(AccountDepositAndWithdraw depositAndWithdraw)
        {

            //get the account number
            uint tempAccNumber = depositAndWithdraw.accountId;

            //select the account
            accountAccess.SelectAccount(tempAccNumber);

            //deposit to the account
            try {

                accountAccess.Deposit(depositAndWithdraw.ammount);

            }catch(Exception){

                //if exception return false - everytime false here will mean the account id was wrong
                return false;

            }
   

            //return true if successful
            return true;

        }

        //withdraw from the account
        [Route("api/Account/WithdrawFromAccount")]
        [HttpPost]
        public Boolean Withdraw(AccountDepositAndWithdraw depositAndWithdraw)
        {

            //get the account number
            uint tempAccNumber = depositAndWithdraw.accountId;
            uint tempBalance;

            //select the account
            accountAccess.SelectAccount(tempAccNumber);

            try
            {
                //get the current balance of the account
                tempBalance = accountAccess.GetBalance();

            }
            catch (Exception)
            {

                //if exception return false
                return false;

            }

            //if withdraw ammount is greater than the balance return false
            if ( depositAndWithdraw.ammount > tempBalance)
            {

                return false;

            }


            //withdraw from the account
            accountAccess.Withdraw(depositAndWithdraw.ammount);

            //return true if successful
            return true;

        }

        //get accounts of a user
        [Route("api/Account/GetAccountsByUserId/{userId}")]
        [HttpGet]
        public List<uint> GetAccountsByUserId(uint userId)
        {

            List<uint> l1 = accountAccess.GetAccountIDsByUser(userId);


            //return the accounts
            return l1;

        }

        //get balance of the account
        [Route("api/Account/GetBalanceOfAccount/{accountId}")]
        [HttpGet]
        public uint GetBalanceOfAccount(uint accountId)
        {
            accountAccess.SelectAccount(accountId);

            uint bal;

            try
            {
                bal = accountAccess.GetBalance();

            }
            catch (Exception)
            {

                //the transaction would not be able to go ahead with a balance of 0
                return 0;

            }

            return bal;

        }

    }
}