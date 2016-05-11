using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.Models
{
    [PetaPoco.TableName("job")]
    [PetaPoco.PrimaryKey("id")]
    public class Job
    {
        public int id{get;set;}
        public String name{get;set;}
        public string money { get; set; }

        [PetaPoco.ResultColumn("work_address")]
        public string workAddress { get; set; }
        public string duty { get; set; }
        public string demand{get;set;}
        public string dept{get;set;}
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