using Session1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Session1.Controllers
{
    public class AccountBookController : Controller
    {
        AccountBookData accountbookdata = AccountBookData.GetInstance();
        // GET: AccountBook
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AccountBookViewModel pageData)
        {
            var maxIden = (accountbookdata.AccountBookDataList.Last()).iden+1;
            pageData.iden = maxIden;
            accountbookdata.AccountBookDataList.Add(pageData);

            return View();
        }


        public ActionResult GetAccountBookList()
        {
            return View(accountbookdata.AccountBookDataList);
        }
    }
}