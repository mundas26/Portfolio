using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using Portfolio.Models.ViewModel;
using Portfolio.Utility;
using System.Data;

namespace Portfolio.Areas.Admin.Controllers
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
            List<Project> objProductList = _unitOfWork.Project.GetAll(includeProperties: "Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            ProjectVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Project = new Project()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Project = _unitOfWork.Project.Get(u => u.Id == id, includeProperties: "ProjectImages");
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProjectVM projectVM, List<IFormFile> files)
        {
            if (projectVM.Project.CategoryId == null || projectVM.Project.CategoryId == 0)
            {
                ModelState.AddModelError("Project.CategoryId", "Please select a category.");
            }
            if (ModelState.IsValid)
            {
                if (projectVM.Project.Id == 0)
                {
                    _unitOfWork.Project.Add(projectVM.Project);
                }
                else
                {
                    _unitOfWork.Project.Update(projectVM.Project);
                }
                _unitOfWork.Save();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string projectPath = @"images\projects\project-" + projectVM.Project.Id;
                        string finalPath = Path.Combine(wwwRootPath, projectPath);

                        if (!Directory.Exists(finalPath))
                            Directory.CreateDirectory(finalPath);

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        ProjectImage projectImage = new()
                        {
                            ImageUrl = @"\" + projectPath + @"\" + fileName,
                            ProjectId = projectVM.Project.Id,
                        };

                        if (projectVM.Project.ProjectImages == null)
                            projectVM.Project.ProjectImages = new List<ProjectImage>();

                        projectVM.Project.ProjectImages.Add(projectImage);
                    }
                    _unitOfWork.Project.Update(projectVM.Project);
                    _unitOfWork.Save();
                }
                TempData["success"] = "Project created/updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                projectVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(projectVM);
            }
        }

        public IActionResult DeleteImage(int imageId)
        {
            var imageToBeDeleted = _unitOfWork.ProjectImages.Get(u => u.Id == imageId);
            int projectId = imageToBeDeleted.ProjectId;
            if (imageToBeDeleted != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    var oldImagePath =
                                   Path.Combine(_webHostEnvironment.WebRootPath,
                                   imageToBeDeleted.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _unitOfWork.ProjectImages.Remove(imageToBeDeleted);
                _unitOfWork.Save();

                TempData["success"] = "Deleted successfully";
            }
            return RedirectToAction(nameof(Upsert), new { id = projectId });
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Project> objProjectList = _unitOfWork.Project.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProjectList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var projectToBeDeleted = _unitOfWork.Project.Get(u => u.Id == id);
            if (projectToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string projectPath = @"images\projects\project-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, projectPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath);
            }
            _unitOfWork.Project.Remove(projectToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}