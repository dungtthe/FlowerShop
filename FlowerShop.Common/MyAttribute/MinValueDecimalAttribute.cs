using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Common.MyAttribute
{
  
    public class MinValueDecimalAttribute : ValidationAttribute
    {
        private readonly long _minValue;
        private readonly string name;
        public MinValueDecimalAttribute(string name,long minValue)
        {
            _minValue = minValue;
            ErrorMessage = $"{name} phải lớn hơn {_minValue}.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Skip null values
            }

            if (value is decimal decimalValue && (long)decimalValue > _minValue)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
