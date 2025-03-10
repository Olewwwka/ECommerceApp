using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Models
{
    public class User
    {
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public User(string login, string passwordHash, string email, string firstname, string lastname)
        {
            Login = login;
            PasswordHash = passwordHash;
            Email = email;
            FirstName = firstname;
            LastName = lastname;
        }

        public static User Create(string login, string passwordHash, string email, string firstname, string lastname)
        {
            return new User(login, passwordHash, email, firstname, lastname);
        }
    }
}
