using AutoMapper;
using Session1.Models;
using Session1.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Session1.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(a =>
            {
                a.AddProfile<AccountAccountBookToEntProfile>();
                a.AddProfile<AccountBookProfileToModelProfile>();
            });          
        }
    }

    public class AccountAccountBookToEntProfile : Profile
    {

        public AccountAccountBookToEntProfile()
        {
            Mapper.Initialize(
             cfg => {
                 cfg.CreateMap<AccountBookViewModel, AccountBook>()
               .ForMember(x => x.Id, y => y.MapFrom(z => Guid.NewGuid()))
               .ForMember(x => x.Categoryyy, y => y.MapFrom(z => z.AccountType))
               .ForMember(x => x.Amounttt, y => y.MapFrom(z => z.Amount))
               .ForMember(x => x.Dateee, y => y.MapFrom(z => z.AcountDate))
               .ForMember(x => x.Remarkkk, y => y.MapFrom(z => z.Mome));
             });
        }

        public override string ProfileName
        {
            get
            {
                return "AccountAccountBookToEntProfile";
            }
        }

    }



    public class AccountBookProfileToModelProfile : Profile
    {
        public AccountBookProfileToModelProfile()
        {
            Mapper.Initialize(
             cfg =>
             {
                 cfg.CreateMap<AccountBook, AccountBookViewModel>()
                .ForMember(x => x.iden, y => y.MapFrom(z => 1))
                .ForMember(x => x.AccountType, y => y.MapFrom(z => z.Categoryyy == 1 ? "1.支出" : "2.收入"))
                .ForMember(x => x.Amount, y => y.MapFrom(z => z.Amounttt))
                .ForMember(x => x.AcountDate, y => y.MapFrom(z => z.Dateee.ToString("yyyy-MM-dd")))
                .ForMember(x => x.Mome, y => y.MapFrom(z => z.Remarkkk));
             });
        }

        public override string ProfileName
        {
            get
            {
                return "AccountBookProfileToModelProfile";
            }
        }

    }
}