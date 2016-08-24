using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ValidateSample.Filters;
using Session1.Expansion.Attribute;

namespace Session1.Models
{
    
    public class AccountBookViewModel
    {
        [DisplayName("#")]
        public int? iden { get; set;}

        [DisplayName("類別")]
        [Required(ErrorMessage = "請輸入{0}")]
        public string AccountType { get; set; }

        [DisplayName("金額")]
        [Range(0.00,(double)decimal.MaxValue,ErrorMessage = "{0}請輸入大於{1}")]
        [Required(ErrorMessage = "請輸入金額")]
        public decimal Amount { get; set; }

        [DisplayName("日期")]
        [Required(ErrorMessage = "請輸入{0}")]
        //[RemotePlusAttribute("Index", "Valid","", ErrorMessage = "{0}請輸入小於天日期")]
        [CheckDataAttribute(ErrorMessage = "{0}請輸入小於天日期")]
        public string AcountDate { get; set; }

        [DisplayName("備註")]
        [StringLength(100,ErrorMessage ="{0}字數請少於{1}")]
        [Required(ErrorMessage = "請輸入備註")]
        public string Mome { get; set; } 
    }

    //public class AccountBookListViewModel
    //{
    //    public List<AccountBookViewModel> AccountBookDataList { get; set; }
    //}


    public class AccountBookData
    {
        public List<AccountBookViewModel>  AccountBookDataList {get;set;}

        private static AccountBookData accountbookdata = null;

        public static AccountBookData GetInstance()
        {
            if(accountbookdata==null)
            {
                accountbookdata = new AccountBookData();
            }
            return accountbookdata;
        }


        private AccountBookData()
        {
            AccountBookDataList = new List<AccountBookViewModel>()
            {
                new AccountBookViewModel {iden=1,AccountType="1",Amount=10000,AcountDate="2017-01-01",Mome="" },
                new AccountBookViewModel {iden=2,AccountType="2",Amount=10000,AcountDate="2017-01-02",Mome="" },
                new AccountBookViewModel {iden=3,AccountType="1",Amount=10000,AcountDate="2017-01-03",Mome="" },
                new AccountBookViewModel {iden=4,AccountType="2",Amount=10000,AcountDate="2017-01-04",Mome="" },
                new AccountBookViewModel {iden=5,AccountType="1",Amount=10000,AcountDate="2017-01-05",Mome="" },
                new AccountBookViewModel {iden=6,AccountType="2",Amount=10000,AcountDate="2017-01-06",Mome="" },
                new AccountBookViewModel {iden=7,AccountType="1",Amount=10000,AcountDate="2017-01-07",Mome="" },
                new AccountBookViewModel {iden=8,AccountType="2",Amount=10000,AcountDate="2017-01-08",Mome="" },
                new AccountBookViewModel {iden=9,AccountType="1",Amount=10000,AcountDate="2017-01-09",Mome="" },

            };
        }



    }

}