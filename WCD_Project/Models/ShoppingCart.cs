using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WCD_Project.Models
{
    public partial class ShoppingCart
    {
        private StoreEntities storeDB = new StoreEntities();
        private string ShoppingCartID { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartID = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product Product)
        {
            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.CartID == ShoppingCartID
                && c.ProductID == Product.ProductID);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductID = Product.ProductID,
                    CartID = ShoppingCartID,
                    Count = 1,
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }

            storeDB.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = storeDB.Carts.Single(
                cart => cart.CartID == ShoppingCartID
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }

                storeDB.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartID == ShoppingCartID);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }

            storeDB.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.CartID == ShoppingCartID).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartID == ShoppingCartID
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartID == ShoppingCartID
                              select (int?)cartItems.Count *
                              cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductID = item.ProductID,
                    OrderID = order.OrderID,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Product.Price);

                storeDB.OrderDetails.Add(orderDetail);
            }

            order.Total = orderTotal;

            storeDB.SaveChanges();

            EmptyCart();

            return order.OrderID;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();

                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartID == ShoppingCartID);

            foreach (Cart item in shoppingCart)
            {
                item.CartID = userName;
            }
            storeDB.SaveChanges();
        }
    }
}