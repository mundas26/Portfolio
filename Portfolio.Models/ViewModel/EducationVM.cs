﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Models.ViewModel
{
    public class EducationVM
    {
        public IEnumerable<Education> EducationsList { get; set; }
        public Certification Certification { get; set; }
    }
}
