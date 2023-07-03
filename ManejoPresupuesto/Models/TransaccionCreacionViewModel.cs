using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TransaccionCreacionViewModel : Transaccion //hredamos de transaccion porque lo unico que queremos
                                                           //añadi son dos enumerables de selectlistitem
    {
        public IEnumerable<SelectListItem> Cuentas { get; set; }
        public IEnumerable<SelectListItem> Categorias { get; set; }

        //[Display(Name = "Tipo Operacion")]
   /*     public TipoOperacion TipoOperacionId { get; set; } = TipoOperacion.Ingreso;*/ //porque colocamos el tipooperacion si lo borramos este campo de la tabla transacciones
                                            //pues esto sera impotante porque a partir del tipo operacion es que determinare las categorias a mostrar
                                              //recordar que tenemos categorias que pertenecen al tipo INGRESO Y GASTOS

    }
}
