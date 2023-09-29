using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Name of school")]
        public string SchoolName { get; set; }
        [Required]
        public string Course { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
