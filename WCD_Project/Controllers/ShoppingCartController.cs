using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCD_Project.Models;
using WCD_Project.ViewModel;

namespace WCD_Project.Controllers
{
    public class ShoppingCartController : Controller
    {
        private StoreEntities storeDB = new StoreEntities();

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            var addedProduct = storeDB.Products
                .Single(product => product.ProductID == id);

            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string productName = storeDB.Carts
                .Single(item => item.RecordId == id).Product.Name;

            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) +
                    " đã bị xóa khỏi giỏ hàng.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
    }
}