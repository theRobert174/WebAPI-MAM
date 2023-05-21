using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.Validators
{
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }


            if (value.ToString().Length < 5)
            {
                return new ValidationResult("La contraseña debe de tener 5 caracteres");
            }

            if (value.ToString().Length > 12)
            {
                return new ValidationResult("La contraseña no debe de sobrepasar 12 caracteres");
            }

            if (value != null && Char.IsDigit(value.ToString().First()))
            {
                return new ValidationResult("La contraseña no debe de empezar con un número");
            }
            return ValidationResult.Success;
        }

    }
}
