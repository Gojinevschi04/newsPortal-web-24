using System;
using System.Collections.Generic;
using NewsPortal.Domain.Entities.Comment;

namespace NewsPortal.Domain.Entities.Post
{
    public class PostMinimal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string ImagePath { get; set; }
        public int AuthorId { get; set; }
        public DateTime DateAdded { get; set; }
        public IEnumerable<CommentaryMinimal> Commentaries { get; set; }
    }
}