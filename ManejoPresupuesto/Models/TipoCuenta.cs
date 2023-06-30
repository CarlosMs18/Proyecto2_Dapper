using ManejoPresupuesto.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta /*: IValidatableObject*/ //PARA HACER VALIDACIONES A NIVL DE OBJETO EL PROBLEMA D ESTO ES QUE NO SON RETULIZABLES
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:50, MinimumLength = 3, ErrorMessage = "La longitud del campo {0} debe de estar entr {2} y {1} caracteres")]
        [Display(Name ="Nombre del tipo cuenta")] //si tenemos un asp-for en cshtml con esto podemos sincronizar y salra ese valor en ellabel
        [PrimeraLetraMayuscula]
        [Remote(action : "VerificarExisteTipoCuenta",controller : "TiposCuentas")] //saldra un error si el campo es duplicado una vez que quitemos
                                                                               //el foco al campo que estamos colocando hace una peticion http y verifica 
        public string Nombre { get; set; }

        public int UsuarioId { get; set; }

        public int Orden { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //   if(Nombre != null  && Nombre.Length > 0)
        //    {
        //        var primeraLetra = Nombre[0].ToString();
        //        if(primeraLetra != primeraLetra.ToUpper())
        //        {
        //            yield return new ValidationResult("La primera letra debe de ser mayúscula"
        //                , new[] { nameof(Nombre)}); //El segundo parametro es el campo donde se le pondra elerror
        //        }
        //    }
        //}

        //Pruebas de otras validaciones por defecto
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        //[EmailAddress(ErrorMessage = "El campo debe de ser un correo electronico valido")]
        //public string Email { get ; set; }


        //[Range(minimum : 18 , maximum : 130,  ErrorMessage = "El valor debe estar entre {1} y {2}")]
        //public int Edad { get; set; }


        //[Url(ErrorMessage = "El campo debe de ser un valor valido")]
        //public string URL { get; set; }


        //[CreditCard(ErrorMessage = "La tarjeta de credito no es valida")]
        //[Display(Name ="Tarjeta de Crédito")]
        //public string TarjetaDeCredito { get; set; }
    }
}
