using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ValidateSample.Filters
{
    public class RemoteDoublePlusAttribute : RemoteAttribute
    {
        private string _action { get; set; }
        private string _controller { get; set; }
        public RemoteDoublePlusAttribute(string action, string controller, AreaReference areaReference)
            : base(action, controller)
        {
            this._action = action;
            this._controller = controller;
            if (areaReference == AreaReference.UseRoot)
            {
                this.RouteData["area"] = null;
            }
        }
        public RemoteDoublePlusAttribute(string action, string controller, string area)
            : base(action, controller, area)
        {
            this._action = action;
            this._controller = controller;
            this.RouteData["area"] = area;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var additionalFields = this.AdditionalFields.Split(',');
            var propValues = new List<object> { value };

            foreach (var additionalField in additionalFields)
            {
                var prop = validationContext.ObjectType.GetProperty(additionalField);
                if (prop != null)
                {
                    var propValue = prop.GetValue(validationContext.ObjectInstance, null);
                    propValues.Add(propValue);
                }
                else
                {
                    propValues.Add(null);
                }
            }

            var controllerType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(d => d.Name.ToLower() == (this._controller + "Controller").ToLower());

            if (controllerType == null) return null;

            var instance = Activator.CreateInstance(controllerType);

            var method = controllerType.GetMethod(this._action);

            if (method == null) return null;

            var response = (ActionResult)method.Invoke(instance, propValues.ToArray());

            if (!(response is JsonResult)) return null;

            var isAvailable = false;
            var json = (JsonResult)response;
            var jsonData = Convert.ToString(json.Data);

            bool.TryParse(jsonData, out isAvailable);

            return isAvailable ? null : new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}