using System.Data.Entity;
using Gastronique.Domain.Entities.Restaurant;
using Gastronique.Domain.Entities.User;

namespace Gastronique.BusinessLogic.DbModel
{
     public class PostContext : DbContext
     {
          public PostContext() : base("name=WebDataBase")
          {
          }

          public virtual DbSet<RDbTable> Posts { get; set; }
     }
}