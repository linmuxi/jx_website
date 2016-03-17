using jx_website.Web.BLL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jx_website.Web.Controllers
{
    public class ProductController : Controller
    {

        ProductBLL productBLL = new ProductBLL();

        // 产品首页
        public ActionResult Index()
        {
            Product product = new Product();

            // 所有产品
            ViewBag.ProductList = productBLL.queryProductList(product);

            // 热门产品
            product.isHot = "1";
            ViewBag.HotProductList = productBLL.queryProductList(product);

            return View();
        }

        // 单个产品明细
        public ActionResult Detail(Product product)
        {

            if(null == product){
                return View();
            }

            Product pro = this.productBLL.getProductById(product.id);

            if (null != pro)
            {
                ViewBag.Title = pro.name;
            }

            // 产品明细
            ViewBag.Product = pro;

            // 相关产品(暂时没有规则，目前按照最后修改日期降序查询4个产品)
            ViewBag.MoreProduct = this.productBLL.queryMoreProduct();

            return View();
        }

        // 到客户申请页面
        public ActionResult ToClientApply()
        {
            return View("ClientApply");
        }

        // 客户申请
        public JsonResult ClientApply(ClientApply clientApply)
        {
            var success = this.productBLL.addClientApply(clientApply);
            return Json(new Message(success, "操作完成"));
        }

    }
}