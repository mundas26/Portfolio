using Microsoft.AspNetCore.Mvc;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using Portfolio.Models.ViewModel;
using System.Diagnostics;

namespace Portfolio.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Project()
        {
            IEnumerable<Project> projectList = _unitOfWork.Project.GetAll(includeProperties: "Category,ProjectImages");
            return View(projectList);
        }
        public IActionResult Education()
        {
            IEnumerable<Education> educationList = _unitOfWork.Education.GetAll();
            IEnumerable<Certification> certificationList = _unitOfWork.Certification.GetAll();
            var educAndCertList = new EducationCertificationVM
            {
                Educations = educationList,
                Certifications = certificationList
            };
            return View(educAndCertList);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
