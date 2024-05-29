using System.Collections.Generic;
using NewsPortal.Domain.Entities.Post;

namespace NewsPortal.Web.Models
{
     public class ListingPageData
     {
          public List<PostMinimal> ListingItems { get; set; }
          public SideBarData SideBar { get; set; }
     }
}