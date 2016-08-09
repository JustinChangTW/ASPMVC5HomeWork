using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Session1.Models.DbModel;
using Session1.Models;
using AutoMapper;

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

        public void Add(AccountBookViewModel pageData)
        {

            //var accountBook = new AccountBook() { };

            //使用AutoMapper轉換
            var config = new MapperConfiguration(a => 
            {
                a.CreateMap<AccountBookViewModel, AccountBook>()
                .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.Categoryyy, y => y.MapFrom(z => z.AccountType))
                .ForMember(x => x.Amounttt, y => y.MapFrom(z => z.Amount))
                .ForMember(x => x.Dateee, y => y.MapFrom(z => z.AcountDate))
                .ForMember(x => x.Remarkkk, y => y.MapFrom(z => z.Mome));
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            //mapper
            var accountBook = mapper.Map<AccountBook>(pageData);
            //accountBook.Id = Guid.NewGuid();
            //accountBook.

            _db.AccountBook.Add(accountBook);
            
        }

        //public void Edit(Acc pageData,AccountBook currentData)
        //{
            
        //}

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