using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Date Created")]
        public DateTime DateCreated{ get; set; }
        [DisplayName("Category ID")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]  
        [ValidateNever]
        public Category Category{ get; set;}
        [ValidateNever]
        public List<ProjectImage> ProjectImages { get; set; }
        public string? YoutubeLink{ get; set; }
        public string? WebsiteLink{ get; set; }
    }
}
