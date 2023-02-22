using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        private User() { }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; private set; }
        public Gender Gender { get; set; }
        
      
        public virtual Blog Blog { get; set; }
        public static User CreateUser(string firstname, string lastname, Gender gen, string emailAddress, DateTime dob = default)
        {
            return new User
            {
                FirstName = firstname,
                LastName = lastname,
                Gender = gen,
                EmailAddress = emailAddress,
                DOB = dob,
            };
        }
    }
}
