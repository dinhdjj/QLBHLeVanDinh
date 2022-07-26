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
            var ps = da.Products.ToList();
            return View(ps);
        }
    }
}