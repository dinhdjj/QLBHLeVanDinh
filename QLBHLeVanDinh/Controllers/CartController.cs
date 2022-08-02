using QLBHLeVanDinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBHLeVanDinh.Controllers
{
    public class CartController : Controller
    {
        NWDataClassesDataContext da = new NWDataClassesDataContext();

        public ActionResult Index()
        {
            var cart = getCart();

            return View(cart);
        }

        public ActionResult AddToCart(int productID)
        {
            var cartItem = new CartItem(productID);

            var cart = getCart();

            if (cart.Exists(i => i.ProductID == cartItem.ProductID))
            {
                var itemIndex = cart.FindIndex(i => i.ProductID == cartItem.ProductID);
                cartItem.Quantity += cart[itemIndex].Quantity;
                cart[itemIndex] = cartItem;
            }
            else
            {
                cart.Add(cartItem);
            }

            Session["cart"] = cart;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int productID)
        {
            var cart = getCart();

            cart.RemoveAll(i => i.ProductID == productID);

            Session["cart"] = cart;

            return RedirectToAction("Index");
        }

        private List<CartItem> getCart()
        {
            var cart = Session["cart"] as List<CartItem>;

            if(cart == null)
            {
                cart = new List<CartItem>();
                Session["cart"] = cart;
            }

            return cart;
        }
    }
}