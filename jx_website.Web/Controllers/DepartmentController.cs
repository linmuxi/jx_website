using jx_website.Web.BLL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jx_website.Web.Controllers
{
    public class DepartmentController : Controller
    {
        public DepartmentBLL departmentBLL = new DepartmentBLL();

        public ActionResult Index()
        {

            List<Department> list = departmentBLL.queryDepartmentList(new Department());

            ViewBag.DepartmentList = list;
            ViewBag.QueryCondition = new Department();

            return View();
        }

        public ActionResult QueryBranch(Department department)
        {
            // 保存查询条件，供页面回显
            Department searchDepartment = department.Clone();

            // var city = Request.QueryString["city"];
            // var queryKey = Request.QueryString["queryKey"];

            if(department.city == "undefined"){
                department.city = "";
            }
            
            //如果省份和城市相同，则按照省份查询
            if(department.province == department.city){
                department.city = "";
            }
            else{
                //否则按照城市查询
                department.province = "";
            }

            List<Department> list = departmentBLL.queryDepartmentList(department);

            ViewBag.DepartmentList = list;
            ViewBag.QueryCondition = searchDepartment;

            return View("Index");
        }

    }
}