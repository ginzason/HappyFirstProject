﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Happy.Dac.Mis;
using Happy.Mis.Models;
using Happy.Utility;
using System.Management;

namespace Happy.Mis.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Login()
        {
            ViewBag.ss = Security.RsaEncription("1111");
            return View();
        }
        public JsonResult LoginProc(string id= "", string pwd = "")
        {
            int count = 0;
            DataSet ds = new Dac_Mis_UserInfo().Select_UserInfo(id);
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                UserInfo userinfo = DataUtill.ConvertToRow<UserInfo>(ds.Tables[0].Rows[0]);
                if(Security.TomochanSecurityDescription(pwd) == Security.RsaDescription(userinfo.userpwd))
                {
                    InsertLoginHistory(id, "S");
                    count = CreateCookie(count, userinfo);
                }
                else
                {
                    InsertLoginHistory(id, "F");
                }
            }
            return Json(count);
        }
        public RedirectResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Login");
        }
        private int CreateCookie(int count, UserInfo userinfo)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string data = serializer.Serialize(userinfo);
            FormsAuthenticationTicket newticket = new FormsAuthenticationTicket(1,
                                                          "Mis",
                                                          DateTime.Now,
                                                          DateTime.Now.AddDays(1),
                                                          false, // always persistent
                                                          data,
                                                          FormsAuthentication.FormsCookiePath);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(newticket));
            cookie.Expires = newticket.Expiration;
            Response.Cookies.Add(cookie);
            FormsAuthentication.GetAuthCookie("Mis", true);
            CreateMenu(userinfo.userid);
            count = 1;
            return count;
        }
        private void CreateMenu(string userid)
        {
            DataSet ds = new Dac_Mis_UserInfo().Select_UserMenu(userid);
            List<UserMenu> usermenuList = DataUtill.ConvertToList<UserMenu>(ds.Tables[0]);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string data = serializer.Serialize(usermenuList);

            HttpCookie myCookie = new HttpCookie("MisMenu");
            myCookie["Menu"] = Server.UrlEncode(data);
            myCookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(myCookie);
            //Session.Add("usermenuList", usermenuList);
        }
        private void InsertLoginHistory(string id, string result)
        {
            string ip = Request.UserHostAddress == "::1" ? "127.0.0.1" : Request.UserHostAddress;
            string mac = GetMacAddress(ip);
            new Dac_Mis_LoginHis().Insert_Login_His(id, result, mac, ip);
        }

        public string GetMacAddress(string ip)
        {
            string rtn = string.Empty;
            ObjectQuery oq = new System.Management.ObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled='TRUE'");
            ManagementObjectSearcher query1 = new ManagementObjectSearcher(oq);
            foreach (ManagementObject mo in query1.Get())
            {
                string[] address = (string[])mo["IPAddress"];
                if (address.Length  == 2)
                {
                    rtn = address[1];
                }
            }
            return rtn;
        }
    }
}
