using Base;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Base.WPF.Validation
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public bool IsRequired { get; set; }
        public int MinLength { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireNonLetterOrDigit { get; set; }

        public PasswordValidationAttribute()
        {
            IsRequired = true;
        }

        public override bool IsValid(object value)
        {            
            if (value == null)
                return !IsRequired;

            var str = value.ToString();
            if (str.IsEmpty())
                return !IsRequired;

            int nTotal = str.Length;

            if (nTotal < MinLength)
                return false;

            int nDigit = str.Count(c => char.IsDigit(c));

            if (RequireDigit && nDigit == 0)
                    return false;

            int nLower = str.Count(c => char.IsLower(c));

            if (RequireLowercase && nLower == 0)
                return false;

            int nUpper = str.Count(c => char.IsUpper(c));

            if (RequireUppercase && nUpper == 0)
                return false;


            if (RequireNonLetterOrDigit)
                if (nDigit + nUpper + nLower == nTotal)
                    return false;

            return true;
        }
    }
}
