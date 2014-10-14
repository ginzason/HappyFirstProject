using Happy.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Happy.Hims.Controllers
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
            string path = WebUtill.GetAppSetting("FtpDir");
            string filename = "2014100713103763.xlsx";
            return new DownloadResult
            {
                FileName = "파일.xlsx",
                Path = path + filename
            };
        }
        public ActionResult Zip()
        {
            string query = "http://biz.epost.go.kr/KpostPortal/openapi?regkey=";

            string authkey = "cc147c3dea0547fa21412663380861";
            string keyword = "서초"; //검색할 읍면동 정보, 파라미터로 받아서 처리하셔도 됩니다.

            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding("euc-kr");

            query = query + authkey + "&target=post&query=" + HttpUtility.UrlEncode(keyword, euckr);
            WebClient client = new WebClient();
            client.Headers["accept-language"] = "ko";
            XmlTextReader reader = new XmlTextReader(client.OpenRead(query));
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element )
                {
                    if(reader.Name == "address")
                        Response.Write("주소 : " + reader.ReadElementString() );
                    if (reader.Name == "postcd")
                        Response.Write(" / 우편번호 : " + reader.ReadElementString() + "<br>");
                }
            }
            ViewBag.ssss = jsonText;
            return View();
        }
        public JsonResult ZipCode(string name = "")
        {
            string query = "http://biz.epost.go.kr/KpostPortal/openapi?regkey=";
            string authkey = WebUtill.GetAppSetting("Zip");
            string keyword = name; //검색할 읍면동 정보, 파라미터로 받아서 처리하셔도 됩니다.
            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding("euc-kr");
            query = query + authkey + "&target=post&query=" + HttpUtility.UrlEncode(keyword, euckr);
            WebClient client = new WebClient();
            client.Headers["accept-language"] = "ko";
            XmlTextReader reader = new XmlTextReader(client.OpenRead(query));
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            string jsonText = JsonConvert.SerializeXmlNode(doc).Replace("#cdata-section", "section");
            return Json(jsonText);
        }
        public ActionResult Pad(string param = "")
        {
            if (param != "")
            {
                string[] arrParam = Security.TomochanSecurityDescription(param).Split('/');
                ViewBag.Id = arrParam[0];
                ViewBag.Name = arrParam[1];
            }
            return View();
        }
    }
}
