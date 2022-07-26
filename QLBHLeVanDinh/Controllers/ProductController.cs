using QLBHLeVanDinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBHLeVanDinh.Controllers
{
    public class ProductController : Controller
    {
        NWDataClassesDataContext da = new NWDataClassesDataContext();

        // GET: Product
        public ActionResult Index()
        {
            var ps = da.Products.OrderByDescending(s=>s.ProductID).ToList();
            return View(ps);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, Product product)
        {
            da.Products.InsertOnSubmit(product);
            da.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var product = da.Products.First(p => p.ProductID == id);
            return View(product);
        }
    }
}