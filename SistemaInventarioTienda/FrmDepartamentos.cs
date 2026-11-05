using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmDepartamentos : Form
    {
        CN_Departamentos objetoCN = new CN_Departamentos();
        private string? idDepartamento = null;
        private bool Editar = false;

        public FrmDepartamentos()
        {
            InitializeComponent();
        }

        private void FrmDepartamentos_Load(object sender, EventArgs e)
        {
            try
            {
                MostrarDepartamentos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los departamentos: " + ex.Message);
            }
        }

        private void MostrarDepartamentos()
        {
            dgvDepartamentos.DataSource = objetoCN.Mostrar();
            dgvDepartamentos.Columns["Id_Departamento"].Visible = false;
            dgvDepartamentos.ReadOnly = true;
            dgvDepartamentos.AllowUserToAddRows = false;
            dgvDepartamentos.MultiSelect = false;
            dgvDepartamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Editar)
                {
                    objetoCN.Insertar(txtNombre.Text);
                    MessageBox.Show("Departamento insertado correctamente");
                }
                else
                {
                    objetoCN.Editar(idDepartamento!, txtNombre.Text);
                    MessageBox.Show("Departamento editado correctamente");
                }
                MostrarDepartamentos();
                LimpiarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDepartamentos.SelectedRows.Count > 0)
            {
                Editar = true;
                idDepartamento = dgvDepartamentos.CurrentRow.Cells["Id_Departamento"].Value.ToString();
                txtNombre.Text = dgvDepartamentos.CurrentRow.Cells["Nombre"].Value.ToString();
                btnGuardar.Text = "Actualizar";
            }
            else
                MessageBox.Show("Seleccione una fila por favor");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDepartamentos.SelectedRows.Count > 0)
            {
                var respuesta = MessageBox.Show("¿Eliminar el departamento seleccionado?", "Confirmar",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta != DialogResult.Yes) return;

                try
                {
                    string id = dgvDepartamentos.CurrentRow.Cells["Id_Departamento"].Value.ToString()!;
                    objetoCN.Eliminar(id);
                    MessageBox.Show("Eliminado correctamente");
                    MostrarDepartamentos();
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
            Editar = false;
            idDepartamento = null;
            btnGuardar.Text = "Guardar";
        }
    }
}