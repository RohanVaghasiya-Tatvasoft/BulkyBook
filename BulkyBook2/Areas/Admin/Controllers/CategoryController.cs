using BulkyBook2.Data;
using BulkyBook2.DataAccess.Repository.IRepository;
using BulkyBook2.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> ObjCategoryList = _unitOfWork.CategoryRepository1.GetAll();
            return View(ObjCategoryList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name can't be the same as Display Order... bitch");
                ModelState.AddModelError("Displayorder", "Display order can't be the same as Display Name... stupid bitch");
                ModelState.AddModelError("CustomError", "Name and Display Order Can't be the same... Please use different Name or Display Order");

            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository1.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Bro your Category Created Successfully... ;) stupid bakra";
                return RedirectToAction("Index", "Category");
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
            // var CategoryFromDb = _context.Categories.Find(id);
            var CategoryFromDbFirst = _unitOfWork.CategoryRepository1.GetFirstOrDefault(u => u.Id == id);
            //var CategoryFromDbSingle = _context.Categories.SingleOrDefault(u => u.Id == id);
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);
        }

        //Post method for Edit view...
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and Display Order can't be equal... ullu ke pathe");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository1.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Alright I updated it for free... next time give me money :)";
                return RedirectToAction("Index", "Category");
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
            var CategoryFromDbFirst = _unitOfWork.CategoryRepository1.GetFirstOrDefault(u => u.Id == id);
            if (CategoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(CategoryFromDbFirst);
        }

        //Post method for delete category...
        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            _unitOfWork.CategoryRepository1.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Why you Delete it... bitch I'll See you motherfucker -_-";
            return RedirectToAction("Index", "Category");
        }
    }
}
