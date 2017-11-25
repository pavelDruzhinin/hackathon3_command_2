using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Users
    {
        public int UsersId { get; set; }
        private string userName;
        public string UserName { get; set; }
        private string userPatronymicName;
        private string userSurName;
        private DateTime userBirthDate;
        private DateTime userCreateDate;
        public int UserRoleId {get; set;}

        public void NewUser(string Name, string PatronymicName, string SurName, DateTime BirthDate)
        {
            userName = Name;
            userPatronymicName = PatronymicName;
            userSurName = SurName;
            userBirthDate = BirthDate;
            userCreateDate = DateTime.Now;
        }



        public DateTime UserCreateDate
        {
            get
            {
                return UserCreateDate;
            }
        }
        public DateTime UserBirthDate
        {
            get
            {
                return UserBirthDate;
            }
        }
        public string UserFullName()
        {
            return $"{userName} {userPatronymicName} {userSurName}";
        }
    }
}