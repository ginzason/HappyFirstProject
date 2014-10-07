﻿using Happy.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Happy.Mis.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FaceBook()
        {
            return View();
        }

        public ActionResult FtpUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file1)
        {
            bool isresult = WebUtill.LicenseFileUpload(file1);
            return RedirectToAction("FtpUpload");
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
