using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmProveedores : Form
    {
        CN_Proveedores objetoCN = new CN_Proveedores();
        private string? idProveedor = null;
        private bool Editar = false;

        public FrmProveedores()
        {
            InitializeComponent();
        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            try
            {
                MostrarProveedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los proveedores: " + ex.Message);
            }
        }

        private void MostrarProveedores()
        {
            dgvProveedores.DataSource = objetoCN.Mostrar();
            dgvProveedores.Columns["Id_Proveedor"].Visible = false;
            dgvProveedores.ReadOnly = true;
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.MultiSelect = false;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Editar)
                {
                    objetoCN.Insertar(txtNombre.Text, txtTelefono.Text);
                    MessageBox.Show("Proveedor insertado correctamente");
                }
                else
                {
                    objetoCN.Editar(idProveedor!, txtNombre.Text, txtTelefono.Text);
                    MessageBox.Show("Proveedor editado correctamente");
                }
                MostrarProveedores();
                LimpiarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count > 0)
            {
                Editar = true;
                DataGridViewRow fila = dgvProveedores.CurrentRow;
                idProveedor = fila.Cells["Id_Proveedor"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value == DBNull.Value
                    ? "" : fila.Cells["Telefono"].Value.ToString();
                btnGuardar.Text = "Actualizar";
            }
            else
                MessageBox.Show("Seleccione una fila por favor");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count > 0)
            {
                var respuesta = MessageBox.Show("¿Eliminar el proveedor seleccionado?", "Confirmar",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta != DialogResult.Yes) return;

                try
                {
                    string id = dgvProveedores.CurrentRow.Cells["Id_Proveedor"].Value.ToString()!;
                    objetoCN.Eliminar(id);
                    MessageBox.Show("Eliminado correctamente");
                    MostrarProveedores();
                    LimpiarForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Seleccione una fila por favor");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarForm();
        }

        private void LimpiarForm()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            Editar = false;
            idProveedor = null;
            btnGuardar.Text = "Guardar";
        }
    }
}