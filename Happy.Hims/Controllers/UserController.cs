using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Happy.Dac.Mis;
using Happy.Models;
using Happy.Utility;

namespace Happy.Hims.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            if(Request["ReturnUrl"] != null && Request["ReturnUrl"] != "")
            {
                ViewBag.url = HttpUtility.UrlDecode(Request["ReturnUrl"]);
            }
            if(User.Identity.IsAuthenticated)
            {
                Response.Redirect(Url.Action("Index","Home"));
            }
            return View();
        }

        [HttpPost]
        public JsonResult LoginProc(string id= "", string pwd = "", string geo = "")
        {
            int count = 0;
            DataSet ds = new Dac_Hims_UserInfo().Select_UserInfo(id);
            if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                UserInfo userinfo = DataUtill.ConvertToRow<UserInfo>(ds.Tables[0].Rows[0]);
                if(Security.TomochanSecurityDescription(pwd) == Security.RsaDescription(userinfo.userpwd)
                    && userinfo.user_status == "AL")
                {
                    InsertLoginHistory(id, "S", geo);
                    count = CreateCookie(count, userinfo);
                }
                else
                {
                    InsertLoginHistory(id, "F", geo);
                }
            }
            else
            {
                InsertLoginHistory(id, "F", geo);
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
                                                          DateTime.Now.AddHours(WebUtill.RequestByInt("Cookie")),
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
            DataSet ds = new Dac_Hims_UserInfo().Select_UserMenu(userid);
            List<UserMenu> usermenuList = DataUtill.ConvertToList<UserMenu>(ds.Tables[0]);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string data = serializer.Serialize(usermenuList);

            HttpCookie myCookie = new HttpCookie("MisMenu");
            myCookie["Menu"] = Server.UrlEncode(data);
            myCookie.Expires = DateTime.Now.AddHours(WebUtill.RequestByInt("Cookie"));
            Response.Cookies.Add(myCookie);
            //Session.Add("usermenuList", usermenuList);
        }
        private void InsertLoginHistory(string id, string result, string geo)
        {
            string ip = Request.UserHostAddress == "::1" ? "127.0.0.1" : Request.UserHostAddress;
            new Dac_Hims_LoginHis().Insert_Login_His(id, result, geo, ip);
        }

        public ActionResult Deny()
        {
            return View();
        }
        
        public ViewResult SetAuth()
        {
            return View();
        }
        [HttpPost]
        public JsonResult authProc(string pwd="")
        {
            string result = "F";
            //if(pwd.Trim() == "happyQwe123!@#")
            //{
                HttpCookie cookie = new HttpCookie("himsAuth");
                cookie.Value = Security.RsaEncription("auth sucess");
                cookie.Expires = DateTime.Now.AddYears(5);
                Response.Cookies.Add(cookie);
                result = "S";
            //}
            return Json(result);
        }
    }
}
