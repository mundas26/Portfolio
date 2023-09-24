using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using Portfolio.Utility;

namespace PortfolioAspDotNetMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                //Create
                return View(new Category());
            }
            else
            {
                Category categoryObj = _unitOfWork.Category.Get(u => u.Id == id);
                return View(categoryObj);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Category categoryObj)
        {
            if (ModelState.IsValid)
            {
                if (categoryObj.Id == 0)
                {
                    _unitOfWork.Category.Add(categoryObj);
                }
                else
                {
                    _unitOfWork.Category.Update(categoryObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Created category successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(categoryObj);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return Json(new { data = objCategoryList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var categoryToBeDeleted = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting..." });
            }
            _unitOfWork.Category.Remove(categoryToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Category deleted successfully!" });
        }
        #endregion
    }
}
