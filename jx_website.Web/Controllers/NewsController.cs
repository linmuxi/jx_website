using jx_website.Web.BLL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jx_website.Web.Controllers
{
    public class NewsController : Controller
    {

        private NewsBLL newsBLL = new NewsBLL();

        public ActionResult Index()
        {
            // 按照创建日期降序查询
            News news = new News();
            news.sortField = "create_date";
            news.sortType = "asc";
            List<News> list = newsBLL.queryNewsList(news);

            ViewBag.firstNews = list[0];
            
            list.RemoveAt(0);

            ViewBag.newsList = list;

            return View();
        }

        public ActionResult Detail()
        {
            string id = Request.QueryString["id"];

            ViewBag.News = this.newsBLL.getNewsById(int.Parse(id));

            return View();
        }
    }
}