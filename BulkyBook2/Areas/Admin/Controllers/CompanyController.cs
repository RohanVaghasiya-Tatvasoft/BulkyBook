using BulkyBook2.Data;
using BulkyBook2.DataAccess.Repository.IRepository;
using BulkyBook2.Models;
using BulkyBook2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;

namespace BulkyBook2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Get method for Edit view...
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            
            if (id == null || id == 0)
            {                
                return View(company);
            }
            else
            {
                company = _unitOfWork.CompanyRepository1.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }            
        }

        //Post method for Edit view...
        [HttpPost]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Id == 0)
                {
                    _unitOfWork.CompanyRepository1.Add(obj);
                    TempData["Success"] = "Hurray! Your Company Created successfully :)";
                }
                else
                {
                    _unitOfWork.CompanyRepository1.Update(obj);
                    TempData["Success"] = "Alright I updated it for free... next time give me money :)";
                }
                _unitOfWork.Save();                
                return RedirectToAction("Index", "Company");
            }
            return View(obj);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.CompanyRepository1.GetAll();
            return Json(new { data = companyList });
        }
        #endregion

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.CompanyRepository1.GetFirstOrDefault(u => u.Id == id);
            if(obj == null)
            {
                return Json(new { Success = false, message = "Error while deleting..." });
            }            
            _unitOfWork.CompanyRepository1.Remove(obj);
            _unitOfWork.Save();
            return Json(new { Success = true, message = "Deleted Successfully..." });
        }
    }
}
