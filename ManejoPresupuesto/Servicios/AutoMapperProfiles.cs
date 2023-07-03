using AutoMapper;
using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Servicios
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Cuenta, CuentaCreacionViewModel>(); //generico de que tipo a que tipo de dato a que tipo de datos vamos a mapear, si nos fijamos en el controlador de cuentas controls  en el
                                                          //controler get de Editar es de cuenta hacia CuentaCreacionViewModel


            CreateMap<TransaccionActualizarViewModel, Transaccion>().ReverseMap(); //reverse map significa que vamos a llevar de transaccionAc a transacion y viceveersa, con el reversemap se hace esto en una solalinea

        }
    }
}
