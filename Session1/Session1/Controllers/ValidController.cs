using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ValidateSample.Controllers
{
    public class ValidController : Controller
    {
        public ActionResult Index(string acountdate)
        {
            bool isValidate = Convert.ToDateTime(acountdate) <= DateTime.Now;//.ToString("yyyy-MM-dd") ;
            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }
	}
}