﻿using System;

namespace Happy.Models
{
    public class UserInfo
    {
        public string userid { get; set; }
        public string userpwd { get; set; }
        public string username { get; set; }
        public string user_status { get; set; }
        public string code_name { get; set; } 
        public string create_user { get; set; }
        public DateTime create_date { get; set; }
        public string update_user { get; set; }
        public DateTime update_date { get; set; }

    }
}