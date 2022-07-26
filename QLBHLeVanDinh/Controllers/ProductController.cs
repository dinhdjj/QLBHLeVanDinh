﻿using QLBHLeVanDinh.Models;
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
        public ActionResult Create(Product product)
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

        public ActionResult Edit(int id)
        {
            var product = da.Products.First(p => p.ProductID == id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = da.Products.First(p => p.ProductID == id);
            product.ProductName = form["ProductName"];
            da.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}