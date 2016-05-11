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

            var sql = "select * from product p where 1 = 1 and is_show = '1' ";

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

            return db.Single<Product>("select * from product p where is_show = 1 and p.id = @0", id);
        }

        //相关产品
        public List<Product> queryMoreProduct()
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();

            var sql = "select * from product p where is_show = 1 order by last_update_date desc";

            PetaPoco.Page<Product> productPage = db.Page<Product>(1, 4, sql, new object[] { });

            return productPage.Items;
        }

        /// <summary>
        /// 添加客户申请
        /// </summary>
        /// <param name="clientApply"></param>
        /// <returns></returns>
        public object addClientApply(ClientApply clientApply)
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();

            return db.Insert(clientApply);

            //return db.Execute("INSERT INTO client_apply(TYPE,product_id,username,idcard,phone,sex,address) VALUES(@0,@1,@2,@3,@4,@5,@6)",
            //    clientApply.type, clientApply.productId, clientApply.username, clientApply.idcard, clientApply.phone, clientApply.sex, clientApply.address) > 0;
        }

        public ClientApply getClientApplyByPhoneAndUsername(ClientApply obj)
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();

            //return db.Single<ClientApply>("select * from client_apply t where t.username=@0 and t.phone = @1", new object[] { obj.username, obj.phone });
            //return db.Single<ClientApply>("select * from client_apply t where t.username='11' and t.phone = '111'");

            return db.FirstOrDefault<ClientApply>("select * from client_apply t where t.username=@0 and t.phone = @1", new object[] { obj.username, obj.phone });
        }

        public bool UpdateEmailStatus(ClientApply obj)
        {
            PetaPoco.Database db = DBHelper.newInstance().getDB();
            return db.Execute("UPDATE client_apply t SET t.`is_sendMail` = '1' WHERE t.`id` = @0", new object[] { obj.id }) > 0;
        }
    }
}