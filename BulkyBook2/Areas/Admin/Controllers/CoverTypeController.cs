using BulkyBook2.Data;
using BulkyBook2.DataAccess.Repository.IRepository;
using BulkyBook2.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> ObjCoverTypeList = _unitOfWork.CoverTypeRepository1.GetAll();
            return View(ObjCoverTypeList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypeRepository1.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Bro your CoverType Created Successfully... ;) stupid bakra";
                return RedirectToAction("Index", "CoverType");
            }
            return View(obj);
        }

        //Get method for Edit view...
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverTypeFromDbFirst = _unitOfWork.CoverTypeRepository1.GetFirstOrDefault(u => u.Id == id);
            if (CoverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDbFirst);
        }

        //Post method for Edit view...
        [HttpPost]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypeRepository1.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Alright I updated it for free... next time give me money :)";
                return RedirectToAction("Index", "CoverType");
            }
            return View(obj);
        }

        //Get method for Delete view...
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CovertypeFromDbFirst = _unitOfWork.CoverTypeRepository1.GetFirstOrDefault(u => u.Id == id);
            if (CovertypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CovertypeFromDbFirst);
        }

        //Post method for delete CoverType...
        [HttpPost]
        public IActionResult Delete(CoverType obj)
        {
            _unitOfWork.CoverTypeRepository1.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Why you Delete it... bitch I'll See you motherfucker -_-";
            return RedirectToAction("Index", "CoverType");
        }
    }
}
