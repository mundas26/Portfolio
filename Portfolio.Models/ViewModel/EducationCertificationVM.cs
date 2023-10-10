using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Portfolio.Models.ViewModel
{
    public class EducationCertificationVM
    {
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Certification> Certifications { get; set; }
    }
}
