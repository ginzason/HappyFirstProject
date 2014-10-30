using Happy.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Net.Mail;
using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using Happy.Dac.Mis;

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
        public DownloadResult FileDownload()
        {
            string path = WebUtill.GetAppSetting("EditorImgDir");
            string filename = "1.jpg";
            return new DownloadResult
            {
                FileName = "1.jpg",
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
        public ActionResult Mail()
        {
            //bool result = SendEmail(
            //    "aka.tomochan@gmail.com", 
            //    "제목",
            //    "<!DOCTYPE html>" +
            //                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
            //                "<head>" +
            //                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>" +
            //                "    <title></title>" +
            //                "</head>" +
            //                "<body>" +
            //                "    <h1>Test입니다.</h1>" +
            //                "    <p>dsfgsdfsdfsdfsdfsdfsdfsdfsdfsdf</p>" +
            //                "<img src=\"http://dbscthumb.phinf.naver.net/2881_000_1/20141009090046126_6M2N8OHP2.jpg/20141009_0710_DC.jpg?type=w646_fst;;93;true\""+
            //                "    <ul>"+
            //                "        <li>fsdfsdfsdfsd</li><li>fsdfsdfsdfsd</li><li>fsdfsdfsdfsd</li><li>fsdfsdfsdfsd</li><li>fsdfsdfsdfsd</li>"+
            //                "    </ul>"+
            //                "</body>"+
            //                "</html>",
            //                true);
            //ViewBag.Result = result;

            List<string> toList = new List<string>();
            toList.Add("aka.tomochan@gmail.com");
            toList.Add("dj_tomochan@naver.com");
            List<string> title = new List<string>();
            title.Add("title1");
            title.Add("title2");
            List<string> body = new List<string>();
            body.Add("1");
            body.Add("2");
            List<bool> resultList = SendEmail(toList, title, body, false);
            var result = "";
            foreach(var data in resultList)
            {
                result += ", " + data.ToString();
            }
            ViewBag.Result = result;
            return View();
        }
        public ActionResult Paging()
        {
            return View();
        }
        public ActionResult Video()
        {
            return View();
        }
        public ActionResult Editor()
        {
            return View();
        }

        public ActionResult DatePicker()
        {
            return View();
        }
        public ActionResult Excel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExcelUpload(HttpPostedFileBase file1)
        {
            DataTable dt = WebUtill.ExcelToDataSet(file1);
            return Redirect("Excel");
        }
        public ActionResult ExcelDown()
        {
            List<string> list = new List<string>();
            list.Add("시스템아이디");
            list.Add("사용자아이디");
            list.Add("결과");
            list.Add("맥");
            list.Add("아이피");
            list.Add("날짜");
            WebUtill.DataTableToExcelDown(new Dac_Hims_LoginHis().Select_Login_His("admin").Tables[0], list, "excel");
            return Redirect("Excel");
        }
       

    }
}
