using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Kaizen.Validations
{
    public class NotNullOrEmptyCollectionAttribute : ValidationAttribute
    {
#nullable enable
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is ICollection collection)
            {
                return collection.Count != 0 ? ValidationResult.Success : new ValidationResult(ErrorMessage);
            }

            if (value is IEnumerable enumerable)
            {
                return enumerable.GetEnumerator().MoveNext()
                    ? ValidationResult.Success
                    : new ValidationResult(ErrorMessage);
            }

            return new ValidationResult(ErrorMessage);
        }
#nullable disable
    }
}
