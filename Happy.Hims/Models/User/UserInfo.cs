using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Happy.Mis.Models
{
    public class UserInfo
    {
        public string userid { get; set; }
        public string userpwd { get; set; }
        public string username { get; set; }
        public string create_user { get; set; }
        public DateTime create_date { get; set; }
        public string update_user { get; set; }
        public DateTime update_date { get; set; }

    }
}