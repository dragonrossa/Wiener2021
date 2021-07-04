using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WiPartners.Models
{
    public enum Types
    {
        Personal = 1,
        Legal = 2
    }

    public class PartnerPersonalData : PersonalData
    {
        [Required(ErrorMessage = "Partner number is required")]
        [Column("PartnerNumber")]
        [Display(Name = "Partner number")]
        [RegularExpression(@"^.{20}$", ErrorMessage = "Partner number must have exactly 20 numbers")]
        public decimal PartnerNumber { get; set; }
        [Required(ErrorMessage = "Partner type is required")]
        [Column("PartnerTypeId")]
        [Display(Name = "Partner type id")]
        public int PartnerTypeId { get; set; }
        public IEnumerable<PartnerPersonalData> partners { get; set; }

    }
}