using CapaDatos;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmLogin : Form
    {
        private readonly CN_Usuario objNegocio = new CN_Usuario();

        // El formulario principal lee esto después de un login correcto
        public EntidadUsuario? UsuarioActual { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            AcceptButton = btnIngresar;   // Enter = Ingresar
            CancelButton = btnSalir;      // Esc = Salir
        }

        private void btnIngresar_Click(object? sender, EventArgs e)
        {
            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MostrarError("Escribe tu usuario y contraseña.");
                return;
            }

            try
            {
                var usuario = objNegocio.Login(txtUsuario.Text, txtClave.Text);

                if (usuario == null)
                {
                    MostrarError("Usuario o contraseña incorrectos.");
                    txtClave.Clear();
                    txtClave.Focus();
                    return;
                }

                UsuarioActual = usuario;
                DialogResult = DialogResult.OK;   // cierra el login y deja entrar al sistema
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con la base de datos:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}