using PagedList;
using Session1.Models;
using Session1.Repositories;
using Session1.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Session1.Controllers
{
    [RoutePrefix("SkillTree")]
    [Route("{action=index}")]
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
        public ActionResult Index(int? page)
        {
            ViewBag.AccountTypeItems = GetAccountTypeSelectListItem();
            ViewBag.Type = "";
            TempData["date"] = "";
            var pageIndex = (page.HasValue ? page.Value < 1 ? 1 : page.Value : 1);
            TempData["page"] = pageIndex;
            return View();
        }

        //▪增加年月列表功能
        //▪可指定年月顯示該年月的所有收支
        //▪輸入 http://localhost/skilltree/yyyy/mm 可看到該年月收支
        [Route("{*date:datetime}")]
        public ActionResult Index(DateTime date,int? page)
        {
            ViewBag.AccountTypeItems = GetAccountTypeSelectListItem();
            ViewBag.Type = "";
            TempData["date"] = date.ToString("yyyy-MM");
            TempData["page"] = page;
            return View();
        }

        // GET: AccountBook
        public ActionResult IndexAjax()
        {
            ViewBag.AccountTypeItems = GetAccountTypeSelectListItem();
            ViewBag.Type = "AJAX";
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


        [HttpPost]
        public ActionResult IndexAjax(AccountBookViewModel pageData)
        {

            ViewBag.Type = "AJAX";
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
            IEnumerable<AccountBookViewModel> returnDate = null;
            //增加日期過瀘條件
            String date;
            if ((String)TempData["date"] != "")
            {
                date = (String)TempData["date"];
                returnDate = _accountbookSerivce.GetAll().Where(a => a.AcountDate.Substring(0, 7) == date).OrderByDescending(a => a.AcountDate);
            }
            else
            {
                returnDate = _accountbookSerivce.GetAll().OrderByDescending(a => a.AcountDate);
            }

            var page = (int)TempData["page"];

            var pageSize = 10;

            return View(returnDate.ToPagedList(page,pageSize));

         }

        public ActionResult GetAccountBookListAjax(AccountBookViewModel pageData)
        {
            Thread.Sleep(1000);
            if (pageData.AccountType != null)
            {
                ViewBag.AccountTypeItems = GetAccountTypeSelectListItem();
                _accountbookSerivce.Add(pageData);
                _accountbookSerivce.Save();
            }
            return View(_accountbookSerivce.GetAll().OrderByDescending(a => a.AcountDate));
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