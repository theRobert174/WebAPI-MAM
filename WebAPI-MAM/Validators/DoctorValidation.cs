//using AutoMapper.Configuration;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_MAM.Validators
{
    public class DoctorValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if( value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if (value.ToString().Equals("doctor") || value.ToString().Equals("paciente"))
            {
                return ValidationResult.Success;

            }
            else
            {
                return new ValidationResult("Hello, debes de ser alguien autorizado para entrar a este sistema");
            }
        }
    }
}
