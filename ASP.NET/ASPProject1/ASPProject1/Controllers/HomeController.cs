using ASP_Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Project.Common.Models;
using System.Threading;

namespace ASP_Project.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(SortBy? sortBy)
        {
            string uid = Request.Cookies["user"]?["userId"];
            var prods = DataAccessor.GetAllProducts(uid).ToClientProducts();
            switch (sortBy)
            {
                case SortBy.Date:
                    prods = prods.OrderByDescending(p => p.UploadingDate);
                    break;
                case SortBy.Title:
                    prods = prods.OrderBy(p => p.Title);
                    break;
                default: break;
            }
            return View("Index", prods);
        }

        public ActionResult Login(LoginUser user)
        {
            if (DataAccessor.Exists(user) && ModelState.IsValid)
            {
                Response.Cookies["user"]["username"] = user.Username;
                Response.Cookies["user"]["userId"] = user.Id.Value.ToString();
                Response.Cookies["user"]["firstName"] = user.FirstName;
                Response.Cookies["user"]["lastname"] = user.LastNmae;
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(1);
                var ids = DataAccessor.GetProductsIds(user.Id.Value).Select(p => p.ToString());
                string Ids = string.Empty;
                foreach (var item in ids) Ids += item + ",";
                Response.Cookies["cart"]["productsIds"] = Ids;
            }

            else ViewBag.LoginMessage = "Username or more password is not correct.";
            return Index(null);
        }

        public ActionResult Logout()
        {
            Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["cart"].Expires = DateTime.Now.AddDays(-1);
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Cart()
        {
            IEnumerable<ClientProduct> cart;
            if (Request.Cookies["user"] != null)
            {
                var username = Request.Cookies["user"]["username"];
                var userId = Request.Cookies["user"]["userId"] /*?? null*/;
                cart = DataAccessor.GetCartOf(username, userId);
            }
            else
            {
                int[] ids = Request.Cookies["cart"]?["productsIds"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s))
                    .ToArray();
                cart = DataAccessor.GetProductsByIds(ids)?.ToClientProducts();
            }
            return View(cart);
        }

        public ActionResult YourProducts()
        {
            var user = Request.Cookies["user"]?.Values;
            if (user != null)
                return View(DataAccessor.GetProductsOf(user["username"], user["userId"]));
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult loginBox(LoginUser user)
        {
            if (ModelState.IsValid && DataAccessor.Exists(user))
            {
                Response.Cookies["user"]["username"] = user.Username;
                Response.Cookies["user"]["userId"] = user.Id.Value.ToString();
                Response.Cookies["user"]["firstName"] = user.FirstName;
                Response.Cookies["user"]["lastname"] = user.LastNmae;
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(1);
                var ids = DataAccessor.GetProductsIds(user.Id.Value).Select(p => p.ToString());
                string Ids = string.Empty;
                foreach (var item in ids) Ids += item + ",";
                Response.Cookies["cart"]["productsIds"] = Ids;

            }
            else ModelState.AddModelError("ErMsg", "Username or more password is not correct.");

            return PartialView(user);
        }

        [HttpPost]
        public ActionResult SaveProductForUser(string productId, string userId = null)
        {
            DataCollector.SaveProductFor(productId, userId);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult ReleaseProduct(string productId)
        {
            DataCollector.ReleaseProducts(productId);
            return Redirect(Request.UrlReferrer.ToString());
        }

        //[HttpPost]
        public void ReleaseProducts(/*params string[] ids*/)
        {
            string[] ids = Request.Cookies["cart"]["productsIds"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            DataCollector.ReleaseProducts(ids);
        }

        public ActionResult About()
        {
            return View();
        }
    }

    public enum SortBy
    {
        Date = 0,
        Title = 1,
    }
}