using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet; 

namespace Happy.Utility
{
    public class WebUtill
    {
        /// <summary>
        /// Request
        /// </summary>
        /// <param name="name">파라미터이름</param>
        /// <returns>string 벨류</returns>
        public static string Request(string name)
        {
            string returnValue = string.Empty;
            if (HttpContext.Current.Request[name] != null)
            {
                returnValue = HttpContext.Current.Request[name];
            }
            return returnValue;
        }

        /// <summary>
        /// Request
        /// </summary>
        /// <param name="name">파라미터이름</param>
        /// <returns>int 벨류</returns>
        public static int RequestByInt(string name)
        {
            int returnValue = 0;
            try
            {
                if (HttpContext.Current.Request[name] != null && HttpContext.Current.Request[name].Trim() != "")
                {
                    returnValue = int.Parse(HttpContext.Current.Request[name]);
                }
            }
            catch(Exception ex)
            {

            }
            return returnValue;
        }

        /// <summary>
        /// 페이저
        /// </summary>
        /// <param name="totalRow">총 레코드 수</param>
        /// <param name="pageSize">한화면에 보여줄 행 수</param>
        /// <returns></returns>
        public static string Pager(int totalRow, int pageSize)
        {
            var html = "<ul class=\"pagination\">";
            var currnetPage = RequestByInt("page") == 0 ? 1 : RequestByInt("page");

            int pagecount = ((totalRow - 1) / pageSize) + 1;
            int tmpPageNa = currnetPage % pageSize == 0 ? 0 : 1;
            int tmpPage = currnetPage / pageSize + tmpPageNa;
            int lastPage = ((tmpPage - 1) * 5) + pageSize > pagecount ? pagecount : ((tmpPage - 1) * 5) + pageSize;
            //이전가기
            if (pagecount <= 1 || currnetPage <= 5)
            {
                html += string.Format("<li class=\"disabled\"><a href=\"javascript:;\">&#60;</a></li>");
            }
            else
            {
                html += string.Format("<li><a href='javascript:;'  onclick=\"Common.GoPage({0});\">&#60;</a></li>", (((currnetPage / pageSize) - 1) * pageSize) + 1);
            }
            for (int i = ((tmpPage - 1) * 5) + 1; i <= lastPage; i++)
            {
                if (i == currnetPage)
                {
                    html += string.Format("<li class=\"active\"><a href=\"javascript:;\">{0}</a></li>", i);
                }
                else
                {
                    html += string.Format("<li><a href=\"javascript:Common.GoPage({0})\">{0}</a></li>", i);
                }
            }
            //다음가기
            if (lastPage < pagecount)
            {
                html += string.Format("<li><a href='javascript:;'>....</a></li>");
                html += string.Format("<li><a href='javascript:;'  onclick=\"Common.GoPage({0});\">{0}</a></li>", pagecount);
                html += string.Format("<li><a href='javascript:;'  onclick=\"Common.GoPage({0});\">&#62;</a></li>", (tmpPage * pageSize) + 1);
            }
            else
            {
                html += string.Format("<li class=\"disabled\"><a href=\"javascript:;\">&#62;</a></li>");
            }
            html += "</ul>";
            return html;
        }

