using Microsoft.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Catalogos
    {
        private readonly CD_Conexion conexion = new CD_Conexion();

        public DataTable Departamentos() => Consultar("sp_MostrarDepartamentos");
        public DataTable Proveedores() => Consultar("sp_MostrarProveedores");

        private DataTable Consultar(string procedimiento)
        {
            DataTable tabla = new DataTable();
            using (SqlCommand comando = new SqlCommand(procedimiento, conexion.AbrirConexion()))
            {
                comando.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader leer = comando.ExecuteReader())
                    tabla.Load(leer);
            }
            conexion.CerrarConexion();
            return tabla;
        }
    }
}