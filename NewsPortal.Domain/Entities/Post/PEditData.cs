using System;

namespace NewsPortal.Domain.Entities.Post
{
    public class PEditData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }  
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public string Author { get; set; }
    }
}