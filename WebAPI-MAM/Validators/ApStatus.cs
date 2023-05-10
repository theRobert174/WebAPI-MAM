using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.Validators
{
    public class ApStatus : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /*if (string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult("Default Status set to 'Pendiente' ");//Desde aqui no se puede modificar el valor a uno por defecto
            }*/

            if(String.Equals(value.ToString(), "Pendiente" ) || String.Equals(value.ToString(), "pendiente" ))
            {
                return ValidationResult.Success;
            }

            if (String.Equals(value.ToString(), "Atendido") || String.Equals(value.ToString(), "atendido"))
            {
                return ValidationResult.Success;
            }

            if (String.Equals(value.ToString(), "NP") || String.Equals(value.ToString(), "np"))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Value is not valid");
        }
    }
}
