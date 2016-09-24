using Session1.Models;
using Session1.Repositories;
using Session1.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Session1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ModifyController : Controller
    {
        private AccountBookService _accountbookSerivce = null;

        public ModifyController()
        {
            var unitOfWork = new EFUnitOfWork();
            _accountbookSerivce = new AccountBookService(unitOfWork);
        }


        // GET: Admin/Modify
        public ActionResult Index()
        {
            
            return View(_accountbookSerivce.GetAll());
        }

        // GET: Admin/Modify/Details/5
        public ActionResult Details(Guid? iden)
        {
            _accountbookSerivce.GetSingle(a => a.Id == iden);
            return View(_accountbookSerivce.GetSingle(a => a.Id == iden));
        }

        // GET: Admin/Modify/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Modify/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Modify/Edit/5
        public ActionResult Edit(Guid? Id)
        {
            //var r = _accountbookSerivce.GetSingle(a => a.Id == Id);
            var r = _accountbookSerivce.GetSingle(Id);
            return View(r);
        }

        // POST: Admin/Modify/Edit/5
        [HttpPost]
        //public ActionResult Edit(Guid iden, FormCollection collection)
         public ActionResult Edit(AccountBookViewModel collection)
        {
            //try
            //{
                // TODO: Add update logic here
                //if (ModelState.IsValid)
                //{
                   _accountbookSerivce.Modify(collection);
                   _accountbookSerivce.Save();
                   return RedirectToAction("Index");
                //}
                //return View();

            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Admin/Modify/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: Admin/Modify/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
