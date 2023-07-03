namespace ManejoPresupuesto.Models
{
    public class TransaccionActualizarViewModel : TransaccionCreacionViewModel //heredamos de aca porque esta clase de transaccionACAUALIZARVIEWMODEL
                                                                               //lo que hara ser agregar las dos propiedades nuevas 

    {
        public int cuentaAnteriorId { get; set; }  
        public decimal MontoAnterior { get; set; }  
    }
}
