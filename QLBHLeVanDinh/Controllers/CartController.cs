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

            ViewBag.CartCount = cart.Sum(i => i.Quantity);
            ViewBag.CartAmount = cart.Sum(i => i.Total);

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

        public ActionResult Order()
        {
            var cart = getCart();

            var order = new Order();
            order.OrderDate = DateTime.Now;
            da.Orders.InsertOnSubmit(order);
            da.SubmitChanges();

            order = da.Orders.OrderByDescending(i => i.OrderID).Take(1).First();

            foreach(var item in cart)
            {
                Order_Detail d = new Order_Detail();
                d.OrderID = order.OrderID;
                d.ProductID = item.ProductID;
                d.Quantity = short.Parse(item.Quantity.ToString());
                d.Discount = 0;

                da.Order_Details.InsertOnSubmit(d);
            }

            da.SubmitChanges();

            Session["cart"] = null;
            return RedirectToAction("Index", "Product");
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