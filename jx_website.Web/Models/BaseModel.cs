using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.Models
{
    public class BaseModel
    {
        private int _pageNo = 1;
        private int _pageLimit = 20;
        private string _sortField = "last_update_date";
        private string _sortType = "desc";

        public int pageNo
        {
            get { return _pageNo; }
            set { _pageNo = value; }
        }
        public int pageLimit
        {
            get { return _pageLimit; }
            set { _pageLimit = value; }
        }

        public string sortField
        {
            get
            {
                return _sortField;
            }
            set
            {
                _sortField = value;
            }
        }

        public string sortType
        {
            get
            {
                return _sortType;
            }
            set
            {
                _sortType = value;
            }
        }
    }
}