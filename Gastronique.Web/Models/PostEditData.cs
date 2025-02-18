using System;
using System.Collections.Generic;
using System.Web;

namespace Gastronique.Web.Models
{
     public class PostEditData
     {
          public int Id { get; set; }
          public string Title { get; set; }
          public string Category { get; set; }
          public string Content { get; set; }
          public string ImagePath { get; set; }
          public string NewImagePath { get; set; }
          public HttpPostedFileBase ImageFile { get; set; }
          public DateTime DateAdded { get; set; }
          public string Author { get; set; }
          public int AuthorId { get; set; }
     }
}