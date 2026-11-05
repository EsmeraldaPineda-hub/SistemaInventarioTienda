using Microsoft.Data.SqlClient;

namespace CapaDatos
{
    public class EntidadUsuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = "";
    }

    public class CD_Usuario
    {
        private readonly CD_Conexion objConexion = new CD_Conexion();

        public EntidadUsuario? ValidarUsuario(string usuario, string claveHash)
        {
            try
            {
                var cn = objConexion.AbrirConexion();
                using (var cmd = new SqlCommand(
                    @"SELECT Id_Usuario, NombreUsuario
                      FROM Usuario
                      WHERE NombreUsuario = @u AND ClaveHash = @c", cn))
                {
                    cmd.Parameters.AddWithValue("@u", usuario);
                    cmd.Parameters.AddWithValue("@c", claveHash);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new EntidadUsuario
                            {
                                IdUsuario = dr.GetInt32(0),
                                NombreUsuario = dr.GetString(1)
                            };
                        }
                    }
                }
                return null;
            }
            finally
            {
                objConexion.CerrarConexion();
            }
        }
    }
}