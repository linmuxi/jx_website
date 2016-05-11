using jx_website.Web.DAL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.BLL
{
    public class DepartmentBLL
    {
        private DepartmentDAL departmentDAL = new DepartmentDAL();

        public List<Department> queryDepartmentList(Department dept)
        {
            return this.departmentDAL.queryDepartmentList(dept);
        }

        public Department GetDepartmentByCity(string city)
        {
            return this.departmentDAL.GetDepartmentByCity(city);
        }
    }
}