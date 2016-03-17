using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jx_website.Web.Models
{
    public class Message
    {
        bool success;
        string msg;

        public Message(bool success, string msg)
        {
            this.success = success;
            this.msg = msg;
        }

        public bool Success
        {
            get { return success; }
            set { success = value; }
        }
        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }
    }
}