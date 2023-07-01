using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Models
{
    public class CuentaCreacionViewModel: Cuenta //creacion de esta clase para poder usar un SELECT
    {

        public IEnumerable<SelectListItem> TiposCuentas { get; set; }//es una clase especial de .net core que nos permite crear select sencillos
    }
}