        /// <summary>
        /// appsettings 문자열 가져오기
        /// </summary>
        /// <param name="key">키</param>
        /// <returns>value</returns>
        public static string GetAppSetting(string key)
        {
            string returnTx = string.Empty;
            if(ConfigurationManager.AppSettings[key] != null)
            {
                returnTx = ConfigurationManager.AppSettings[key].ToString();
            }
            return returnTx;
        }
        public static string ImgFileUpload(HttpPostedFile file, string dir)
        {
            bool isSucess = false;

            string fileRename = string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmssff"));
            string[] arrOrigin = file.FileName.Split('.');
            string rename = string.Format("{0}.{1}", fileRename, arrOrigin[arrOrigin.Length - 1]);
            string path = dir;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(path + rename);
                    isSucess = true;
                }
            }
            catch (Exception ex)
            {
                new CreateLog().WriteErrorLog(ex);
            }
            new CreateLog().FileUplodLog(rename, file.FileName, path, isSucess.ToString());
            return rename;
        }
        #region Email
        /// <summary>
        /// email 보내기 : 한사람
        /// </summary>
        /// <param name="to">받는사람 email addr</param>
        /// <param name="title">제목</param>
        /// <param name="body">내용</param>
        /// <param name="isHtml">html인지 아닌지</param>
        /// <returns>결과</returns>
        public static bool SendMail(string to, string title, string body, bool isHtml)
        {
            bool isTrue = false;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(WebUtill.GetAppSetting("FromMail"));
            mail.To.Add(new MailAddress(to));
            mail.Subject = title;
            mail.IsBodyHtml = true;
            mail.Body = body;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;
            SmtpClient client = new SmtpClient(WebUtill.GetAppSetting("SmtpUrl"));
            try
            {
                client.Send(mail);
                isTrue = true;
            }
            catch (Exception ex)
            {
                new CreateLog().WriteErrorLog(ex);
            }
            return isTrue;
        }
        /// <summary>
        /// email 보내기 : 여러명에게 가각다른 제목 내용
        /// </summary>
        /// <param name="to">받는사람 email addr</param>
        /// <param name="title">제목</param>
        /// <param name="body">내용</param>
        /// <param name="isHtml">html인지 아닌지</param>
        /// <returns>결과</returns>
        public static List<bool> SendMail(List<string> to, List<string> title, List<string> body, bool isHtml)
        {
            List<bool> isTrue = new List<bool>();
            for (int i = 0; i < to.Count; i++)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(WebUtill.GetAppSetting("FromMail"));
                mail.To.Add(new MailAddress(to[i]));
                mail.Subject = title[i];
                mail.IsBodyHtml = true;
                mail.Body = body[i];
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                SmtpClient client = new SmtpClient(WebUtill.GetAppSetting("SmtpUrl"));
                try
                {
                    client.Send(mail);
                    isTrue.Add(true);
                }
                catch (Exception ex)
                {
                    isTrue.Add(false);
                    new CreateLog().WriteErrorLog(ex);
                }
            }
            return isTrue;
        }
        /// <summary>
        /// email 보내기 : 여러명에게 같은 메일 보내기
        /// </summary>
        /// <param name="to">받는사람 email addr</param>
        /// <param name="title">제목</param>
        /// <param name="body">내용</param>
        /// <param name="isHtml">html인지 아닌지</param>
        /// <returns>결과</returns>
        public static bool SendMail(List<string> to, string title, string body, bool isHtml)
        {
            bool isTrue = false;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(WebUtill.GetAppSetting("FromMail"));
            for (int i = 0; i < to.Count; i++)
            {
                mail.To.Add(new MailAddress(to[i]));
            }
            mail.Subject = title;
            mail.IsBodyHtml = true;
            mail.Body = body;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;
            SmtpClient client = new SmtpClient(WebUtill.GetAppSetting("SmtpUrl"));
            try
            {
                client.Send(mail);
                isTrue = true;
            }
            catch (Exception ex)
            {
                new CreateLog().WriteErrorLog(ex);
            }
            return isTrue;
        } 
        #endregion
        /// <summary>
        /// Excel 파일 dataset으로 읽기
        /// </summary>
        /// <param name="file1"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataSet(HttpPostedFileBase file1)
        {
            #region oledb
            //DataSet ds = new DataSet();
            //string type = String.Empty;
            //string conn = string.Empty;
            //if (file1 != null && file1.ContentLength > 0)
            //{
            //    var extension = Path.GetExtension(file1.FileName).Replace(".", "").ToLower();
            //    string fileName = HttpContext.Current.Server.MapPath("/tmp/excel/") + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + extension;
            //    file1.SaveAs(fileName);
            //    if (extension == "xls")
            //    {
            //        conn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
            //               "Data Source=\"" + fileName + "\";" +
            //               "Mode=ReadWrite|Share Deny None;" +
            //               "Extended Properties='Excel 8.0; HDR=YES; IMEX=1';" +
            //               "Persist Security Info=False";
            //    }
            //    if (extension == "xlsx")
            //    {
            //        conn = "Provider=Microsoft.ACE.OLEDB.12.0;" +
            //                "Data Source=\"" + fileName + "\";" +
            //                "Mode=ReadWrite|Share Deny None;" +
            //                "Extended Properties='Excel 12.0; HDR=YES; IMEX=1';" +
            //                "Persist Security Info=False";
            //    }
            //    OleDbConnection ole = new OleDbConnection(conn);
            //    ole.Open();
            //    OleDbCommand cmd = new OleDbCommand();
            //    cmd.Connection = ole;

            //    DataTable dtSheet = ole.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //    foreach (DataRow dr in dtSheet.Rows)
            //    {
            //        string sheetName = dr["TABLE_NAME"].ToString().Replace("'", "");

            //        if (!sheetName.EndsWith("$"))
            //            continue;
            //        cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

            //        DataTable dt = new DataTable();
            //        dt.TableName = sheetName;

            //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //        da.Fill(dt);
            //        ds.Tables.Add(dt);
            //    }
            //    cmd = null;
            //    ole.Close();
            //    FileInfo file = new FileInfo(fileName);
            //    file.Delete();
            //}
            //ds.Tables.Add(ReadAsDataTable(fileName)); 
            #endregion
            DataTable dataTable = new DataTable();
            if (file1 != null && file1.ContentLength > 0)
            {
                var extension = Path.GetExtension(file1.FileName).Replace(".", "").ToLower();
                string fileName = HttpContext.Current.Server.MapPath("/tmp/excel/") + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + extension;
                file1.SaveAs(fileName);
                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();

                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }

                    foreach (Row row in rows)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                        {
                            dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                        }

                        dataTable.Rows.Add(dataRow);
                    }

                }
                dataTable.Rows.RemoveAt(0);
            }
            return dataTable;
        }

        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue == null ? "" : cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        public static void DataTableToExcelDown(DataTable dt, List<string> header, string fileName)
        {
            if (dt.Rows.Count > 0 && dt.Columns.Count == header.Count)
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Sheet1");
                for (int i = 0; i < dt.Columns.Count; i++ )
                {
                    worksheet.Cell(1, i+1).Value = dt.Columns[i].ToString();
                    worksheet.Cell(2, i+1).Value = header[i];
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 3, j+1).Value = dt.Rows[i][j].ToString();
                    }
                }
                HttpResponse httpResponse = HttpContext.Current.Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + fileName + ".xlsx\"");
                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }

                httpResponse.End();
            }
        }


    }
}
