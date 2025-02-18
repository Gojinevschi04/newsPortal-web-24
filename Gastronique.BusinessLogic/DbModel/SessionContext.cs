using System.Data.Entity;
using Gastronique.Domain.Entities.User;

namespace Gastronique.BusinessLogic.DbModel
{
     public class SessionContext : DbContext
     {
          public SessionContext() : base("name=WebDataBase")
          {
          }

          public virtual DbSet<Session> Sessions { get; set; }
     }
}