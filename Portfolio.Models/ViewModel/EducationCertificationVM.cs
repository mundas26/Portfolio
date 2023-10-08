using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Models.ViewModel
{
    public class EducationCertificationVM
    {
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Certification> Certifications { get; set; }
    }
}
