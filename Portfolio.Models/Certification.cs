using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    public class Certification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [DisplayName("Certification Image")]
        [ValidateNever]
        public string CertificationImage { get; set; }
    }
}
