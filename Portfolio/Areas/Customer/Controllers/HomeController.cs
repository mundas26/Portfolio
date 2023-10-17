using Humanizer;
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
            var programmingStartDate = new DateTime(2019, 8, 8); // Your programming start date
            Experience model = new Experience
            {
                ProgrammingStartDate = programmingStartDate
            };

            // Calculate the initial experience
            model.CalculateTotalExperience();

            return View(model);
        }
        public IActionResult UpdateExperience()
        {
            var programmingStartDate = new DateTime(2019, 8, 8); // Your programming start date
            Experience model = new Experience
            {
                ProgrammingStartDate = programmingStartDate
            };

            // Calculate the initial experience
            model.CalculateTotalExperience();

            return View(model);
        }
        public IActionResult Project()
        {
            IEnumerable<Project> projectList = _unitOfWork.Project.GetAll(includeProperties: "Category,ProjectImages");
            return View(projectList);
        }
        public IActionResult Skill()
        {
            IEnumerable<Skill> skillList = _unitOfWork.skill.GetAll();
            return View(skillList);
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
        public IActionResult Details(int? projectId)
        {
            if (projectId == null || projectId == 0)
            {
                return NotFound();
            }
            else
            {
                Project project = _unitOfWork.Project.Get(u => u.Id == projectId);
                return View(project);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
