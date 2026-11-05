using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmDepartamentos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvDepartamentos = new DataGridView();
            lblNombre = new Label();
            txtNombre = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDepartamentos).BeginInit();
            SuspendLayout();

            dgvDepartamentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDepartamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDepartamentos.Location = new Point(12, 12);
            dgvDepartamentos.Name = "dgvDepartamentos";
            dgvDepartamentos.Size = new Size(460, 220);
            dgvDepartamentos.TabIndex = 0;

            btnEditar.Location = new Point(12, 240);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 32);
            btnEditar.TabIndex = 1;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;

            btnEliminar.Location = new Point(122, 240);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 32);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;

            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(12, 296);
            lblNombre.Name = "lblNombre";
            lblNombre.Text = "Nombre";

            txtNombre.Location = new Point(80, 293);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(392, 23);
            txtNombre.TabIndex = 3;

            btnGuardar.Location = new Point(222, 330);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 36);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar.Location = new Point(352, 330);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 36);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 385);
            Controls.Add(dgvDepartamentos);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FrmDepartamentos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Departamentos";
            Load += FrmDepartamentos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDepartamentos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvDepartamentos;
        private Label lblNombre;
        private TextBox txtNombre;
        private Button btnGuardar;
        private Button btnCancelar;
        private Button btnEditar;
        private Button btnEliminar;
    }
}