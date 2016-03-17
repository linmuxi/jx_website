using jx_website.Web.DAL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.BLL
{
    public class NewsBLL
    {
        private NewsDAL newsDAL = new NewsDAL();

        public List<News> queryNewsList(News news)
        {
            return this.newsDAL.queryNewsList(news);
        }
        public News getNewsById(int id)
        {
            return this.newsDAL.getNewsById(id);
        }
    }
}