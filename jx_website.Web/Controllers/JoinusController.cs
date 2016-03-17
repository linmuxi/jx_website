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
    public class JoinusController : Controller
    {

        private JoinusBLL joinusBLL = new JoinusBLL();

        public ActionResult Index()
        {

            ViewBag.jobs = joinusBLL.queryJobs();

            return View();
        }
    }
}