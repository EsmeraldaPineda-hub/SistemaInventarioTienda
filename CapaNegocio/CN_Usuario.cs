using System.Security.Cryptography;
using System.Text;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private readonly CD_Usuario objDatos = new CD_Usuario();

        public EntidadUsuario? Login(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                return null;

            return objDatos.ValidarUsuario(usuario.Trim(), GenerarHash(clave));
        }

        private static string GenerarHash(string texto)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(texto));
            return Convert.ToHexString(bytes);   // mayúsculas, igual que en la base
        }
    }
}