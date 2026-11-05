using CapaNegocio;
using System.Data;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        CN_Productos objetoCN = new CN_Productos();
        CN_Catalogos catalogosCN = new CN_Catalogos();
        private string? idProducto = null;
        private bool Editar = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                MostrarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message);
            }
        }

        // ---------- CARGA DE DATOS ----------
        private void CargarCombos()
        {
            cboDepartamento.DataSource = catalogosCN.Departamentos();
            cboDepartamento.DisplayMember = "Nombre";
            cboDepartamento.ValueMember = "Id_Departamento";

            cboProveedor.DataSource = catalogosCN.Proveedores();
            cboProveedor.DisplayMember = "Nombre";
            cboProveedor.ValueMember = "Id_Proveedor";
        }

        private void MostrarProductos()
        {
            dgvProductos.DataSource = objetoCN.MostrarProd();
            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            dgvProductos.Columns["Id_Producto"].Visible = false;
            dgvProductos.Columns["Id_Departamento"].Visible = false;
            dgvProductos.Columns["Id_Proveedor"].Visible = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.MultiSelect = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Pinta en rojo claro los productos con stock bajo (necesitan reposición)
        private void dgvProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                int stock = Convert.ToInt32(fila.Cells["StockActual"].Value);
                int minimo = Convert.ToInt32(fila.Cells["StockMinimo"].Value);
                if (stock <= minimo)
                    fila.DefaultCellStyle.BackColor = Color.MistyRose;
            }
        }

        // ---------- GUARDAR (INSERTAR / EDITAR) ----------
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Editar)
                {
                    objetoCN.InsertarProd(txtDescripcion.Text, cboDepartamento.SelectedValue,
                        cboProveedor.SelectedValue, txtUnidadesCaja.Text, txtCosto.Text, txtPrecio.Text,
                        chkVence.Checked, dtpVencimiento.Value, txtStock.Text, txtStockMinimo.Text);
                    MessageBox.Show("Producto insertado correctamente");
                }
                else
                {
                    objetoCN.EditarProd(idProducto!, txtDescripcion.Text, cboDepartamento.SelectedValue,
                        cboProveedor.SelectedValue, txtUnidadesCaja.Text, txtCosto.Text, txtPrecio.Text,
                        chkVence.Checked, dtpVencimiento.Value, txtStock.Text, txtStockMinimo.Text);
                    MessageBox.Show("Producto editado correctamente");
                }
                MostrarProductos();
                LimpiarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar: " + ex.Message);
            }
        }

        // ---------- EDITAR (carga la fila seleccionada en el formulario) ----------
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                DataGridViewRow fila = dgvProductos.CurrentRow;
                Editar = true;
                idProducto = fila.Cells["Id_Producto"].Value.ToString();
                txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
                cboDepartamento.SelectedValue = fila.Cells["Id_Departamento"].Value;
                cboProveedor.SelectedValue = fila.Cells["Id_Proveedor"].Value;
                txtUnidadesCaja.Text = fila.Cells["UnidadesPorCaja"].Value.ToString();
                txtCosto.Text = fila.Cells["CostoUnidad"].Value.ToString();
                txtPrecio.Text = fila.Cells["PrecioVenta"].Value.ToString();
                txtStock.Text = fila.Cells["StockActual"].Value.ToString();
                txtStockMinimo.Text = fila.Cells["StockMinimo"].Value.ToString();

                if (fila.Cells["FechaVencimiento"].Value == DBNull.Value)
                    chkVence.Checked = false;
                else
                {
                    chkVence.Checked = true;
                    dtpVencimiento.Value = Convert.ToDateTime(fila.Cells["FechaVencimiento"].Value);
                }
                btnGuardar.Text = "Actualizar";
            }
            else
                MessageBox.Show("Seleccione una fila por favor");
        }

        // ---------- ELIMINAR ----------
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                var respuesta = MessageBox.Show("¿Eliminar el producto seleccionado?", "Confirmar",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta != DialogResult.Yes) return;

                try
                {
                    string id = dgvProductos.CurrentRow.Cells["Id_Producto"].Value.ToString()!;
                    objetoCN.EliminarProd(id);
                    MessageBox.Show("Eliminado correctamente");
                    MostrarProductos();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se puede eliminar: el producto tiene compras o movimientos registrados.");
                }
            }
            else
                MessageBox.Show("Seleccione una fila por favor");
        }

        // ---------- FILTROS DE CONTROL DE INVENTARIO ----------
        private void btnBajoStock_Click(object sender, EventArgs e)
        {
            dgvProductos.DataSource = objetoCN.ProductosBajoStock();
            ConfigurarGrid();
        }

        private void btnPorVencer_Click(object sender, EventArgs e)
        {
            dgvProductos.DataSource = objetoCN.ProductosPorVencer();
            ConfigurarGrid();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            MostrarProductos();
        }

        // ---------- UTILIDADES ----------
        private void chkVence_CheckedChanged(object sender, EventArgs e)
        {
            dtpVencimiento.Enabled = chkVence.Checked;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarForm();
        }

        private void LimpiarForm()
        {
            txtDescripcion.Clear();
            txtUnidadesCaja.Clear();
            txtCosto.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtStockMinimo.Clear();
            chkVence.Checked = false;
            Editar = false;
            idProducto = null;
            btnGuardar.Text = "Guardar";
        }

        private void btnDepartamentos_Click(object sender, EventArgs e)
        {
            new FrmDepartamentos().ShowDialog();
            CargarCombos();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            new FrmProveedores().ShowDialog();
            CargarCombos();
        }
    }
}