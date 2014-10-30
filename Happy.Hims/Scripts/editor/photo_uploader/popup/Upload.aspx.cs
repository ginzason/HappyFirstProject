using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Happy.Utility;

namespace Happy.Hims.Scripts.editor.photo_uploader.popup
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];
            string callback_func = Request.Form["callback_func"];
            string call = Request.Form["callback_func"];
            string name = WebUtill.ImgFileUpload(userPostedFile, WebUtill.GetAppSetting("EditorImgDir"));
            string returnUrl = string.Format("callback.html?callback_func={0}&bNewLine=true&sFileName={1}&sFileURL={2}",
                            callback_func, name, WebUtill.GetAppSetting("EditorUrl") + name);
            Response.Redirect(returnUrl);
        }
    }
}