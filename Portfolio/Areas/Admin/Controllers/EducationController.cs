using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using Portfolio.Models.ViewModel;
using Portfolio.Utility;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = SD.Role_Admin)]
    public class EducationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EducationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Education> educationsList = _unitOfWork.Education.GetAll().ToList();
            return View(educationsList);
        }
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                //Create    
                return View(new Education());
            }
            else
            {
                Education educationObj = _unitOfWork.Education.Get(u => u.Id == id);
                return View(educationObj);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Education educationObj)
        {
            if (ModelState.IsValid)
            {
                if (educationObj.Id == 0)
                {
                    _unitOfWork.Education.Add(educationObj);
                }
                else
                {
                    _unitOfWork.Education.Update(educationObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Created Education list successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(educationObj);
            }
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Education> objEducationList = _unitOfWork.Education.GetAll().ToList();
            return Json(new { data = objEducationList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var educationToBeDeleted = _unitOfWork.Education.Get(u => u.Id == id);
            if (educationToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting..." });
            }
            _unitOfWork.Education.Remove(educationToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Education deleted successfully!" });
        }
        #endregion 

    }
}
