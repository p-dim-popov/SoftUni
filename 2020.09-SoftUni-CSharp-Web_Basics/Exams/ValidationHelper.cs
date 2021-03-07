public static class ValidationHelper
{
    public static (bool isSuccessful, string errorMessage) IsValid(this object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return (Validator.TryValidateObject(dto, validationContext, validationResult, true), validationResult.FirstOrDefault()?.ErrorMessage);
    }

    public const string RequiredErrorMessage = " is required";
    public const string MinLengthErrorMessage = " minimum length is ";
    public const string MaxLengthErrorMessage = " maximum length is ";
    public const string RangeErrorMessage = " range is between ";
    public const string EmailErrorMessage = "Email not in correct format";
    public const string CompareErrorMessage = " does not match ";
    public const string RegexErrorMessage = " not in correct format";

}