using Microsoft.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Departamentos
    {
        private readonly CD_Conexion conexion = new CD_Conexion();

        public DataTable Mostrar()
        {
            DataTable tabla = new DataTable();
            using (SqlCommand comando = new SqlCommand("sp_MostrarDepartamentos", conexion.AbrirConexion()))
            {
                comando.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader leer = comando.ExecuteReader())
                    tabla.Load(leer);
            }
            conexion.CerrarConexion();
            return tabla;
        }

        public void Insertar(string nombre) => Ejecutar("sp_InsertarDepartamento", null, nombre);
        public void Editar(int id, string nombre) => Ejecutar("sp_EditarDepartamento", id, nombre);
        public void Eliminar(int id) => Ejecutar("sp_EliminarDepartamento", id, null);

        private void Ejecutar(string procedimiento, int? id, string? nombre)
        {
            using (SqlCommand comando = new SqlCommand(procedimiento, conexion.AbrirConexion()))
            {
                comando.CommandType = CommandType.StoredProcedure;
                if (id.HasValue) comando.Parameters.AddWithValue("@id", id.Value);
                if (nombre != null) comando.Parameters.AddWithValue("@nombre", nombre);
                try { comando.ExecuteNonQuery(); }
                finally { conexion.CerrarConexion(); }
            }
        }
    }
}