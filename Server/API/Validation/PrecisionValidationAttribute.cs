using System.ComponentModel.DataAnnotations;

namespace API.Validation
{
    public class PrecisionValidationAttribute : ValidationAttribute
    {
        public PrecisionValidationAttribute()
        {
            ErrorMessage ??= "{0} must not have precision greater than 2 decimal places";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is decimal number)
            {
                var decimals = (number - Math.Truncate(number)) * 100;
                var isTooPrecise = decimals - Math.Truncate(decimals);

                if (isTooPrecise > 0)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }

                return ValidationResult.Success;
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}