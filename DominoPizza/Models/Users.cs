using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Users
    {
        public int usersId { get; set; }
        private string userFirstName;
        public string userLastName { get; set; }
        private string userPatronymicName { get; set; }
        private Boolean userSex { get; set; }
        private DateTime userBirthDate;

        public int UserRoleId { get; set; }
        public UserRoles UserRoles { get; set; }


        public void NewUser(string Name, string PatronymicName, string LastName, DateTime BirthDate, Boolean uSex)
        {
            userFirstName = Name;
            userPatronymicName = PatronymicName;
            userLastName= LastName;
            userBirthDate = BirthDate;
            userSex = uSex;
            
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