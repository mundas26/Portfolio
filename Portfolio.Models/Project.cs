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
        [DisplayFormat(DataFormatString = "{0:MMMM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Created")]
        public DateTime DateCreated{ get; set; }
        [DisplayName("Category ID")]
        [Required(ErrorMessage = "Please select a category.")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]  
        [ValidateNever]
        public Category Category{ get; set;}
        [ValidateNever]
        public List<ProjectImage> ProjectImages { get; set; }
        public string? YoutubeLink{ get; set; }
        public string? WebsiteLink{ get; set; }

        public Project()
        {
            var formatString = "{0:MMMM-dd-yyyy}";
            (this.GetType().GetProperty("DateCreated").GetCustomAttributes(typeof(DisplayFormatAttribute), true)[0] as DisplayFormatAttribute).DataFormatString = formatString;
        }
    }
}
