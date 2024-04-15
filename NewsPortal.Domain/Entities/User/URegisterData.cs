using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Domain.Entities.User
{
    public class URegisterData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public DateTime LoginDataTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}