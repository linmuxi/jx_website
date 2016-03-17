using jx_website.Web.DAL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.BLL
{
    public class JoinusBLL
    {
        private JoinusDAL joinusDAL = new JoinusDAL();

        public List<Job> queryJobs()
        {
            return this.joinusDAL.queryJobs();
        }
    }
}