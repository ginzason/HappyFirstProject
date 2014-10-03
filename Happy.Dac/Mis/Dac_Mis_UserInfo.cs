using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Happy.Utility;

namespace Happy.Dac.Mis 
{
    public class Dac_Mis_UserInfo : DbHelper
    {
        /// <summary>
        /// 사용자 저장
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="pwd">비밀번호</param>
        /// <param name="name">이름</param>
        /// <param name="status">상태</param>
        /// <param name="createUser">등록자</param>
        /// <returns>행수</returns>
        public int Insert_UserInfo(string id, string pwd, string name, int status, string createUser)
        {
            string qry = "SP_Mis_INSERT_USER_INFO";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@USERID", id));
            ParamList.Add(new SqlParameter("@USERPWD", pwd));
            ParamList.Add(new SqlParameter("@USERNAME", name));
            ParamList.Add(new SqlParameter("@USER_STATUS", status));
            ParamList.Add(new SqlParameter("@CREATE_USER", createUser));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// 사용자 존재유무
        /// </summary>
        /// <param name="id">아이디</param>
        /// <returns>사용자 카운트</returns>
        public int Select_UserCount(string id)
        {
            string qry = "SP_Mis_SELECT_USER_INFO_COUNT";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("USERID", id));
            return DataUtill.ConvertInt(SqlGetScarlar(qry, ParamList, System.Data.CommandType.StoredProcedure));
        }

        /// <summary>
        /// 비밀번호 변경
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="pwd">비밀번호</param>
        /// <param name="updateUser">변경자</param>
        /// <returns>행수</returns>
        public int Update_UserInfoPwd(string id, string pwd, string updateUser)
        {
            string qry = "SP_Mis_UPDATE_USER_INFO_PWD";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@USERID", id));
            ParamList.Add(new SqlParameter("@USERPWD", pwd));
            ParamList.Add(new SqlParameter("@UPDATE_USER", updateUser));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// 상태변경
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="status">상태변경</param>
        /// <param name="updateUser">변경자</param>
        /// <returns>행수</returns>
        public int Update_UserInfoStatus(string id, int status, string updateUser)
        {
            string qry = "SP_MIS_UPDATE_USER_INFO_PWD";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@USERID", id));
            ParamList.Add(new SqlParameter("@@USER_STATUS", status));
            ParamList.Add(new SqlParameter("@UPDATE_USER", updateUser));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// 사용자 리스트
        /// </summary>
        /// <param name="name">이름</param>
        /// <param name="start">시작번호</param>
        /// <param name="end">종료번호</param>
        /// <returns>리스트</returns>
        public DataSet Select_UserInfoList(string name, int start, int end)
        {
            string qry = "SP_MIS_SELECT_USER_INFO";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@USERNAME", name));
            ParamList.Add(new SqlParameter("@SNUM", start));
            ParamList.Add(new SqlParameter("@ENUM", end));
            return SqlGetDataSet(qry, ParamList, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 아이디중복체크
        /// </summary>
        /// <param name="id">아이디</param>
        /// <returns>카운트</returns>
        public object Select_UserInfoCount(string id)
        {
            string qry = "SP_MIS_SELECT_USER_INFO";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@USERID", id));
            return SqlGetScarlar(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 사용자 정보
        /// </summary>
        /// <param name="id">아이디</param>
        /// <param name="pwd">비번</param>
        /// <returns></returns>
        public DataSet Select_UserInfo(string id)
        {
            string qry = "SP_MIS_SELECT_USER_LOGIN";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@USERID", id));
            return SqlGetDataSet(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 사용자권한 메뉴
        /// </summary>
        /// <param name="id">아이디</param>
        /// <returns></returns>
        public DataSet Select_UserMenu(string id)
        {
            string qry = "SP_MIS_SELECT_USERAUTH_MENU";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@USERID", id));
            return SqlGetDataSet(qry, ParamList, CommandType.StoredProcedure);
        }

    }
}
