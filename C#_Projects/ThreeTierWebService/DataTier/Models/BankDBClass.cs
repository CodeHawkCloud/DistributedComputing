using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankDB;

namespace DataTier.Models
{
    public static class BankDBClass
    {

        //static bankDB object
        public static BankDB.BankDB bank = new BankDB.BankDB();

        //static method to get the accounts interface
        public static AccountAccessInterface getAccountInterface()
        {

            return bank.GetAccountInterface();

        }

        //static method to get the user interface
        public static UserAccessInterface getUserAccessInterface()
        {

            return bank.GetUserAccess();

        }

        //static method to get the transaction interface

        public static TransactionAccessInterface getTransactionInterface()
        {

            return bank.GetTransactionInterface();

        }

        //save function
        public static void saveToDisk()
        {

            bank.SaveToDisk();

        }

        //process all transactons functions
        public static Boolean processAllTransactions()
        {

            try
            {
                bank.ProcessAllTransactions();

            }catch(Exception)
            {
                return false;
            }


            return true;

        }

    }
}