using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.Models
{
    [PetaPoco.TableName("branch")]
    [PetaPoco.PrimaryKey("id")]
    public class Department:ICloneable
    {
        public int id{get;set;}
        public String name { get; set; }
        public string address { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string tel { get; set; }

        [PetaPoco.ResultColumn("create_date")]
        public DateTime createDate { get; set; }

        [PetaPoco.ResultColumn("last_updator")]
        public string lastUpdator { get; set; }

        [PetaPoco.ResultColumn("last_update_date")]
        public DateTime lastUpdateDate { get; set; }

        public string queryKey { get;set;}

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public Department Clone()
        {
            return (Department)this.MemberwiseClone();
        } 
    }
}