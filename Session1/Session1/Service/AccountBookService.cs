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
        private static MapperConfiguration _config = null;
        private static IMapper _mapper = null;

        public AccountBookService()
        {
            _db = new DbModel();
            SetMapperConfiguration();
        }

        private static void SetMapperConfiguration()
        {
            //_config = new MapperConfiguration(a =>
            //{
            //    a.CreateMap<AccountBookViewModel, AccountBook>()
            //    .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
            //    .ForMember(x => x.Categoryyy, y => y.MapFrom(z => z.AccountType))
            //    .ForMember(x => x.Amounttt, y => y.MapFrom(z => z.Amount))
            //    .ForMember(x => x.Dateee, y => y.MapFrom(z => z.AcountDate))
            //    .ForMember(x => x.Remarkkk, y => y.MapFrom(z => z.Mome));
            //});

            //_config = new MapperConfiguration(a =>
            //{
            //    a.CreateMap<AccountBook, AccountBookViewModel>()
            //    .ForMember(x => x.iden, y => y.MapFrom(z => 1))
            //    .ForMember(x => x.AccountType, y => y.MapFrom(z => z.Categoryyy))
            //    .ForMember(x => x.Amount, y => y.MapFrom(z => z.Amounttt))
            //    .ForMember(x => x.AcountDate, y => y.MapFrom(z => z.Dateee.ToString("yyyy-MM-dd")))
            //    .ForMember(x => x.Mome, y => y.MapFrom(z => z.Remarkkk));
            //});

            //_config.AssertConfigurationIsValid();
            //_mapper = _config.CreateMapper();
        }

        public IEnumerable<AccountBookViewModel> GetAll()
        {

            //
            _config = new MapperConfiguration(a =>
            {
                a.CreateMap<AccountBook, AccountBookViewModel>()
                .ForMember(x => x.iden, y => y.MapFrom(z => 1))
                .ForMember(x => x.AccountType, y => y.MapFrom(z => z.Categoryyy))
                .ForMember(x => x.Amount, y => y.MapFrom(z => z.Amounttt))
                .ForMember(x => x.AcountDate, y => y.MapFrom(z => z.Dateee.ToString("yyyy-MM-dd")))
                .ForMember(x => x.Mome, y => y.MapFrom(z => z.Remarkkk));
            });

            _config.AssertConfigurationIsValid();
            _mapper = _config.CreateMapper();
            //


            var getData = _db.AccountBook.ToList();
            var resualData = new List<AccountBookViewModel>();
            getData.ForEach(d => resualData.Add(_mapper.Map<AccountBookViewModel>(d)));
            return resualData;
        }

        public void Add(AccountBookViewModel pageData)
        {
            //
            _config = new MapperConfiguration(a =>
            {
                a.CreateMap<AccountBookViewModel, AccountBook>()
                .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.Categoryyy, y => y.MapFrom(z => z.AccountType))
                .ForMember(x => x.Amounttt, y => y.MapFrom(z => z.Amount))
                .ForMember(x => x.Dateee, y => y.MapFrom(z => z.AcountDate))
                .ForMember(x => x.Remarkkk, y => y.MapFrom(z => z.Mome));
            });

            _config.AssertConfigurationIsValid();
            _mapper = _config.CreateMapper();
            //


            var accountBook = _mapper.Map<AccountBook>(pageData);
            _db.AccountBook.Add(accountBook);
            
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}