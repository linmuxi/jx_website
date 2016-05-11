using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.Models
{
    [PetaPoco.TableName("product")]
    [PetaPoco.PrimaryKey("id")]
    public class Product : BaseModel
    {
        public int id{get;set;}
        public string name { get; set; }
        public string maxlimit { get; set; }
        public string periods { get; set; }

        [PetaPoco.ResultColumn("fit_group")]
        public string fitGroup { get; set; }

        [PetaPoco.ResultColumn("month_rate")]
        public string monthRate { get; set; }

        [PetaPoco.ResultColumn("apply_condition")]
        public string applyCondition { get; set; }

        [PetaPoco.ResultColumn("apply_data")]
        public string applyData { get; set; }

        [PetaPoco.ResultColumn("is_hot")]
        public string isHot { get; set; }

        [PetaPoco.ResultColumn("pic_url")]
        public string picUrl { get; set; }

        [PetaPoco.ResultColumn("is_home")]
        public string isHome { get; set; }

        [PetaPoco.ResultColumn("home_pic_url")]
        public string homePicUrl { get; set; }

        public string creator{get;set;}

        [PetaPoco.ResultColumn("create_date")]
        public DateTime createDate { get; set; }

        [PetaPoco.ResultColumn("last_updator")]
        public string lastUpdator { get; set; }

        [PetaPoco.ResultColumn("last_update_date")]
        public DateTime lastUpdateDate { get; set; }

        [PetaPoco.ResultColumn("product_url")]
        public string productUrl { get; set; }
    }
}