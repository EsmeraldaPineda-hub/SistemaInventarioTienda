using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class CN_Catalogos
    {
        private readonly CD_Catalogos objetoCD = new CD_Catalogos();

        public DataTable Departamentos() => objetoCD.Departamentos();
        public DataTable Proveedores() => objetoCD.Proveedores();
    }
}