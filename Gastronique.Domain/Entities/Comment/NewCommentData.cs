using System;

namespace Gastronique.Domain.Entities.Comment
{
    public class NewCommentData
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public int PostId { get; set; }
    }
}