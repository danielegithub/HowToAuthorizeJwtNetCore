using JwtAuth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuth.Services
{
    public interface IUserServices
    {
        bool GetUser(string email, string password);
        User GetUser(string email);

    }
    public class UserServices : IUserServices
    {
        private List<User> Users { get; set; }

        public UserServices()
        {
            this.Users = new List<User>
            {
                new User{ Id = 1, Email = "test@test.com", Password = "1", Cognome = "Cognome 1", Nome = "Nome 1" },
                new User{ Id = 2, Email = "test2@test.com", Password = "2", Cognome = "Cognome 2", Nome = "Nome 2" },
                new User{ Id = 3, Email = "test3@test.com", Password = "3", Cognome = "Cognome 3", Nome = "Nome 3" }
            };
        }

        public bool GetUser(string email, string password)
        {
            return this.Users.Any(i => i.Email == email && i.Password == password);
        }

        public User GetUser(string email)
        {
            return this.Users.FirstOrDefault(i => i.Email == email);
        }
    }
}
