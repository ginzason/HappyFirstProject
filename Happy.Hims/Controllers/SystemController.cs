using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Happy.Dac.Mis;
using Happy.Models;
using Happy.Utility;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Happy.Dac.Com;
using Happy.Dac.Hims;
namespace Happy.Hims.Controllers
{
    public class SystemController : BaseController
    {
        #region Menu
        public ActionResult Menu()
        {
            List<UserMenu> usermenuList = DataUtill.ConvertToList<UserMenu>(new Dac_Hims_MenuInfo().Select_Menu_Info().Tables[0]);
            return View(usermenuList);
        }
        [HttpPost]
        public JsonResult MenuSave(int menu_idx = 0, int parent_idx = 0, string menu_name = "", string menu_url = "", string page_name = "", int sort_order = 0)
        {
            int row = 0;
            if (menu_idx == 0)
            {
                row = new Dac_Hims_MenuInfo().Insert_Menu_info(parent_idx, menu_name, menu_url, page_name, sort_order, UserId);
            }
            else
            {
                row = new Dac_Hims_MenuInfo().Update_Menu_info(menu_idx, parent_idx, menu_name, menu_url, page_name, sort_order, UserId);
            }
            return Json(row);
        }
        [HttpPost]
        public JsonResult MenuDelete(int menu_idx = 0)
        {
            int row = new Dac_Hims_MenuInfo().Delete_Menu_Info(menu_idx);
            return Json(row);
        }
        #endregion
        #region 권한
        public ActionResult Auth()
        {
            List<AuthMenu> usermenuList = DataUtill.ConvertToList<AuthMenu>(new Dac_Hims_AuthInfo().Select_AuthMenuAll(0).Tables[0]);
            return View(usermenuList);
        }
        [HttpPost]
        public JsonResult AuthMasterList()
        {
            DataTable dt = new Dac_Hims_AuthInfo().Select_AuthMaster().Tables[0];
            var json = JsonConvert.SerializeObject(dt);

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AuthMasterSave(string name = "")
        {
            int row = new Dac_Hims_AuthInfo().Insert_AuthMaster(name, UserId);
            return Json(row);
        }
        [HttpPost]
        public JsonResult AuthMasterDelete(int au_idx = 0)
        {
            int row = new Dac_Hims_AuthInfo().Delete_AuthMaster(au_idx);
            return Json(row);
        }
        [HttpPost]
        public JsonResult AuthMenuInsert(int au_idx = 0, string menu_idx = "")
        {
            Dac_Hims_AuthInfo dac = new Dac_Hims_AuthInfo();
            int row = 0;
            row = dac.Delete_AuthMenu(au_idx);
            string[] arrMenuIdx = menu_idx.Trim().Split(',');
            foreach (var data in arrMenuIdx)
            {
                row += dac.Insert_AuthMenu(au_idx, DataUtill.ConvertInt(data), UserId);
            }

            return Json(row);
        }
        #endregion
        #region 사용자권한부여
        public ActionResult UserAuth()
        {
            List<UserInfo> list = DataUtill.ConvertToList<UserInfo>(new Dac_Hims_UserInfo().Select_UserInfoList("", 1, 9999).Tables[0]);
            return View(list);
        }
        [HttpPost]
        public JsonResult UserAuthInsert(int au_idx = 0, string userid = "")
        {
            Dac_Hims_AuthInfo dac = new Dac_Hims_AuthInfo();
            int row = 0;
            row = dac.Delete_AuthUser(au_idx);
            string[] arrMenuIdx = userid.Trim().Split(',');
            foreach (var data in arrMenuIdx)
            {
                row += dac.Insert_AuthUser(au_idx, data, UserId);
            }
            return Json(row);
        }
        #endregion
        #region 공통코드
        public ViewResult ComCodeInfo(int cat_idx = 0)
        {
            List<ComCodeInfo> codeList = DataUtill.ConvertToList<ComCodeInfo>(new Dac_Com_CodeInfo().Select_Code_Info(cat_idx, 1, 99999999).Tables[0]);
            return View(codeList);
        }
        [HttpPost]
        public JsonResult ComCodeCategory()
        {
            List<ComCodeCategory> categoryList = DataUtill.ConvertToList<ComCodeCategory>(new Dac_Com_CodeInfo().Select_Code_Category(1, 99999999).Tables[0]);
            return Json(categoryList);
        }
        [HttpPost]
        public JsonResult ComCodeMaster(int cat_idx = 0)
        {
            List<ComCodeInfo> comCodeList = DataUtill.ConvertToList<ComCodeInfo>(new Dac_Com_CodeInfo().Select_Code_Info(cat_idx, 1, 99999999).Tables[0]);
            return Json(comCodeList);
        }
        [HttpPost]
        public JsonResult CategorySave(string cat_name = "")
        {
            int row = new Dac_Com_CodeInfo().Insert_Code_Category(cat_name, UserId);
            return Json(row);
        }
        [HttpPost]
        public JsonResult ComCodeSave(int cat_idx = 0, string code_name = "", string code_desc = "")
        {
            int row = new Dac_Com_CodeInfo().Insert_Code_Master(cat_idx, code_name, code_desc, UserId);
            return Json(row);
        }
        [HttpPost]
        public JsonResult ComCodeEdit(int code_idx, int cat_idx = 0, string code_name = "", string code_desc = "")
        {
            int row = new Dac_Com_CodeInfo().Update_Code_Master(code_idx, cat_idx, code_name, code_desc, UserId);
            return Json(row);
        }
        [HttpPost]
        public JsonResult ComCodeDelete(int code_idx)
        {
            int row = new Dac_Com_CodeInfo().Delete_Code_Master(code_idx);
            return Json(row);
        }
        #endregion
    }
}
