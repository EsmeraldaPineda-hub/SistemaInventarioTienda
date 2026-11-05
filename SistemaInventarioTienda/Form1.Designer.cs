using System.Drawing;
using System.Windows.Forms;


namespace CapaPresentacion
{
    partial class Form1
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
            dgvProductos = new DataGridView();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnBajoStock = new Button();
            btnPorVencer = new Button();
            btnTodos = new Button();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblDepartamento = new Label();
            cboDepartamento = new ComboBox();
            lblProveedor = new Label();
            cboProveedor = new ComboBox();
            lblUnidadesCaja = new Label();
            txtUnidadesCaja = new TextBox();
            lblCosto = new Label();
            txtCosto = new TextBox();
            lblPrecio = new Label();
            txtPrecio = new TextBox();
            chkVence = new CheckBox();
            dtpVencimiento = new DateTimePicker();
            lblStock = new Label();
            txtStock = new TextBox();
            lblStockMinimo = new Label();
            txtStockMinimo = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            btnDepartamentos = new Button();
            btnProveedores = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // dgvProductos
            // 
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(12, 12);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(931, 250);
            dgvProductos.TabIndex = 0;
            dgvProductos.DataBindingComplete += dgvProductos_DataBindingComplete;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(12, 272);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(100, 32);
            btnEditar.TabIndex = 1;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(122, 272);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(100, 32);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnBajoStock
            // 
            btnBajoStock.Location = new Point(232, 272);
            btnBajoStock.Name = "btnBajoStock";
            btnBajoStock.Size = new Size(100, 32);
            btnBajoStock.TabIndex = 3;
            btnBajoStock.Text = "Bajo stock";
            btnBajoStock.UseVisualStyleBackColor = true;
            btnBajoStock.Click += btnBajoStock_Click;
            // 
            // btnPorVencer
            // 
            btnPorVencer.Location = new Point(342, 272);
            btnPorVencer.Name = "btnPorVencer";
            btnPorVencer.Size = new Size(100, 32);
            btnPorVencer.TabIndex = 4;
            btnPorVencer.Text = "Por vencer";
            btnPorVencer.UseVisualStyleBackColor = true;
            btnPorVencer.Click += btnPorVencer_Click;
            // 
            // btnTodos
            // 
            btnTodos.Location = new Point(452, 272);
            btnTodos.Name = "btnTodos";
            btnTodos.Size = new Size(100, 32);
            btnTodos.TabIndex = 5;
            btnTodos.Text = "Ver todos";
            btnTodos.UseVisualStyleBackColor = true;
            btnTodos.Click += btnTodos_Click;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(12, 333);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(69, 15);
            lblDescripcion.TabIndex = 6;
            lblDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(170, 330);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(260, 23);
            txtDescripcion.TabIndex = 6;
            // 
            // lblDepartamento
            // 
            lblDepartamento.AutoSize = true;
            lblDepartamento.Location = new Point(12, 367);
            lblDepartamento.Name = "lblDepartamento";
            lblDepartamento.Size = new Size(83, 15);
            lblDepartamento.TabIndex = 7;
            lblDepartamento.Text = "Departamento";
            // 
            // cboDepartamento
            // 
            cboDepartamento.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDepartamento.Location = new Point(170, 364);
            cboDepartamento.Name = "cboDepartamento";
            cboDepartamento.Size = new Size(260, 23);
            cboDepartamento.TabIndex = 7;
            // 
            // lblProveedor
            // 
            lblProveedor.AutoSize = true;
            lblProveedor.Location = new Point(12, 401);
            lblProveedor.Name = "lblProveedor";
            lblProveedor.Size = new Size(61, 15);
            lblProveedor.TabIndex = 8;
            lblProveedor.Text = "Proveedor";
            // 
            // cboProveedor
            // 
            cboProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProveedor.Location = new Point(170, 398);
            cboProveedor.Name = "cboProveedor";
            cboProveedor.Size = new Size(260, 23);
            cboProveedor.TabIndex = 8;
            // 
            // lblUnidadesCaja
            // 
            lblUnidadesCaja.AutoSize = true;
            lblUnidadesCaja.Location = new Point(12, 435);
            lblUnidadesCaja.Name = "lblUnidadesCaja";
            lblUnidadesCaja.Size = new Size(101, 15);
            lblUnidadesCaja.TabIndex = 9;
            lblUnidadesCaja.Text = "Unidades por caja";
            // 
            // txtUnidadesCaja
            // 
            txtUnidadesCaja.Location = new Point(170, 432);
            txtUnidadesCaja.Name = "txtUnidadesCaja";
            txtUnidadesCaja.Size = new Size(260, 23);
            txtUnidadesCaja.TabIndex = 9;
            // 
            // lblCosto
            // 
            lblCosto.AutoSize = true;
            lblCosto.Location = new Point(12, 469);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(99, 15);
            lblCosto.TabIndex = 10;
            lblCosto.Text = "Costo por unidad";
            // 
            // txtCosto
            // 
            txtCosto.Location = new Point(170, 466);
            txtCosto.Name = "txtCosto";
            txtCosto.Size = new Size(260, 23);
            txtCosto.TabIndex = 10;
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(12, 503);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(88, 15);
            lblPrecio.TabIndex = 11;
            lblPrecio.Text = "Precio de venta";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(170, 500);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(260, 23);
            txtPrecio.TabIndex = 11;
            // 
            // chkVence
            // 
            chkVence.AutoSize = true;
            chkVence.Location = new Point(12, 536);
            chkVence.Name = "chkVence";
            chkVence.Size = new Size(134, 19);
            chkVence.TabIndex = 12;
            chkVence.Text = "¿Tiene vencimiento?";
            chkVence.UseVisualStyleBackColor = true;
            chkVence.CheckedChanged += chkVence_CheckedChanged;
            // 
            // dtpVencimiento
            // 
            dtpVencimiento.Enabled = false;
            dtpVencimiento.Format = DateTimePickerFormat.Short;
            dtpVencimiento.Location = new Point(170, 534);
            dtpVencimiento.Name = "dtpVencimiento";
            dtpVencimiento.Size = new Size(260, 23);
            dtpVencimiento.TabIndex = 13;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(12, 571);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(71, 15);
            lblStock.TabIndex = 14;
            lblStock.Text = "Stock actual";
            // 
            // txtStock
            // 
            txtStock.Location = new Point(170, 568);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(260, 23);
            txtStock.TabIndex = 14;
            // 
            // lblStockMinimo
            // 
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Location = new Point(12, 605);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(81, 15);
            lblStockMinimo.TabIndex = 15;
            lblStockMinimo.Text = "Stock mínimo";
            // 
            // txtStockMinimo
            // 
            txtStockMinimo.Location = new Point(170, 602);
            txtStockMinimo.Name = "txtStockMinimo";
            txtStockMinimo.Size = new Size(260, 23);
            txtStockMinimo.TabIndex = 15;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(480, 330);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(150, 45);
            btnGuardar.TabIndex = 16;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(480, 385);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(150, 45);
            btnCancelar.TabIndex = 17;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnDepartamentos
            // 
            btnDepartamentos.Location = new Point(636, 333);
            btnDepartamentos.Name = "btnDepartamentos";
            btnDepartamentos.Size = new Size(154, 42);
            btnDepartamentos.TabIndex = 18;
            btnDepartamentos.Text = "Departamentos";
            btnDepartamentos.UseVisualStyleBackColor = true;
            btnDepartamentos.Click += btnDepartamentos_Click;
            // 
            // btnProveedores
            // 
            btnProveedores.Location = new Point(796, 333);
            btnProveedores.Name = "btnProveedores";
            btnProveedores.Size = new Size(132, 42);
            btnProveedores.TabIndex = 19;
            btnProveedores.Text = "Proveedores";
            btnProveedores.UseVisualStyleBackColor = true;
            btnProveedores.Click += btnProveedores_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(953, 661);
            Controls.Add(btnProveedores);
            Controls.Add(btnDepartamentos);
            Controls.Add(dgvProductos);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(btnBajoStock);
            Controls.Add(btnPorVencer);
            Controls.Add(btnTodos);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(lblDepartamento);
            Controls.Add(cboDepartamento);
            Controls.Add(lblProveedor);
            Controls.Add(cboProveedor);
            Controls.Add(lblUnidadesCaja);
            Controls.Add(txtUnidadesCaja);
            Controls.Add(lblCosto);
            Controls.Add(txtCosto);
            Controls.Add(lblPrecio);
            Controls.Add(txtPrecio);
            Controls.Add(chkVence);
            Controls.Add(dtpVencimiento);
            Controls.Add(lblStock);
            Controls.Add(txtStock);
            Controls.Add(lblStockMinimo);
            Controls.Add(txtStockMinimo);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inventario de Tienda";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvProductos;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnBajoStock;
        private Button btnPorVencer;
        private Button btnTodos;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Label lblDepartamento;
        private ComboBox cboDepartamento;
        private Label lblProveedor;
        private ComboBox cboProveedor;
        private Label lblUnidadesCaja;
        private TextBox txtUnidadesCaja;
        private Label lblCosto;
        private TextBox txtCosto;
        private Label lblPrecio;
        private TextBox txtPrecio;
        private CheckBox chkVence;
        private DateTimePicker dtpVencimiento;
        private Label lblStock;
        private TextBox txtStock;
        private Label lblStockMinimo;
        private TextBox txtStockMinimo;
        private Button btnGuardar;
        private Button btnCancelar;
        private Button btnDepartamentos;
        private Button btnProveedores;
    }
}