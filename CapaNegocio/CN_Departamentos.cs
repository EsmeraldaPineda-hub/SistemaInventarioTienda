using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class CN_Departamentos
    {
        private readonly CD_Departamentos objetoCD = new CD_Departamentos();

        public DataTable Mostrar() => objetoCD.Mostrar();

        public void Insertar(string nombre)
        {
            objetoCD.Insertar(ValidarNombre(nombre));
        }

        public void Editar(string id, string nombre)
        {
            objetoCD.Editar(Convert.ToInt32(id), ValidarNombre(nombre));
        }

        public void Eliminar(string id)
        {
            objetoCD.Eliminar(Convert.ToInt32(id));
        }

        private string ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre del departamento es obligatorio.");
            nombre = nombre.Trim();
            if (nombre.Length > 50)
                throw new Exception("El nombre no puede tener más de 50 caracteres.");
            return nombre;
        }
    }
}