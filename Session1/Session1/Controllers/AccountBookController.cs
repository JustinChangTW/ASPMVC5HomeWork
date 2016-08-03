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
            ViewBag.AccountTypeItems = GetAccountTypeSelectListItem();
            return View();
        }

        [HttpPost]
        public ActionResult Index(AccountBookViewModel pageData)
        {
            var maxIden = (accountbookdata.AccountBookDataList.Last()).iden+1;
            pageData.iden = maxIden;
            accountbookdata.AccountBookDataList.Add(pageData);

            //ViewData["AccountTypeSelectListItem"] = GetAccountTypeSelectListItem();
            //ViewBag.AccountTypeItems = GetAccountTypeSelectListItem();

            return View();
        }

        [ChildActionOnly]
        public ActionResult GetAccountBookList()
        {
            return View(accountbookdata.AccountBookDataList);
        }


        public IEnumerable<SelectListItem> GetAccountTypeSelectListItem()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem { Text="1.支出",Value="1" },
                new SelectListItem { Text="2.收入",Value="2" }
            }.AsEnumerable();
        }
    }
}