using CapaDatos;
using System.Data;
using System.Text.RegularExpressions;

namespace CapaNegocio
{
    public class CN_Proveedores
    {
        private readonly CD_Proveedores objetoCD = new CD_Proveedores();

        public DataTable Mostrar() => objetoCD.Mostrar();

        public void Insertar(string nombre, string telefono)
        {
            Validar(ref nombre, telefono, out string? tel);
            objetoCD.Insertar(nombre, tel);
        }

        public void Editar(string id, string nombre, string telefono)
        {
            Validar(ref nombre, telefono, out string? tel);
            objetoCD.Editar(Convert.ToInt32(id), nombre, tel);
        }

        public void Eliminar(string id)
        {
            objetoCD.Eliminar(Convert.ToInt32(id));
        }

        private void Validar(ref string nombre, string telefono, out string? telefonoLimpio)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre del proveedor es obligatorio.");
            nombre = nombre.Trim();
            if (nombre.Length > 80)
                throw new Exception("El nombre no puede tener más de 80 caracteres.");

            if (string.IsNullOrWhiteSpace(telefono))
            {
                telefonoLimpio = null;   // el teléfono es opcional
                return;
            }

            telefono = telefono.Trim();
            if (telefono.Length > 15)
                throw new Exception("El teléfono no puede tener más de 15 caracteres.");
            if (!Regex.IsMatch(telefono, @"^[0-9\-\+\(\) ]+$"))
                throw new Exception("El teléfono solo puede tener números, espacios y los símbolos + - ( ).");
            telefonoLimpio = telefono;
        }
    }
}