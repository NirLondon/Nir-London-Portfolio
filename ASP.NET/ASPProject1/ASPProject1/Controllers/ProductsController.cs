using ASP_Project.Common.Models;
using ASP_Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ASP_Project.Client.Controllers
{
    public class ProductsController : Controller
    {
        [HttpPost]
        public ActionResult Buy(int productId)
        {
            DataCollector.BuyProduct(productId);
            var ids = Request.Cookies["cart"]["productsIds"];
            var idsArr = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Response.Cookies["cart"]["productsIds"] = string.Empty;
            foreach (var id in idsArr)
            {
                if (id != productId.ToString())
                    Response.Cookies["cart"]["productsIds"] += id + ",";
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult _ProductDetails(int productId)
        {
            var prod = DataAccessor.GetProduct(productId);
            var owner = DataAccessor.GetOwnerDetails(prod.OwnerId);
            return View(new OwnerProduct { Product = prod, Owner = owner });
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadProduct(UploadedProduct product)
        {
            if (ModelState.IsValid)
            {
                DataCollector.UploadProduct(product, Request.Cookies["user"]["userId"]);
                return RedirectToRoute(new { controller = "Home" });
            }
            return View("AddProduct", product);
        }

        public ActionResult RemoveProduct(int productId)
        {
            DataCollector.RemoveProduct(productId);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}