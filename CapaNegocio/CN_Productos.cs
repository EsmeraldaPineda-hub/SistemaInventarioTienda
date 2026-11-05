using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class CN_Productos
    {
        private readonly CD_Productos objetoCD = new CD_Productos();

        public DataTable MostrarProd() => objetoCD.Mostrar();
        public DataTable ProductosBajoStock() => objetoCD.BajoStock();
        public DataTable ProductosPorVencer() => objetoCD.PorVencer();

        public void InsertarProd(string descripcion, object idDepartamento, object idProveedor,
            string unidadesCaja, string costo, string precio, bool tieneVencimiento,
            DateTime fechaVencimiento, string stock, string stockMinimo)
        {
            Guardar(0, descripcion, idDepartamento, idProveedor, unidadesCaja, costo, precio,
                    tieneVencimiento, fechaVencimiento, stock, stockMinimo);
        }

        public void EditarProd(string id, string descripcion, object idDepartamento, object idProveedor,
            string unidadesCaja, string costo, string precio, bool tieneVencimiento,
            DateTime fechaVencimiento, string stock, string stockMinimo)
        {
            Guardar(Convert.ToInt32(id), descripcion, idDepartamento, idProveedor, unidadesCaja, costo,
                    precio, tieneVencimiento, fechaVencimiento, stock, stockMinimo);
        }

        public void EliminarProd(string id)
        {
            objetoCD.Eliminar(Convert.ToInt32(id));
        }

        // id = 0 → insertar; id > 0 → editar
        private void Guardar(int id, string descripcion, object idDepartamento, object idProveedor,
            string unidadesCaja, string costo, string precio, bool tieneVencimiento,
            DateTime fechaVencimiento, string stock, string stockMinimo)
        {
            // ---- VALIDACIONES ----
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new Exception("La descripción del producto es obligatoria.");
            if (idDepartamento == null || idProveedor == null)
                throw new Exception("Seleccione un departamento y un proveedor.");
            if (!int.TryParse(unidadesCaja, out int uCaja) || uCaja <= 0)
                throw new Exception("Las unidades por caja deben ser un entero mayor que 0.");
            if (!decimal.TryParse(costo, out decimal costoUnidad) || costoUnidad < 0)
                throw new Exception("El costo debe ser un número mayor o igual a 0.");
            if (!decimal.TryParse(precio, out decimal precioVenta) || precioVenta < 0)
                throw new Exception("El precio de venta debe ser un número mayor o igual a 0.");
            if (precioVenta < costoUnidad)
                throw new Exception("El precio de venta no puede ser menor que el costo (vendería con pérdida).");
            if (!int.TryParse(stock, out int stockActual) || stockActual < 0)
                throw new Exception("El stock debe ser un entero mayor o igual a 0.");
            if (!int.TryParse(stockMinimo, out int stockMin) || stockMin < 0)
                throw new Exception("El stock mínimo debe ser un entero mayor o igual a 0.");

            DateTime? vence = tieneVencimiento ? fechaVencimiento : (DateTime?)null;

            // ---- LLAMADA A LA CAPA DE DATOS ----
            if (id == 0)
                objetoCD.Insertar(descripcion.Trim(), Convert.ToInt32(idDepartamento), Convert.ToInt32(idProveedor),
                                  uCaja, costoUnidad, precioVenta, vence, stockActual, stockMin);
            else
                objetoCD.Editar(id, descripcion.Trim(), Convert.ToInt32(idDepartamento), Convert.ToInt32(idProveedor),
                                uCaja, costoUnidad, precioVenta, vence, stockActual, stockMin);
        }
    }
}