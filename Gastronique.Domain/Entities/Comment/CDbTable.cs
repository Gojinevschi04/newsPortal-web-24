using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gastronique.Domain.Entities.Comment
{
    public class CDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Content")] public string Content { get; set; }
        [Display(Name = "PostId")] public int PostId { get; set; }
        [DataType(DataType.Date)] public DateTime DateAdded { get; set; }
        [Display(Name = "Author")] public string Author { get; set; }
        [Display(Name = "AuthorId")] public int AuthorId { get; set; }
    }
}