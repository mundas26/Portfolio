using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using Portfolio.Models.ViewModel;
using Portfolio.Utility;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = SD.Role_Admin)]
    public class CertificationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public IWebHostEnvironment _webHostEnvironment;
        public CertificationController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Certification> certificationList = _unitOfWork.Certification.GetAll().ToList();
            return View(certificationList);
        }
        public IActionResult Upsert(int? id)
        {
            CertificationVM certificationVM = new()
            {
                Certification = new Certification()
            };
            if (id == null || id == 0)
            {
                return View(certificationVM);
            }
            else
            {
                certificationVM.Certification = _unitOfWork.Certification.Get(u => u.Id == id);
                return View(certificationVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(CertificationVM certificationVM, IFormFile? file)
        {
            if (file == null)
            {
                ModelState.AddModelError("Certification.CertificationImage", "Please select an image");
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string certificationPath = Path.Combine(wwwRootPath, @"images\certification");

                    if (!string.IsNullOrEmpty(certificationVM.Certification.CertificationImage))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, certificationVM.Certification.CertificationImage.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                   
                    using (var fileStream = new FileStream(Path.Combine(certificationPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    certificationVM.Certification.CertificationImage = @"\images\certification\" + fileName;
                }
                if (certificationVM.Certification.Id == 0)
                {
                    _unitOfWork.Certification.Add(certificationVM.Certification);
                }
                else
                {
                    _unitOfWork.Certification.Update(certificationVM.Certification);
                }
                _unitOfWork.Save();
                TempData["success"] = "Certification Created successfully";
                return RedirectToAction("Index");
            }
            return View(certificationVM);
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Certification> objCertificationList = _unitOfWork.Certification.GetAll().ToList();
            return Json(new { data = objCertificationList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var certificationToBeDeleted = _unitOfWork.Certification.Get(u => u.Id == id);
            if (certificationToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting..." });
            }
            _unitOfWork.Certification.Remove(certificationToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Certification deleted successfully!" });
        }
        #endregion 
    }
}
