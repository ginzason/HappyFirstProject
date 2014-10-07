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
            string path = WebUtill.GetAppSetting("FtpDir");
            string filename = "2014100713103764.xlsx";
            return new DownloadResult
            {
                FileName = "파일.xlsx",
                Path = path + filename
            };
        }

    }
}
