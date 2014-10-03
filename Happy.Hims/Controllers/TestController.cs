using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
