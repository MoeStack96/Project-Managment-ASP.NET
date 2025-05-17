using System.ComponentModel.DataAnnotations;

namespace Project_management_system.Helper
{
    public class MustBeTrueAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool boolValue && boolValue;
        }
    }
}
