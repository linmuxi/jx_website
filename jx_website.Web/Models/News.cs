using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.Models
{
    [PetaPoco.TableName("news")]
    [PetaPoco.PrimaryKey("id")]
    public class News : BaseModel
    {
        public int id{get;set;}
        public String title { get; set; }
        public string content { get; set; }

        [PetaPoco.ResultColumn("main_pic_url")]
        public string mainPicUrl { get; set; }
        public string creator{get;set;}

        [PetaPoco.ResultColumn("create_date")]
        public DateTime createDate { get; set; }

        [PetaPoco.ResultColumn("last_updator")]
        public string lastUpdator { get; set; }

        [PetaPoco.ResultColumn("last_update_date")]
        public DateTime lastUpdateDate { get; set; }

        [PetaPoco.ResultColumn("is_show")]
        public int isShow { get; set; }
    }
}