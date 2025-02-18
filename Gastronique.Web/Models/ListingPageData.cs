using System.Collections.Generic;
using Gastronique.Domain.Entities.Restaurant;

namespace Gastronique.Web.Models
{
     public class ListingPageData
     {
          public List<RestaurantMinimal> ListingItems { get; set; }
          public SideBarData SideBar { get; set; }
     }
}