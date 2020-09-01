private static bool IsValid(object dto)
{
    var validationContext = new ValidationContext(dto);
    var validationResult = new List<ValidationResult>();

    return Validator.TryValidateObject(dto, validationContext, validationResult, true);
}