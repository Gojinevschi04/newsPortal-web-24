using System;

namespace NewsPortal.Web.Models
{
    public class CommentaryData
    {
        public int Id { get; set; }

        public string Content { get; set; }

        // public string ImagePath { get; set; }
        public DateTime DateAdded { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public int PostId { get; set; }
    }
}