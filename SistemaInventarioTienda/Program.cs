using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                using (var login = new FrmLogin())
                {
                    // Si pulsa "Salir" o cierra el login, termina el programa
                    if (login.ShowDialog() != DialogResult.OK)
                        break;

                    using (var principal = new Form1(login.UsuarioActual))
                    {
                        Application.Run(principal);

                        // Si cerró con la X (no con "Cerrar sesión"), termina el programa
                        if (!principal.CerrarSesion)
                            break;
                    }
                }
            }
        }
    }
}