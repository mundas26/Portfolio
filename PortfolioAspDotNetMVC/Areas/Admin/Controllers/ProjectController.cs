using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Models;
using Portfolio.Models.ViewModel;

namespace PortfolioAspDotNetMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProjectController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Project> objProjectList = _unitOfWork.Project.GetAll(includeProperties: "Category").ToList();
            return View(objProjectList);
        }
        public IActionResult Upsert(int? id)
        {
            ProjectVM projectVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u =>
                new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                Project = new Project()
            };

            if (id == null || id == 0)
            {
                //This is for Create..
                return View(projectVM);
            }
            else
            {
                //This is for Update..
                projectVM.Project = _unitOfWork.Project.Get(u => u.Id == id);
                return View(projectVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProjectVM projectVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string projectPath = Path.Combine(wwwRootPath, @"images\project");
                    if (!string.IsNullOrEmpty(projectVM.Project.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, projectVM.Project.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(projectPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    projectVM.Project.ImageUrl = @"\images\project\" + fileName;
                }
                if (projectVM.Project.Id == 0)
                {
                    _unitOfWork.Project.Add(projectVM.Project);
                }
                else
                {
                    _unitOfWork.Project.Update(projectVM.Project);
                }
                _unitOfWork.Save();
                TempData["success"] = "Project Created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                projectVM.CategoryList = _unitOfWork.Category.GetAll().Select(u =>
                new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
                return View(projectVM);
            }
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Project> objProductList = _unitOfWork.Project.GetAll(includeProperties: "Category").ToList();
            return Json(new { Data = objProductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productTobeDeleted = _unitOfWork.Project.Get(u => u.Id == id);
            if (productTobeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" }); 
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productTobeDeleted.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Project.Remove(productTobeDeleted);
            _unitOfWork.Save();
            return Json(new { success = false, message = "Product deleted successfully" });
        }
        #endregion
    }
}
