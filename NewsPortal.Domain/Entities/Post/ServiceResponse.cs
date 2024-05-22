using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Domain.Entities.Post
{
     public class ServiceResponse
     {
          public bool Status { get; set; }
          public string StatusMessage { get; set; }
          public int PostId { get; set; }
     }
}