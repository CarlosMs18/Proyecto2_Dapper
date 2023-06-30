using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Validaciones
{
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute //validaciones por atributo
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //En value tenemos el valor del campo que hemos colocado el atributo
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
            var primeraLetra = value.ToString()[0].ToString();
            if(primeraLetra != primeraLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe de ser mayúscula");
            }
            return ValidationResult.Success;//quiere decir que efectivamente la letra era mayuscula
        }
    }
}
