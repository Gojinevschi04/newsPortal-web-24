using System;
using System.Web;

namespace NewsPortal.Domain.Entities.Post
{
    public class NewPostData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public string ImagePath { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
    }
}