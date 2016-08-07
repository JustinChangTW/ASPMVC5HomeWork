using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Session1.Models.DbModel;

namespace Session1.Service
{
    public class AccountBookService
    {
        private DbModel _db;

        public AccountBookService()
        {
            _db = new DbModel();
        }

        public IEnumerable<AccountBook> GetAll()
        {
            return _db.AccountBook.ToList();
        }

        public AccountBook GetSingle(Guid Id)
        {
            var accountbook = _db.AccountBook.Where(a => a.Id == Id).SingleOrDefault();
            return accountbook;
        }

        public void Add(AccountBook pageData)
        {
            _db.AccountBook.Add(pageData);
        }

        public void Edit(AccountBook pageData,AccountBook currentData)
        {
            
        }

        public void Del(AccountBook pageData)
        {
            _db.AccountBook.Remove(pageData);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}