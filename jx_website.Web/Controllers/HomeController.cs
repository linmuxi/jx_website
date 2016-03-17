using jx_website.Web.BLL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jx_website.Web.Controllers
{
    public class HomeController : Controller
    {
        NewsBLL newsBll = new NewsBLL();
        ProductBLL productBll = new ProductBLL();

        // 首页
        public ActionResult Index()
        {
            // 查询首页展示的最新10条新闻
            News news = new News();
            news.pageLimit = 10;
            news.sortField = "create_date";
            ViewBag.NewsList = newsBll.queryNewsList(news);

            // 查询首页显示的产品(4条)
            Product product = new Product();
            product.pageLimit = 4;
            product.sortField = "create_date";
            product.isHome = "1";
            ViewBag.ProductList = productBll.queryProductList(product);

            return View();
        }

        // 显示借款信息
        public JsonResult BorrowInfo()
        {
            return Json(new Message(true, "湖北武汉王先生借款50000元，用于购买65145G（13：30）"+DateTime.Now));
        }

    }
}