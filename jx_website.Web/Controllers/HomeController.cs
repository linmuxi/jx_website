using jx_website.Web.BLL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
            string paramstr = "{\"pass\":\"jxjr_12346app\",\"date\":\"\"}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://218.17.125.60:6150/api/Android/DisplayData");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(paramstr);
            // request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(paramstr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            //return Json(new Message(true, "湖北武汉王先生借款50000元，用于购买65145G（13：30）"+DateTime.Now));
            return Json(new Message(true, retString));
        }

    }
}