using AspNetMvcECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMvcECommerce.WebUi.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            //ViewBag.Role = mContext.Roles.Select(r => r.name).FirstOrDefault();
            //ViewBag.User = mContext.Roles.Where(u => u.id == 1).Select(r => r.name).FirstOrDefault();
            return View();
        }
    }
}