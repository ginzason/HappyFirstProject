using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Happy.Utility;

namespace Happy.Dac.Mis
{
    public class Dac_Mis_MenuInfo : DbHelper
    {
        /// <summary>
        /// 메뉴생성
        /// </summary>
        /// <param name="parentIdx">부모메뉴</param>
        /// <param name="menuName">메뉴명</param>
        /// <param name="menuUrl">메뉴url</param>
        /// <param name="pageUrl">페이지url</param>
        /// <param name="sort">순서</param>
        /// <param name="createUser">생성자</param>
        /// <returns></returns>
        public int Insert_Menu_info(int parentIdx, string menuName, string menuUrl, string pageUrl, int sort, string createUser)
        {
            string qry = "SP_MIS_INSERT_MENU_INFO";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@PARENT_IDX", parentIdx));
            ParamList.Add(new SqlParameter("@MENU_NAME", menuName));
            ParamList.Add(new SqlParameter("@MENU_URL", menuUrl));
            ParamList.Add(new SqlParameter("@PAGE_URL", pageUrl));
            ParamList.Add(new SqlParameter("@SORT_ORDER", sort));
            ParamList.Add(new SqlParameter("@CREATE_USER", createUser));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// 메뉴수정
        /// </summary>
        /// <param name="menu_idx">아이디</param>
        /// <param name="parentIdx">부모메뉴</param>
        /// <param name="menuName">메뉴명</param>
        /// <param name="menuUrl">메뉴url</param>
        /// <param name="pageUrl">페이지url</param>
        /// <param name="sort">순서</param>
        /// <param name="updateUser">수정자</param>
        /// <returns></returns>
        public int Update_Menu_info(int menu_idx, int parentIdx, string menuName, string menuUrl, string pageUrl, int sort, string updateUser)
        {
            string qry = "SP_MIS_UPDATE_MENU_INFO";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@MENU_IDX", menu_idx));
            ParamList.Add(new SqlParameter("@PARENT_IDX", parentIdx));
            ParamList.Add(new SqlParameter("@MENU_NAME", menuName));
            ParamList.Add(new SqlParameter("@MENU_URL", menuUrl));
            ParamList.Add(new SqlParameter("@PAGE_URL", pageUrl));
            ParamList.Add(new SqlParameter("@SORT_ORDER", sort));
            ParamList.Add(new SqlParameter("@UPDATE_USER", updateUser));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// 메뉴삭제
        /// </summary>
        /// <param name="menuIdx"></param>
        /// <returns></returns>
        public int Delete_Menu_Info(int menuIdx)
        {
            string qry = "SP_MIS_DELETE_MENU_INFO";
                List<SqlParameter> ParamList = new List<SqlParameter>();
                ParamList.Add(new SqlParameter("@MENU_IDX", menuIdx));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// 메뉴리스트
        /// </summary>
        /// <returns></returns>
        public DataSet Select_Menu_Info()
        {
            string qry = "SP_MIS_SELECT_MENU_INFO";
            return SqlGetDataSet(qry, null, CommandType.StoredProcedure);
        }
    }
}
