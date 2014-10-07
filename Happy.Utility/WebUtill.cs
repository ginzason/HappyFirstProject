using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public static string GetPaging(int totalRow, int pageSize)
        {
            int currentPage = RequestByInt("page") != 0 ? RequestByInt("page") : 1;
            StringBuilder PagerHTML = new StringBuilder();
            PagerHTML.Append("<input type=\"hidden\" id=\"page\" name=\"page\" value=\"" + currentPage + "\" />");
            PagerHTML.Append("<div class=\"paging\">");
            int page = currentPage;
            int currentBlodkEndNo;		// 현재 블럭에서의 페이지 단위 값 : 10, 20, 30...
            int endNum;					// 현재 페이지 번호의 뒷자리 값
            int endNoOfLoop;			// 페이지 블럭에서 마지막 페이지 번호의 값		

            string tempStr, tempImgStr;

            // 총 페이지 수 구하기
            int pageCount = ((totalRow - 1) / pageSize) + 1;

            string strPage = page.ToString();
            // 현재 페이지 번호의 뒷자리 값 구하기
            endNum = int.Parse(strPage.Substring(strPage.Length - 1));

            // 현재 자신의 페이지 블럭에서 마지막 페이지 값 구하기.
            if ((page % 10) == 0)
                currentBlodkEndNo = page;
            else
                currentBlodkEndNo = (page + 10) - endNum;

            // 페이지 블럭에서 마지막 페이지 번호 값 구하기
            // 가장 마지막 페이지 블럭에 도달했을 경우에는 endNoOfLoop가 잔여 페이지 수치로 나온다.
            // 그 외의 경우에는 모두 currentBlodkEndNo과 같은 수가 나온다.
            if (pageCount > currentBlodkEndNo)
                endNoOfLoop = currentBlodkEndNo;
            else
                endNoOfLoop = pageCount;


            // 이전 10개 기능 적용
            if (currentBlodkEndNo > 10)
            {
                PagerHTML.Append("<a href='javascript:;' onclick=\"Common.GoPage(1);\"> [<<] </a>");
            }
            else
            {
                PagerHTML.Append("<a href=\"#\"> [<<] </a>");
            }

            // 이전 페이지로 가기 기능 적용
            if (page <= 1)
            {
                PagerHTML.Append("<a href=\"#\"> [<] </a>");
            }
            else
            {
                PagerHTML.Append(string.Format("<a href='javascript:;'  onclick=\"Common.GoPage({0});\"> [<] </a>", page - 1));
            }

            // 1,2,3,4,5,6,7,8,9,10
            for (int i = currentBlodkEndNo - 9; i <= endNoOfLoop; i++)
            {
                if (i == page)
                {
                    PagerHTML.Append(string.Format("<a href=\"#\" class=\"paging_on\">{0}</a>", i));
                }
                else
                {
                    PagerHTML.Append(string.Format("<a href=\"javascript:Common.GoPage({0})\">{0}</a>", i));
                }
            }

            // 다음 페이지로 가기 기능 적용
            if (page == pageCount)
            {
                PagerHTML.Append("<a href=\"#\"> [>] </a>");
            }
            else
            {
                PagerHTML.Append(string.Format("<a href='javascript:;' onclick=\"Common.GoPage({0});\"> [>] </a>", page + 1));
            }

            // 다음 10개 기능 적용
            if (pageCount > currentBlodkEndNo)
            {
                PagerHTML.Append(string.Format("<a href='javascript:;' onclick=\"Common.GoPage({0});\"> [>>]  </a>", pageCount));
            }
            else
            {
                PagerHTML.Append("<a href=\"#\"> [>>]  </a>");
            }

            PagerHTML.Append("</div>");
            return PagerHTML.ToString();
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
    }
}
