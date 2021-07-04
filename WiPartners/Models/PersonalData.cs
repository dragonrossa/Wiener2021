using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WiPartners.Models
{
    public enum Genders
    {
        M,
        F,
        N
    }
    public class PersonalData
    {
        [Column("Id")]
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [Column("FirstName")]
        [Display(Name = "First name")]
        [StringLength(255, ErrorMessage = "First name must be betweeen 2 and 255 letters.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [Column("LastName")]
        [Display(Name = "Last name")]
        [StringLength(255, ErrorMessage = "Last name must be betweeen 2 and 255 letters.", MinimumLength = 2)]
        public string LastName { get; set; }
        [Column("Adress")]
        [Display(Name = "Adress")]
        public string Adress { get; set; }
        [Column("CroatianPIN")]
        [RegularExpression(@"^.{11}$", ErrorMessage = "PIN/OIB must have exactly 11 numbers")]
        [Display(Name = "Croatian PIN")]
        public long CroatianPIN { get; set; }
        [Required(ErrorMessage = "Time is required")]
        [Column("CreatedAtUtc")]
        [Display(Name = "Created at UTC")]
        private DateTime _createdAtUtc;
        public DateTime CreatedAtUtc
        {
            get
            {
                return _createdAtUtc;
            }
            set { _createdAtUtc = DateTime.UtcNow; }
        }
        [Column("CreateByUser")]
        [StringLength(255, ErrorMessage = "Email can have max 255 letters.")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email Address")]
        [Display(Name = "E-mail")]
        public string CreateByUser { get; set; }
        [Required(ErrorMessage = "This value is required")]
        [Column("IsForeign")]
        [Display(Name = "Foreign")]
        public bool IsForeign { get; set; }
        [Column("ExternalCode")]
        [Display(Name = "External code")]
        [StringLength(20, ErrorMessage = "External code can have between 10 and 20 letters.", MinimumLength = 10)]
        public string ExternalCode { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [Column("Gender")]
        [Display(Name = "Gender")]
        public Genders Gender { get; set; }
    }
}