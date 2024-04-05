namespace finance.Helper;


using System.ComponentModel.DataAnnotations;


    public class MatchPropertyAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public MatchPropertyAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

            var comparisonValue = propertyInfo.GetValue(validationContext.ObjectInstance);

            if (value != null && !value.Equals(comparisonValue))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
