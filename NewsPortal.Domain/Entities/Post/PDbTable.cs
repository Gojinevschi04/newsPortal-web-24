using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPortal.Domain.Entities.Post
{
    public class PDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Title")]
        // [StringLength(8, ErrorMessage = "Title cannot be shorter than 8 characters.")]
        public string Title { get; set; }

        [Display(Name = "Content")] public string Content { get; set; }
        [Display(Name = "Category")] public string Category { get; set; }
        
        [DataType(DataType.Date)] public DateTime DateAdded { get; set; }

        [Display(Name = "Author")] public string Author { get; set; }
    }
}