using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using Portfolio.Models.ViewModel;
using Portfolio.Utility;
using System.Drawing.Drawing2D;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SkillController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Skill> objSkillList = _unitOfWork.skill.GetAll().ToList();
            return View(objSkillList);
        }
        public IActionResult Upsert(int? id) 
        {
            SkillVM skillVM = new()
            {   
                Skill = new Skill()
            };
            if (id == null || id == 0)
            {
                return View(skillVM);
            }
            else
            {
                skillVM.Skill = _unitOfWork.skill.Get(u => u.Id == id);
                return View(skillVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(SkillVM skillVM, IFormFile? file)
        {
            if (file == null)
            {
                ModelState.AddModelError("Skill.StackImage", "Please select an image");
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string skillPath = Path.Combine(wwwRootPath, @"images\skill");

                    if (!string.IsNullOrEmpty(skillVM.Skill.StackImage))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, skillVM.Skill.StackImage.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(skillPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    skillVM.Skill.StackImage = @"\images\skill\" + fileName;
                }
                if (skillVM.Skill.Id == 0)
                {
                    _unitOfWork.skill.Add(skillVM.Skill);
                }
                else
                {
                    _unitOfWork.skill.Update(skillVM.Skill);
                }
                _unitOfWork.Save();
                TempData["success"] = "Skill Created successfully";
                return RedirectToAction("Index");
            }
            return View(skillVM);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Skill> objSkillList = _unitOfWork.skill.GetAll().ToList();
            return Json(new {data = objSkillList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var skillToBeDeleted = _unitOfWork.skill.Get(u => u.Id == id);
            if (skillToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleting..."});
            }
            _unitOfWork.skill.Remove(skillToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Skill deleted successfully" });
        }
        #endregion
    }
}
