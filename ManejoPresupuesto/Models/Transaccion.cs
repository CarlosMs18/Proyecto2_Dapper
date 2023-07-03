using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class Transaccion
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [Display(Name = "Fecha Transacción")]
        [DataType(DataType.Date)]
        /*[DataType(DataType.DateTime)]*/ //para poner la fecha sin lahora, indicandole de tipo fecha ponetemos DataType.DateTime si quieremos mostrar la hora tenems que inicialiar cond date.now y no con today
        public DateTime FechaTransaccion { get; set; } = DateTime.Today;  /*DateTime.Parse(DateTime.Now.ToString("g"));*/ //ponemos un valor por defecto

        public decimal Monto { get; set; }

        [Range(1, maximum: int.MaxValue , ErrorMessage = "Debe seleccionar una categoria")] /*El numero debe de ser entre 1 y el maximo entero*/
        [Display(Name = "Categoria")]

        public int CategoriaId { get; set; }


        [StringLength(maximumLength:1000, ErrorMessage = "La nota no puede pasar de {1} caracteres")]
        public string Nota { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar una cuenta")]
        [Display(Name = "Cuenta")]
        public int CuentaId { get; set; }

        //para mostrar el listado de las categorias corecto en losformularios
        [Display(Name = "Tipo Operacion")]
        public TipoOperacion TipoOperacionId { get; set; } = TipoOperacion.Ingreso; //porque colocamos el tipooperacion si lo borramos este campo de la tabla transacciones
                                                                                    //pues esto sera impotante porque a partir del tipo operacion es que determinare las categorias a mostrar
                                                                                    //recordar que tenemos categorias que pertenecen al tipo INGRESO Y GASTOS
    }
}
