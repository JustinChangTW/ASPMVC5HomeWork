using Session1.Models;
using Session1.Repositories;
using Session1.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Session1.Controllers
{
    public class AccountBookController : Controller
    {
        //AccountBookData accountbookdata = AccountBookData.GetInstance();
        private AccountBookService _accountbookSerivce = null;

        public AccountBookController()
        {
            var unitOfWork = new EFUnitOfWork();
            _accountbookSerivce = new AccountBookService(unitOfWork);
        }

        // GET: AccountBook
        public ActionResult Index()
        {
            ViewBag.AccountTypeItems = GetAccountTypeSelectListItem();
            return View();
        }

        [HttpPost]
        public ActionResult Index(AccountBookViewModel pageData)
        {
            //var maxIden = (accountbookdata.AccountBookDataList.Last()).iden+1;
            //pageData.iden = maxIden;
            //accountbookdata.AccountBookDataList.Add(pageData);

            //ViewData["AccountTypeSelectListItem"] = GetAccountTypeSelectListItem();
            ViewBag.AccountTypeItems = GetAccountTypeSelectListItem();

            _accountbookSerivce.Add(pageData);
            _accountbookSerivce.Save();

            return View();
        }

        [ChildActionOnly]
        public ActionResult GetAccountBookList()
        {
            return  View(_accountbookSerivce.GetAll().OrderBy(a=>a.AcountDate));
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