using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCD_Project.Models;
using System.Data.Entity;
using System.Net;

namespace WCD_Project.Controllers
{
    public class HomeController : Controller
    {
        private StoreEntities db = new StoreEntities();

        public ActionResult Index(string searchString, int? categoryID)
        {
            ViewBag.Categories = new SelectList(db.Categories, "CategoryID", "Name");
            var products = db.Products.Include(p => p.Category);
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(Convert.ToString(categoryID)))
            {
                products = products.Where(s => s.CategoryID == categoryID);
            }

            return View(products.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}