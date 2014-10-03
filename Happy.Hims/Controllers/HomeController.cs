using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Happy.Dac;
using Happy.Utility;
using Happy.Dac.Mis;
using Happy.Mis.Models;

namespace Happy.Mis.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
           
            return View();
        }
        public DownloadResult FileDownload()
        {
            return new DownloadResult
            {
                FileName = "aaa.txt",
                Path = "/Upload/점태그.txt"
            };
        }

    }
}
