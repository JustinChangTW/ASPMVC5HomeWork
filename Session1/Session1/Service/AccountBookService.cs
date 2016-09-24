using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Session1.Models.DbModel;
using Session1.Models;
using AutoMapper;
using Session1.Repositories;

namespace Session1.Service
{
    public class AccountBookService: Repository<AccountBook>
    {
        //private DbModel _db;
        private readonly IRepository<AccountBook> _accountbookRep;
        private static MapperConfiguration _Model2Ent = null;
        private static MapperConfiguration _Ent2Model = null;
        private static IMapper _mapper = null;


        public AccountBookService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            //_db = new DbModel();
            _accountbookRep = new Repository<AccountBook>(unitOfWork);

            SetMapperConfiguration();
        }

        private static void SetMapperConfiguration()
        {
            _Model2Ent = new MapperConfiguration(a =>
            {
                a.CreateMap<AccountBookViewModel, AccountBook>()
                .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.Categoryyy, y => y.MapFrom(z => (z.AccountType == "1.支出"?1:2)))
                .ForMember(x => x.Amounttt, y => y.MapFrom(z => z.Amount))
                .ForMember(x => x.Dateee, y => y.MapFrom(z => z.AcountDate))
                .ForMember(x => x.Remarkkk, y => y.MapFrom(z => z.Mome));
            });

            _Ent2Model = new MapperConfiguration(a =>
            {
                a.CreateMap<AccountBook, AccountBookViewModel>()
                .ForMember(x => x.iden, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.AccountType, y => y.MapFrom(z => z.Categoryyy == 1 ? "1.支出" : "2.收入"))
                .ForMember(x => x.Amount, y => y.MapFrom(z => z.Amounttt))
                .ForMember(x => x.AcountDate, y => y.MapFrom(z => z.Dateee.ToString("yyyy-MM-dd")))
                .ForMember(x => x.Mome, y => y.MapFrom(z => z.Remarkkk));
            });
        }

        public IEnumerable<AccountBookViewModel> GetAll()
        {

            _Ent2Model.AssertConfigurationIsValid();
            _mapper = _Ent2Model.CreateMapper();

            //var getData = _db.AccountBook.ToList();
            var getData = _accountbookRep.LookupAll().ToList();
            var resualData = new List<AccountBookViewModel>();
            getData.ForEach(d => resualData.Add(_mapper.Map<AccountBookViewModel>(d)));
            return resualData;
        }

        public AccountBookViewModel GetSingle(Guid? Id)
        {
            var getData = _accountbookRep.GetSingle(a => a.Id == Id);
            _mapper = _Ent2Model.CreateMapper();
            var resualDate = _mapper.Map<AccountBookViewModel>(getData);
            return resualDate;
        }


        public void Add(AccountBookViewModel pageData)
        {
            _Model2Ent.AssertConfigurationIsValid();
            _mapper = _Model2Ent.CreateMapper();

            var accountBook = _mapper.Map<AccountBook>(pageData);
            //_db.AccountBook.Add(accountBook);
            _accountbookRep.Create(accountBook);
        }


        public void Modify(AccountBookViewModel pageData)
        {
            _Model2Ent.AssertConfigurationIsValid();
            _mapper = _Model2Ent.CreateMapper();

            //var accountBook = _mapper.Map<AccountBook>(pageData);

            var modify = _accountbookRep.GetSingle(a => a.Id == pageData.iden);

          
            modify.Categoryyy = pageData.AccountType == "1.支出" ? 1 : 2;
            modify.Dateee = Convert.ToDateTime(pageData.AcountDate);
            modify.Amounttt = (int)pageData.Amount;
            modify.Remarkkk = pageData.Mome;

            //_db.AccountBook.Add(accountBook);
            //_accountbookRep.(accountBook);
        }

        public void Save()
        {
            _accountbookRep.Commit();
        }
    }
}