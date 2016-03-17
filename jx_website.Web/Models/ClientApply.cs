using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.Models
{
    [PetaPoco.TableName("client_apply")]
    [PetaPoco.PrimaryKey("id")]
    public class ClientApply
    {
        public int id{get;set;}

        public string type { get; set; }

        [PetaPoco.ResultColumn("product_id")]
        public string productId { get; set; }
        public string username { get; set; }
        public string idcard { get; set; }
        public string phone { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public string creator{get;set;}

        [PetaPoco.ResultColumn("create_date")]
        public DateTime createDate { get; set; }

        [PetaPoco.ResultColumn("last_updator")]
        public string lastUpdator { get; set; }

        [PetaPoco.ResultColumn("last_update_date")]
        public DateTime lastUpdateDate { get; set; }
        
    }
}