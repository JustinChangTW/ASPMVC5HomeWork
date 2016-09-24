using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Session1.Expansion.Attribute
{
    public sealed class CheckDataAttribute : ValidationAttribute, IClientValidatable
    {
        //public string Input { get; set; }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                //ValidationType 的值一定要是小寫！
                ValidationType = "checkdata",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            //ValidationParameters 一定要是小寫！
            //rule.ValidationParameters["input"] = Input;
            yield return rule;
        }

        
        public override bool IsValid(object value)
        {
            //要不要輸入與此驗證無關
            if (value == null)  return true;

            

            //如果輸入的值是字串才做判斷
            if (value is string)
            {
                //輸入值與欄位值相同就報錯
                //if (Input.Contains(value.ToString()))
                bool isValidate = Convert.ToDateTime(value) <= DateTime.Now;//.ToString("yyyy-MM-dd") ;

                return false;
            }
            return base.IsValid(value);
        }
    }
}