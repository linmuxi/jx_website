using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jx_website.Common;

namespace jx_website.Web.DAL
{
    public class JoinusDAL
    {
        public List<Job> queryJobs()
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();
            
            List<Job> jobs = db.Query<Job>("select * from job").ToList<Job>();

            return jobs;
        }
    }
}