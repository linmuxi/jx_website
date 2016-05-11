using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jx_website.Common;

namespace jx_website.Web.DAL
{
    public class DepartmentDAL
    {
        public List<Department> queryDepartmentList(Department dept)
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();

            var sqlBuilder = PetaPoco.Sql.Builder.Append("select * from branch t where 1=1 ");

            if (null != dept.city && !"".Equals(dept.city))
            {
                sqlBuilder.Append("and t.city = @0 ", dept.city);
            }

            if (null != dept.province && !"".Equals(dept.province))
            {
                sqlBuilder.Append("and t.province = @0 ", dept.province);
            }

            if (null != dept.queryKey && !"".Equals(dept.queryKey))
            {
                sqlBuilder.Append("and (t.city like '%' @0 '%' or t.name like '%' @1 '%')", dept.queryKey, dept.queryKey);
            }
            IEnumerable<Department> list = db.Query<Department>(sqlBuilder);

            return list.ToList<Department>();
        }


        public Department GetDepartmentByCity(string city)
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();
            return db.FirstOrDefault<Department>("SELECT * FROM branch t WHERE t.city LIKE CONCAT(SUBSTR(@0,1,2),'%') LIMIT 0,1", new object[] { city });
        }
    }
}