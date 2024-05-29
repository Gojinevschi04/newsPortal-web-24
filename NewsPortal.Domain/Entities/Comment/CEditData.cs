using System;

namespace NewsPortal.Domain.Entities.Comment
{
    public class CEditData
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}