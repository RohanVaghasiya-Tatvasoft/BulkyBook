using BulkyBook2.DataAccess.Repository;
using BulkyBook2.DataAccess.Repository.IRepository;
using BulkyBook2.Models;
using BulkyBook2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBook2.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM shoppingCartVM { get; set; }
        public int OrderTotal { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM = new ShoppingCartVM
            {
                ListCart = _unitOfWork.ShoppingCartRepository1.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "product"),
                OrderHeaders = new()
            };

            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.product.Price, cart.product.Price50, cart.product.Price100);
                shoppingCartVM.OrderHeaders.OrderTotal+= (cart.Count * cart.Price);
            }

            return View(shoppingCartVM);
        }

        public IActionResult Summary()
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			shoppingCartVM = new ShoppingCartVM
			{
				ListCart = _unitOfWork.ShoppingCartRepository1.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "product"),
				OrderHeaders = new()
			};
			

			foreach (var cart in shoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.product.Price, cart.product.Price50, cart.product.Price100);
				shoppingCartVM.OrderHeaders.OrderTotal += (cart.Count * cart.Price);
			}

			return View(shoppingCartVM);
			return View();
        }

        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCartRepository1.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCartRepository1.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCartRepository1.GetFirstOrDefault(u => u.Id == cartId);
            if(cart.Count <= 1)
            {
                _unitOfWork.ShoppingCartRepository1.Remove(cart);
            }
            else
            {
				_unitOfWork.ShoppingCartRepository1.DecrementCount(cart, 1);
			}
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

		public IActionResult Remove(int cartId)
		{
			var cart = _unitOfWork.ShoppingCartRepository1.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCartRepository1.Remove(cart);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		public double GetPriceBasedOnQuantity(int quantity,double price, double price50, double price100)
        {
            if(quantity <= 50)
            {
                return price;
            }
            else
            {
                if(quantity <= 100)
                {
                    return price50;
                }
                else
                {
                    return price100;
                }
            }
        }
    }
}
