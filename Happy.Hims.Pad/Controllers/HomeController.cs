using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Happy.Hims.Pad.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Pad()
        {
            string param = string.Format("{0}/{1}", UserId, Server.UrlDecode(UserName));
            return Json(param);
        }
    }
}
