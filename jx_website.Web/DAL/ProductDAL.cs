using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jx_website.Common;

namespace jx_website.Web.DAL
{
    public class ProductDAL
    {
        public List<Product> queryProductList(Product product)
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();

            var sql = "select * from product p where 1 = 1 ";

            if ("1".Equals(product.isHome))
            {
                sql += " and p.is_home = 1 ";
            }
            if ("1".Equals(product.isHot))
            {
                sql += " and p.is_hot = 1";
            }

            sql += " order by @0 @1";

            PetaPoco.Page<Product> productPage = db.Page<Product>(product.pageNo, product.pageLimit, sql, product.sortField,product.sortType);

            return productPage.Items;
        }

        public Product getProductById(int id)
        {

            PetaPoco.Database db = DBHelper.newInstance().getDB();

            return db.Single<Product>("select * from product p where p.id = @0", id);
        }

        //相关产品
        public List<Product> queryMoreProduct()
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();

            var sql = "select * from product p where 1 = 1 order by last_update_date desc";

            PetaPoco.Page<Product> productPage = db.Page<Product>(1, 4, sql, new object[] { });

            return productPage.Items;
        }

        public bool addClientApply(ClientApply clientApply)
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();
            
            return db.Execute("INSERT INTO client_apply(TYPE,product_id,username,idcard,phone,sex,address) VALUES(@0,@1,@2,@3,@4,@5,@6)",
                clientApply.type, clientApply.productId, clientApply.username, clientApply.idcard, clientApply.phone, clientApply.sex, clientApply.address) > 0;
        }
    }
}