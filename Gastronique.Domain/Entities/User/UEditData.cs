using Gastronique.Domain.Enums;

namespace Gastronique.Domain.Entities.User
{
     public class UEditData
     {
          public int Id { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Username { get; set; }
          public string Email { get; set; }
          public URole Level { get; set; }
     }
}