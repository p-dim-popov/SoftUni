using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SULS
{
    public static class ValidationHelper
    {
        public static (bool result, string errorMessage) IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return (Validator.TryValidateObject(dto, validationContext, validationResult, true), validationResult.FirstOrDefault()?.ErrorMessage);
        }

        public const string RequiredErrorMessage = " is required";
        public const string MinLengthErrorMessage = " minimum length is ";
        public const string MaxLengthErrorMessage = " maximum length is ";
        public const string RangeErrorMessage = " range is ";
        public const string EmailErrorMessage = "Email not in correct format";
        public const string CompareErrorMessage = " does not match ";
        public const string RegexErrorMessage = " not in correct format";

    }
}