using System;
using System.Collections.Generic;
using System.Web;

namespace NewsPortal.Web.Models
{
    public class PostData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public DateTime DateAdded { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public IEnumerable<CommentaryData> Commentaries { get; set; }
    }
}