﻿using System.Web;
using System.Web.Mvc;

namespace Session1_no_iden
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
