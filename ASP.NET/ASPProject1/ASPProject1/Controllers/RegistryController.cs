using ASP_Project.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ASP_Project.DAL;
using System.Text;

namespace ASP_Project.Client.Controllers
{
    public class RegistryController : Controller
    {
        [HttpGet]
        public ActionResult SigninOrEdit()
        {
            if (Request.Cookies["user"] == null)
            {
                ViewBag.IsSignin = true;
                return View();
            }
            ViewBag.IsSignin = false;
            return View(DataAccessor.GetUser(int.Parse(Request.Cookies["user"]["userid"])));
        }


        [HttpPost]
        public ActionResult SigninOrEdit(SigninUser user)
        {
            bool isSignin = Request.Cookies["user"] == null;
            if (isSignin && DataAccessor.Exists(user.Username, ExistenceCheckOption.ByUsername))
                ModelState.AddModelError("username", "This username is taken. choose an other one.");

            if (isSignin && DataAccessor.Exists(user.Email, ExistenceCheckOption.ByEmail))
                ModelState.AddModelError("email", "This email is already signed in.");

            if (ModelState.IsValid)
            {
                if (isSignin) DataCollector.AddUser(user);
                else DataCollector.UpdateUser(user);

                var u = DataAccessor.GetUser(user.Username, user.Password);

                Response.Cookies["user"]["username"] = u.Username;
                Response.Cookies["user"]["userId"] = u.Id.Value.ToString();
                Response.Cookies["user"]["firstName"] = u.FirstName;
                Response.Cookies["user"]["lastname"] = u.LastNmae;
                Response.Cookies["user"].Expires = DateTime.Now.AddDays(1);

                var ids = DataAccessor.GetProductsIds(u.Id.Value).Select(p => p.ToString());
                var Ids = new StringBuilder();
                foreach (var item in ids) Ids.Append(item + ",");
                Response.Cookies["cart"]["productsIds"] = Ids.ToString();

                Response.Cookies["cart"].Expires = DateTime.Now.AddDays(-1);

                return RedirectToRoute(new { Controller = "Home" });
            }
            ViewBag.IsSignin = isSignin;
            return View(user);
        }

        //private void Signin(User user)
        //{

        //}
    }
}