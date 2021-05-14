using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ludo_API.Validators
{
    public class IsColorAttribute : ValidationAttribute
    {
        private string _errorMessage;

        public IsColorAttribute(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && Regex.IsMatch((string)value, "#[0-9a-fA-F]{3}|[0-9a-fA-F]{6}"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(_errorMessage);
            }
        }

    }
}
