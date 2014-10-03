using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Happy.Mis.Models;

namespace Happy.Mis.Controllers
{
    public class BaseController : Controller
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// 베이스 컨트롤러
        /// </summary>
        /// <param name="context"></param>
        protected override void OnAuthorization(AuthorizationContext context)
        {
            FormsIdentity id = (FormsIdentity)context.HttpContext.User.Identity;
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                userInfo();
                CreateMenu();
            }
            else
            {
                context.Result = new RedirectResult("/User/Login");
            }
        }
        /// <summary>
        /// 로그인계정정보
        /// </summary>
        /// <returns></returns>
        private UserInfo userInfo()
        {
            UserInfo info = new UserInfo();
            FormsIdentity id = (FormsIdentity)User.Identity;
            if (id.IsAuthenticated)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                info = serializer.Deserialize<UserInfo>(id.Ticket.UserData);
                ViewBag.Name = info.username;
                UserName = info.username;
                UserId = info.userid;
            }
            return info;
        }
        /// <summary>
        /// 메뉴생성
        /// </summary>
        private void CreateMenu()
        {
            List<UserMenu> menuList = new List<UserMenu>();
            if (Request.Cookies["MisMenu"] != null)
            {
                if (Request.Cookies["MisMenu"]["Menu"] != null)
                { 
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    menuList = serializer.Deserialize<List<UserMenu>>(Server.UrlDecode(Request.Cookies["MisMenu"]["Menu"].ToString()));
                }
            }

           // var list = Session["usermenuList"] as List<UserMenu>;
            ViewBag.userMenuList = menuList;
        }
    }

    public class DownloadResult : ActionResult
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            context.HttpContext.Response.ContentType = "application/unknown";   // 모든 파일 강제 다운로드
            context.HttpContext.Response.WriteFile(context.HttpContext.Server.MapPath(Path));
        }
    }
}