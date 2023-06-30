namespace ManejoPresupuesto.Servicios
{
    public interface IServicioUsuarios
    {
        int ObtenerUsuarioId();
    }

    public class ServicioUsuarios : IServicioUsuarios
    {

        public int ObtenerUsuarioId() //para evitar la duplicacion de codigo y mejor la reutilziacionde codigo
        {
            return 1;
        }
    }
}
