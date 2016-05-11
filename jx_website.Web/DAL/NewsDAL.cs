using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jx_website.Common;

namespace jx_website.Web.DAL
{
    public class NewsDAL
    {
        public List<News> queryNewsList(News news)
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();

            //PetaPoco.Page<News> newsPage = db.Page<News>(10, 1, "select count(1) from news", null, "select * from news order by last_update_date desc",null);

            PetaPoco.Page<News> newsPage = db.Page<News>(news.pageNo, news.pageLimit, "select * from news where is_show = 1 order by create_date desc ",news.sortField,news.sortType);

            return newsPage.Items;
        }

        public News getNewsById(int id)
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();
            return db.SingleOrDefault<News>("select * from news n where n.id = @0", id);
        }
    }
}