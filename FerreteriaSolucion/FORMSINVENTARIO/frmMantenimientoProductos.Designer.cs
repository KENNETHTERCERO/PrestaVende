namespace FerreteriaSolucion.FORMSINVENTARIO
{
    partial class frmMantenimientoProductos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnExitMant = new System.Windows.Forms.Button();
            this.btnCrearProducto = new System.Windows.Forms.Button();
            this.lblMantenimientoSubCategoria = new System.Windows.Forms.Label();
            this.panelControles = new System.Windows.Forms.Panel();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.lblCodigoProducto = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.cmbSubCategoria = new System.Windows.Forms.ComboBox();
            this.lblSubCategoria = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cmbEstados = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtDescripcionProducto = new System.Windows.Forms.TextBox();
            this.lblProductoDescripcion = new System.Windows.Forms.Label();
            this.lblIDProductoNumero = new System.Windows.Forms.Label();
            this.lblIdProducto = new System.Windows.Forms.Label();
            this.txtPrecioCosto = new System.Windows.Forms.TextBox();
            this.lblPrecioCosto = new System.Windows.Forms.Label();
            this.txtPrecioUnitario = new System.Windows.Forms.TextBox();
            this.lblPrecioUnitario = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.panelControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvProductos);
            this.panelGrid.Location = new System.Drawing.Point(27, 91);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(934, 441);
            this.panelGrid.TabIndex = 25;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Edit});
            this.dgvProductos.Location = new System.Drawing.Point(3, 3);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.Size = new System.Drawing.Size(919, 478);
            this.dgvProductos.TabIndex = 0;
            this.dgvProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellContentClick);
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Editar";
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // btnExitMant
            // 
            this.btnExitMant.Location = new System.Drawing.Point(112, 44);
            this.btnExitMant.Name = "btnExitMant";
            this.btnExitMant.Size = new System.Drawing.Size(179, 30);
            this.btnExitMant.TabIndex = 12;
            this.btnExitMant.Text = "Salir de mantenimiento";
            this.btnExitMant.UseVisualStyleBackColor = true;
            this.btnExitMant.Click += new System.EventHandler(this.btnExitMant_Click);
            // 
            // btnCrearProducto
            // 
            this.btnCrearProducto.Location = new System.Drawing.Point(656, 44);
            this.btnCrearProducto.Name = "btnCrearProducto";
            this.btnCrearProducto.Size = new System.Drawing.Size(204, 30);
            this.btnCrearProducto.TabIndex = 13;
            this.btnCrearProducto.Text = "Nuevo producto";
            this.btnCrearProducto.UseVisualStyleBackColor = true;
            this.btnCrearProducto.Click += new System.EventHandler(this.btnCrearProducto_Click);
            // 
            // lblMantenimientoSubCategoria
            // 
            this.lblMantenimientoSubCategoria.AutoSize = true;
            this.lblMantenimientoSubCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMantenimientoSubCategoria.Location = new System.Drawing.Point(322, 9);
            this.lblMantenimientoSubCategoria.Name = "lblMantenimientoSubCategoria";
            this.lblMantenimientoSubCategoria.Size = new System.Drawing.Size(282, 26);
            this.lblMantenimientoSubCategoria.TabIndex = 24;
            this.lblMantenimientoSubCategoria.Text = "Mantenimiento Productos";
            // 
            // panelControles
            // 
            this.panelControles.Controls.Add(this.txtPrecioCosto);
            this.panelControles.Controls.Add(this.lblPrecioCosto);
            this.panelControles.Controls.Add(this.txtPrecioUnitario);
            this.panelControles.Controls.Add(this.lblPrecioUnitario);
            this.panelControles.Controls.Add(this.txtCantidad);
            this.panelControles.Controls.Add(this.lblCantidad);
            this.panelControles.Controls.Add(this.txtCodigoProducto);
            this.panelControles.Controls.Add(this.lblCodigoProducto);
            this.panelControles.Controls.Add(this.txtMarca);
            this.panelControles.Controls.Add(this.lblMarca);
            this.panelControles.Controls.Add(this.cmbSubCategoria);
            this.panelControles.Controls.Add(this.lblSubCategoria);
            this.panelControles.Controls.Add(this.btnCancelar);
            this.panelControles.Controls.Add(this.btnGuardar);
            this.panelControles.Controls.Add(this.cmbEstados);
            this.panelControles.Controls.Add(this.lblEstado);
            this.panelControles.Controls.Add(this.txtDescripcionProducto);
            this.panelControles.Controls.Add(this.lblProductoDescripcion);
            this.panelControles.Controls.Add(this.lblIDProductoNumero);
            this.panelControles.Controls.Add(this.lblIdProducto);
            this.panelControles.Location = new System.Drawing.Point(112, 80);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(748, 448);
            this.panelControles.TabIndex = 23;
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Location = new System.Drawing.Point(307, 146);
            this.txtCodigoProducto.MaxLength = 7;
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(212, 20);
            this.txtCodigoProducto.TabIndex = 8;
            // 
            // lblCodigoProducto
            // 
            this.lblCodigoProducto.AutoSize = true;
            this.lblCodigoProducto.Location = new System.Drawing.Point(168, 149);
            this.lblCodigoProducto.Name = "lblCodigoProducto";
            this.lblCodigoProducto.Size = new System.Drawing.Size(113, 13);
            this.lblCodigoProducto.TabIndex = 19;
            this.lblCodigoProducto.Text = "CODIGO PRODUCTO";
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(307, 115);
            this.txtMarca.MaxLength = 25;
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(212, 20);
            this.txtMarca.TabIndex = 7;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(236, 118);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(45, 13);
            this.lblMarca.TabIndex = 17;
            this.lblMarca.Text = "MARCA";
            // 
            // cmbSubCategoria
            // 
            this.cmbSubCategoria.FormattingEnabled = true;
            this.cmbSubCategoria.Location = new System.Drawing.Point(307, 50);
            this.cmbSubCategoria.Name = "cmbSubCategoria";
            this.cmbSubCategoria.Size = new System.Drawing.Size(212, 21);
            this.cmbSubCategoria.TabIndex = 5;
            // 
            // lblSubCategoria
            // 
            this.lblSubCategoria.AutoSize = true;
            this.lblSubCategoria.Location = new System.Drawing.Point(212, 53);
            this.lblSubCategoria.Name = "lblSubCategoria";
            this.lblSubCategoria.Size = new System.Drawing.Size(69, 13);
            this.lblSubCategoria.TabIndex = 2;
            this.lblSubCategoria.Text = "CATEGORIA";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(215, 306);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 26);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(307, 306);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(212, 26);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cmbEstados
            // 
            this.cmbEstados.FormattingEnabled = true;
            this.cmbEstados.Location = new System.Drawing.Point(307, 179);
            this.cmbEstados.Name = "cmbEstados";
            this.cmbEstados.Size = new System.Drawing.Size(212, 21);
            this.cmbEstados.TabIndex = 9;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(230, 182);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(51, 13);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "ESTADO";
            // 
            // txtDescripcionProducto
            // 
            this.txtDescripcionProducto.Location = new System.Drawing.Point(307, 84);
            this.txtDescripcionProducto.MaxLength = 50;
            this.txtDescripcionProducto.Name = "txtDescripcionProducto";
            this.txtDescripcionProducto.Size = new System.Drawing.Size(212, 20);
            this.txtDescripcionProducto.TabIndex = 6;
            // 
            // lblProductoDescripcion
            // 
            this.lblProductoDescripcion.AutoSize = true;
            this.lblProductoDescripcion.Location = new System.Drawing.Point(137, 87);
            this.lblProductoDescripcion.Name = "lblProductoDescripcion";
            this.lblProductoDescripcion.Size = new System.Drawing.Size(144, 13);
            this.lblProductoDescripcion.TabIndex = 3;
            this.lblProductoDescripcion.Text = "DESCRIPCION PRODUCTO";
            // 
            // lblIDProductoNumero
            // 
            this.lblIDProductoNumero.AutoSize = true;
            this.lblIDProductoNumero.Location = new System.Drawing.Point(315, 18);
            this.lblIDProductoNumero.Name = "lblIDProductoNumero";
            this.lblIDProductoNumero.Size = new System.Drawing.Size(13, 13);
            this.lblIDProductoNumero.TabIndex = 1;
            this.lblIDProductoNumero.Text = "0";
            // 
            // lblIdProducto
            // 
            this.lblIdProducto.AutoSize = true;
            this.lblIdProducto.Location = new System.Drawing.Point(173, 18);
            this.lblIdProducto.Name = "lblIdProducto";
            this.lblIdProducto.Size = new System.Drawing.Size(108, 13);
            this.lblIdProducto.TabIndex = 0;
            this.lblIdProducto.Text = "ID SUB CATEGORIA";
            // 
            // txtPrecioCosto
            // 
            this.txtPrecioCosto.Location = new System.Drawing.Point(307, 265);
            this.txtPrecioCosto.MaxLength = 25;
            this.txtPrecioCosto.Name = "txtPrecioCosto";
            this.txtPrecioCosto.Size = new System.Drawing.Size(212, 20);
            this.txtPrecioCosto.TabIndex = 24;
            // 
            // lblPrecioCosto
            // 
            this.lblPrecioCosto.AutoSize = true;
            this.lblPrecioCosto.Location = new System.Drawing.Point(194, 268);
            this.lblPrecioCosto.Name = "lblPrecioCosto";
            this.lblPrecioCosto.Size = new System.Drawing.Size(87, 13);
            this.lblPrecioCosto.TabIndex = 25;
            this.lblPrecioCosto.Text = "PRECIO COSTO";
            // 
            // txtPrecioUnitario
            // 
            this.txtPrecioUnitario.Location = new System.Drawing.Point(307, 236);
            this.txtPrecioUnitario.MaxLength = 25;
            this.txtPrecioUnitario.Name = "txtPrecioUnitario";
            this.txtPrecioUnitario.Size = new System.Drawing.Size(212, 20);
            this.txtPrecioUnitario.TabIndex = 22;
            // 
            // lblPrecioUnitario
            // 
            this.lblPrecioUnitario.AutoSize = true;
            this.lblPrecioUnitario.Location = new System.Drawing.Point(179, 239);
            this.lblPrecioUnitario.Name = "lblPrecioUnitario";
            this.lblPrecioUnitario.Size = new System.Drawing.Size(102, 13);
            this.lblPrecioUnitario.TabIndex = 23;
            this.lblPrecioUnitario.Text = "PRECIO UNITARIO";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(307, 208);
            this.txtCantidad.MaxLength = 50;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(212, 20);
            this.txtCantidad.TabIndex = 21;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(219, 211);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(62, 13);
            this.lblCantidad.TabIndex = 20;
            this.lblCantidad.Text = "CANTIDAD";
            // 
            // frmMantenimientoProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 711);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.btnExitMant);
            this.Controls.Add(this.btnCrearProducto);
            this.Controls.Add(this.lblMantenimientoSubCategoria);
            this.Name = "frmMantenimientoProductos";
            this.Text = "frmIngresoAInventario";
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.panelControles.ResumeLayout(false);
            this.panelControles.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.Button btnExitMant;
        private System.Windows.Forms.Button btnCrearProducto;
        private System.Windows.Forms.Label lblMantenimientoSubCategoria;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.ComboBox cmbSubCategoria;
        private System.Windows.Forms.Label lblSubCategoria;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox cmbEstados;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtDescripcionProducto;
        private System.Windows.Forms.Label lblProductoDescripcion;
        private System.Windows.Forms.Label lblIDProductoNumero;
        private System.Windows.Forms.Label lblIdProducto;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.Label lblCodigoProducto;
        private System.Windows.Forms.TextBox txtPrecioCosto;
        private System.Windows.Forms.Label lblPrecioCosto;
        private System.Windows.Forms.TextBox txtPrecioUnitario;
        private System.Windows.Forms.Label lblPrecioUnitario;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lblCantidad;
    }
}