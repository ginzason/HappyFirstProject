using Happy.Utility;
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
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0)
            {
                string path = WebUtill.GetAppSetting("FtpDir");
                string fileRename = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmsstt"));
                string [] arrOrigin = file.FileName.Split('.');
                string rename = string.Format("{0}.{1}", fileRename, arrOrigin[arrOrigin.Length - 1]);

                file.SaveAs(path+rename);

                //string ftpUrl = WebUtill.GetAppSetting("FtpUrl");
                //string ftpId = WebUtill.GetAppSetting("FtpId");
                //string ftpPwd = WebUtill.GetAppSetting("FtpPwd");
                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                //request.Method = WebRequestMethods.Ftp.UploadFile;

                //// This example assumes the FTP site uses anonymous logon.
                //request.Credentials = new NetworkCredential("tomochan", "xhahcks80!!");

                //// Copy the contents of the file to the request stream.
                //StreamReader sourceStream = new StreamReader(file.InputStream);
                //byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                //sourceStream.Close();
                //request.ContentLength = fileContents.Length;

                //Stream requestStream = request.GetRequestStream();
                //requestStream.Write(fileContents, 0, fileContents.Length);
                //requestStream.Close();

                //FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                //new CreateLog().FileUplodLog(file.FileName, file.FileName, "");

                //response.Close();
            }
            return RedirectToAction("FtpUpload");
        }

    }
}
