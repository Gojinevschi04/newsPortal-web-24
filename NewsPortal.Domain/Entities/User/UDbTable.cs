using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NewsPortal.Domain.Enums;


namespace NewsPortal.Domain.Entities.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        // [StringLength(8,  ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        //[StringLength(8,  ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        // [StringLength(5)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        //[StringLength(5)]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        // [StringLength(5)]
        public string LastName { get; set; }

        [DataType(DataType.Date)] public DateTime LastLogin { get; set; }

        public string LastIp { get; set; }

        public URole Level { get; set; }
    }
}