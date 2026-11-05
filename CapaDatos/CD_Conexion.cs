using Microsoft.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Conexion
    {
        private readonly SqlConnection conexion = new SqlConnection(
    "Server=ESME\\SQLEXPRESS;Database=TIENDA USUARIO;Integrated Security=true;TrustServerCertificate=true");

        public SqlConnection AbrirConexion()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
            return conexion;
        }
    }
}