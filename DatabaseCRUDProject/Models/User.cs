using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseCRUDProject.Models
{
    public class User
    {
        public int Ci { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User(int ci, string first_name, string last_name, byte age, string email, string phone_number, string password, string role)
        {
            Ci = ci;
            FirstName = first_name;
            LastName = last_name;
            Age = age;
            Email = email;
            PhoneNumber = phone_number;
            Password = password;
            Role = role;
        }
    }
}
