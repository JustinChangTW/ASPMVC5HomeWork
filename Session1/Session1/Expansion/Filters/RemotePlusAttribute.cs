using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ValidateSample.Filters
{
    public class RemotePlusAttribute : RemoteAttribute
    {
        public RemotePlusAttribute(string action, string controller, string area)
            : base(action, controller, area)
        {
            this.RouteData["area"] = area;
        }
    }
}