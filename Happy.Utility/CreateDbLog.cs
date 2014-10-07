using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;

namespace Happy.Utility
{
    public class CreateLog
    {
        public void WriteDbLog(string query, List<SqlParameter> param)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n-------------------------------------------------------------------------\r\n");
            sb.Append(string.Format("{0} {1}\r\n", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
            sb.Append(string.Format("EXEC {0} ", query));

            int count = 0;
            if (param != null)
            {
                foreach (var data in param)
                {
                    if (count == 0)
                    {
                        sb.Append(string.Format(" '{0}'", data.Value));
                    }
                    else
                    {
                        sb.Append(string.Format(" ,'{0}'", data.Value));
                    }
                    count++;
                }
            }
            string FilePath = HttpContext.Current.Request.MapPath("/log/dblog/") + DateTime.Now.ToShortDateString().Replace("-", "") + ".log";
            string DirPath = HttpContext.Current.Request.MapPath("/log/dblog/");
            string temp;

            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);

            try
            {
                if (!di.Exists)
                {
                    Directory.CreateDirectory(DirPath);
                }

                if (!fi.Exists)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        temp = sb.ToString();
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        temp = sb.ToString();
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void FileUplodLog(string fileName, string originName, string path, string result)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n-------------------------------------------------------------------------\r\n");
            sb.Append(string.Format("{0} {1}\r\n", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
            sb.Append(string.Format("Original File Name : {0}\r\n", originName));
            sb.Append(string.Format("Replace File Name : {0}\r\n", fileName));
            sb.Append(string.Format("Path : {0}\r\n", path));
            sb.Append(string.Format("Result : {0}\r\n", result));
            string FilePath = HttpContext.Current.Request.MapPath("/log/fileUplodLog/") + DateTime.Now.ToShortDateString().Replace("-", "") + ".log";
            string DirPath = HttpContext.Current.Request.MapPath("/log/fileUplodLog/");
            string temp;

            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);
            try
            {
                if (!di.Exists)
                {
                    Directory.CreateDirectory(DirPath);
                }

                if (!fi.Exists)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        temp = sb.ToString();
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        temp = sb.ToString();
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
