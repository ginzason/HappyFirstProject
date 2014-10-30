using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Happy.Dac.Com;
using Happy.Models;
using Happy.Utility;
namespace Happy.Hims.Controllers
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
            if (CheckAuthDevice())
            {
                FormsIdentity id = (FormsIdentity)context.HttpContext.User.Identity;
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    userInfo();
                    CreateMenu();
                    var url = context.HttpContext.Request.Url.AbsolutePath.ToLower();
                    if (url.Contains("/home/index") && !ChcekAuthMenu(url))
                    {
                        context.Result = new RedirectResult("/Home/Index");   
                    }
                }
                else
                {
                    context.Result = new RedirectResult("/User/Login");
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                context.Result = new RedirectResult("/User/Deny");
            }
        }

         #region 사용자 유효성 체크
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
            ViewBag.userMenuList = menuList;
        }
        private bool CheckAuthDevice()
        {
            bool isDevice = false;
            if (Request.Cookies["himsAuth"] != null)
            {
                if (Security.RsaDescription(Request.Cookies["himsAuth"].Value).Contains("auth sucess"))
                {
                    isDevice = true;
                }
            }
            return isDevice;
        } 
        private bool ChcekAuthMenu(string url)
        {
            bool isAuth = false;
            List<UserMenu> menuList = ViewBag.userMenuList;
            foreach(var data in menuList)
            {
                if (url.Contains(data.menu_url.ToLower()))
                {
                    isAuth = true;
                }
            }
            return isAuth;
        }
         #endregion

        #region Email 보내기
        /// <summary>
        /// email 보내기  : 한사람
        /// </summary>
        /// <param name="to">받는사람 email addr</param>
        /// <param name="title">제목</param>
        /// <param name="body">내용</param>
        /// <param name="isHtml">html인지 아닌지</param>
        /// <returns>결과</returns>
        public bool SendEmail(string to, string title, string body, bool isHtml)
        {
            bool result = WebUtill.SendMail(to, title, body, isHtml);
            new Dac_Com_Email().Insert_Email(to, title, body, DateTime.Now, result, UserName);
            return result;
        }
        /// <summary>
        /// email 보내기 : 여러명에게 가각다른 제목 내용
        /// </summary>
        /// <param name="to">받는사람 email addr</param>
        /// <param name="title">제목</param>
        /// <param name="body">내용</param>
        /// <param name="isHtml">html인지 아닌지</param>
        /// <returns>결과</returns>
        public List<bool> SendEmail(List<string> to, List<string> title, List<string> body, bool isHtml)
        {
            List<bool> result = WebUtill.SendMail(to, title, body, isHtml);
            for (int i = 0; i < to.Count; i++)
            {
                new Dac_Com_Email().Insert_Email(to[i], title[i], body[i], DateTime.Now, result[i], UserName);
            }
            return result;
        }
        /// <summary>
        /// email 보내기 : 여러명에게 같은 메일 보내기
        /// </summary>
        /// <param name="to">받는사람 email addr</param>
        /// <param name="title">제목</param>
        /// <param name="body">내용</param>
        /// <param name="isHtml">html인지 아닌지</param>
        /// <returns>결과</returns>
        public bool SendEmail(List<string> to, string title, string body, bool isHtml)
        {
            bool result = WebUtill.SendMail(to, title, body, isHtml);
            string listTo = string.Empty;
            foreach (var data in to)
            {
                listTo += listTo != "" ? " ," + data : data;
            }
            new Dac_Com_Email().Insert_Email(listTo, title, body, DateTime.Now, result, UserName);
            return result;
        } 
        #endregion
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
            context.HttpContext.Response.WriteFile(Path);
        }
    }
}