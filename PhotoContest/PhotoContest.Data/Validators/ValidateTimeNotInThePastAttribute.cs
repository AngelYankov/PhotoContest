using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotoContest.Data.Validators
{
    public class ValidateTimeNotInThePastAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var current = (DateTime)value;
            if (current >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Date time cannot be in the past.");
        }
    }
}
