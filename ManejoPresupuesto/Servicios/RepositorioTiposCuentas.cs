using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Borrar(int id);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados);
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
                                               //(@"INSERT INTO TiposCuenta(Nombre, UsuarioId, Orden)
                                               //VALUES (@Nombre, @UsuarioId,0);
                                               // SELECT SCOPE_IDENTITY();",tipoCuenta);
                                               ("TiposCuentas_Insertar",
                                               new { usuarioId = tipoCuenta.UsuarioId,
                                               nombre = tipoCuenta.Nombre },
                                               commandType : System.Data.CommandType.StoredProcedure //LE DECIMOS QUE LO QUE QUIEREMOS EJECUTAR ES UN PROCESO ALMACENADO
                                               

                                               /*, tipoCuenta*/);//si enviamos el modelo al storeprocesius nos dara un error porque para crear el store solor equiere uno parametros y no sabe diferenciar de los datos del modelo que son mas 
                                //para esto es bueno crear una clase anonima
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


        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection (connectionString);
            return await connection.QueryAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden
                                                        FROM TiposCuenta
                                                        WHERE UsuarioId = @UsuarioId
                                                        ORDER BY Orden", new { usuarioId });

            //QueryAsync permite realizar un select y trae un conjunto de resultado y me mapea ese conjunto de resultado del generico que le pongamso
        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE TiposCuenta
                                    SET Nombre = @Nombre
                                    WHERE Id = @Id", tipoCuenta); //execute nos permite realizar un query que no devuelve nada
        }

        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden
                                        FROM TiposCuenta
                                        WHERE Id= @Id AND UsuarioId = @UsuarioId",
                                        new {id, usuarioId}); //query apra que se carge el reguistro si es dueño de el mismo

        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE TiposCuenta\r\nWHERE \r\nId = @Id", new { id });
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados) //PASANDO ENUMERABLE DE TPO CUENTA
        {
            var query = "UPDATE TiposCuenta SET orden = @Orden WHERE Id = @Id;"; //Ejecuta un query por cada tipo cuenta que le pasemos
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tipoCuentasOrdenados);
        }
    }
}
