using System;
using System.Collections.Generic;
using System.Linq;
using DataTier.Models;
using BankDB;
using System.Web.Http;
using APIClasses;
using System.Diagnostics;

namespace DataTier.Controllers
{
    //create user function
    public class UserController : ApiController
    {

        private UserAccessInterface userAcess = BankDBClass.getUserAccessInterface();

        [Route("api/User/CreateUser")]
        [HttpPost]
        public uint CreateUser([FromBody] UserData user)
        {
            
            uint tempId;

            //user created and id gained
            tempId = userAcess.CreateUser();

            //save the id
            BankDBClass.saveToDisk();

            //select user to input first name and last name
            userAcess.SelectUser(tempId);

            //input first name and last name
            userAcess.SetUserName(user.firstName, user.lastName);

            //return the user id
            return tempId;

        }

        //edit user function
        [Route("api/User/EditUser")]
        [HttpPut]
        public Boolean EditUser([FromBody] UserDataWithId userDataWithId)
        {

            uint tempUserId = userDataWithId.userId;

            //check if the user with the id exists
            List<uint> l1 = userAcess.GetUsers();

            int i;
            for(i = 0; i < l1.Count; i++)
            {
                //if id is available
                if(l1[i] == tempUserId)
                {
                    //select user
                    userAcess.SelectUser(tempUserId);

                    string tempOldFname;
                    string tempOldLname;

                    //get the user's old fname and lname from the db
                    userAcess.GetUserName(out tempOldFname, out tempOldLname);

                    //check if oldFame is simillar to what the user gave
                    if (!tempOldFname.Equals(userDataWithId.oldFName)){


                        //retun false as the old first name doesnt match with the id
                        return false;

                    }

                    userAcess.SetUserName(userDataWithId.newFName, userDataWithId.lName);

                    //save the update
                    BankDBClass.saveToDisk();

                    //all successful
                    return true;
               
                }
                

            }

            //return false if there is no match
            return false;
            

        }


    }


}