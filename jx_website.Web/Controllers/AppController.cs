using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaPoco;
using jx_website.Web.Models;
using jx_website.Web.BLL;

namespace jx_website.Web.Controllers
{
    public class AppController : Controller
    {


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Guide()
        {

            return View();
        }
        public ActionResult DetailAddress()
        {

            return View();
        }

        public ActionResult DimensionCode()
        {
            Response.Redirect("/html/DimensionCode.html");
            return View("");
        }
    }
}