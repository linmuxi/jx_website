using jx_website.Web.DAL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.BLL
{
    public class ProductBLL
    {
        private ProductDAL productDAL = new ProductDAL();

        public List<Product> queryProductList(Product product)
        {
            return this.productDAL.queryProductList(product);
        }
        public Product getProductById(int id)
        {
            return this.productDAL.getProductById(id);
        }
        public List<Product> queryMoreProduct()
        {
            return this.productDAL.queryMoreProduct();
        }
        public object addClientApply(ClientApply clientApply)
        {
            return this.productDAL.addClientApply(clientApply);
        }

        public ClientApply getClientApplyByPhoneAndUsername(ClientApply obj)
        {
            return this.productDAL.getClientApplyByPhoneAndUsername(obj);
        }

        public bool UpdateEmailStatus(ClientApply obj)
        {
            return this.productDAL.UpdateEmailStatus(obj);
        }
    }
}