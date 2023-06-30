using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
    }

    public class RepositorioTiposCuentas  : IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //public void Crear(TipoCuenta tipoCuenta) antes de la programacion asincrona
        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>/*QuerySingle*/
                                               (@"INSERT INTO TiposCuenta(Nombre, UsuarioId, Orden)
                                               VALUES (@Nombre, @UsuarioId,0);
                                                SELECT SCOPE_IDENTITY();",tipoCuenta);
            //querysingle permite hacer un query que estoy seguro que me traera un solo resultado
            //ponemos ese querysingle porque queremos extraer despues de que insertimos el tipocuentas, queremos extraer el id
            //del registro creado
            //el scope identity es lo que realmente nos trae el id del registro creado

            tipoCuenta.Id = id;
        }

        public async Task<bool> Existe(string nombre, int usuarioId) //para ver que el mismo usuario no tenga dos veces el mismo tipo de operacion 
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                                        @"SELECT 1 FROM
                                                        TiposCuenta
                                                        WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId;",
                                                        new {nombre, usuarioId});
            //el primer valor o registro por defecto en caso no exista el registro
            //pero como un valor ppor defecto, hemos puesto en el generico que el valor por defecto devuelve un numero entero, el entero por
            //defecto es 0 , en resum esta sentencia seria traeme el primer valor que encontre y si no el valor por defecto que colocamos en el generico que es 0
            //si existe nos arrojara 1 y si no es 0, no traemos la data ni nada solo queremos saber si existe o no
            return existe == 1;
        }
    }
}
