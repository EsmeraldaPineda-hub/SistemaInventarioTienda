using Microsoft.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Productos
    {
        private readonly CD_Conexion conexion = new CD_Conexion();

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

        public DataTable Mostrar() => Consultar("sp_MostrarProductos");
        public DataTable BajoStock() => Consultar("sp_ProductosBajoStock");
        public DataTable PorVencer() => Consultar("sp_ProductosPorVencer");

        public void Insertar(string descripcion, int idDepartamento, int idProveedor, int unidadesCaja,
                             decimal costo, decimal precio, DateTime? vencimiento, int stock, int stockMinimo)
        {
            Ejecutar("sp_InsertarProducto", null, descripcion, idDepartamento, idProveedor,
                     unidadesCaja, costo, precio, vencimiento, stock, stockMinimo);
        }

        public void Editar(int id, string descripcion, int idDepartamento, int idProveedor, int unidadesCaja,
                           decimal costo, decimal precio, DateTime? vencimiento, int stock, int stockMinimo)
        {
            Ejecutar("sp_EditarProducto", id, descripcion, idDepartamento, idProveedor,
                     unidadesCaja, costo, precio, vencimiento, stock, stockMinimo);
        }

        public void Eliminar(int id)
        {
            using (SqlCommand comando = new SqlCommand("sp_EliminarProducto", conexion.AbrirConexion()))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id", id);
                try { comando.ExecuteNonQuery(); }
                finally { conexion.CerrarConexion(); }
            }
        }

        private void Ejecutar(string procedimiento, int? id, string descripcion, int idDepartamento,
                              int idProveedor, int unidadesCaja, decimal costo, decimal precio,
                              DateTime? vencimiento, int stock, int stockMinimo)
        {
            using (SqlCommand comando = new SqlCommand(procedimiento, conexion.AbrirConexion()))
            {
                comando.CommandType = CommandType.StoredProcedure;
                if (id.HasValue) comando.Parameters.AddWithValue("@id", id.Value);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@idDepartamento", idDepartamento);
                comando.Parameters.AddWithValue("@idProveedor", idProveedor);
                comando.Parameters.AddWithValue("@unidadesPorCaja", unidadesCaja);
                comando.Parameters.AddWithValue("@costoUnidad", costo);
                comando.Parameters.AddWithValue("@precioVenta", precio);
                comando.Parameters.AddWithValue("@fechaVencimiento",
                    vencimiento.HasValue ? (object)vencimiento.Value.Date : DBNull.Value);
                comando.Parameters.AddWithValue("@stockActual", stock);
                comando.Parameters.AddWithValue("@stockMinimo", stockMinimo);
                try { comando.ExecuteNonQuery(); }
                finally { conexion.CerrarConexion(); }
            }
        }
    }
}