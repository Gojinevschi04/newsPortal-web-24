using NewsPortal.Domain.Enums;

namespace NewsPortal.Web.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public URole Level { get; set; }
    }
}