using Microsoft.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Proveedores
    {
        private readonly CD_Conexion conexion = new CD_Conexion();

        public DataTable Mostrar()
        {
            DataTable tabla = new DataTable();
            using (SqlCommand comando = new SqlCommand("sp_MostrarProveedores", conexion.AbrirConexion()))
            {
                comando.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader leer = comando.ExecuteReader())
                    tabla.Load(leer);
            }
            conexion.CerrarConexion();
            return tabla;
        }

        public void Insertar(string nombre, string? telefono)
        {
            using (SqlCommand comando = new SqlCommand("sp_InsertarProveedor", conexion.AbrirConexion()))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@telefono", (object?)telefono ?? DBNull.Value);
                try { comando.ExecuteNonQuery(); }
                finally { conexion.CerrarConexion(); }
            }
        }

        public void Editar(int id, string nombre, string? telefono)
        {
            using (SqlCommand comando = new SqlCommand("sp_EditarProveedor", conexion.AbrirConexion()))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@telefono", (object?)telefono ?? DBNull.Value);
                try { comando.ExecuteNonQuery(); }
                finally { conexion.CerrarConexion(); }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlCommand comando = new SqlCommand("sp_EliminarProveedor", conexion.AbrirConexion()))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                try { comando.ExecuteNonQuery(); }
                finally { conexion.CerrarConexion(); }
            }
        }
    }
}