namespace FerreteriaSolucion.FORMSINVENTARIO
{
    partial class frmIngresoAInventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIngresoAInventario));
            this.panelControles = new System.Windows.Forms.Panel();
            this.txtPrecioUnitario = new System.Windows.Forms.TextBox();
            this.lblPrecioUnitario = new System.Windows.Forms.Label();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.btnExitMant = new System.Windows.Forms.Button();
            this.btnAgregarAInventario = new System.Windows.Forms.Button();
            this.lblMantenimientoSubCategoria = new System.Windows.Forms.Label();
            this.txtPrecioCosto = new System.Windows.Forms.TextBox();
            this.lblPrecioCosto = new System.Windows.Forms.Label();
            this.panelControles.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControles
            // 
            this.panelControles.Controls.Add(this.txtPrecioCosto);
            this.panelControles.Controls.Add(this.lblPrecioCosto);
            this.panelControles.Controls.Add(this.txtPrecioUnitario);
            this.panelControles.Controls.Add(this.lblPrecioUnitario);
            this.panelControles.Controls.Add(this.cmbProducto);
            this.panelControles.Controls.Add(this.lblProducto);
            this.panelControles.Controls.Add(this.btnCancelar);
            this.panelControles.Controls.Add(this.btnGuardar);
            this.panelControles.Controls.Add(this.txtCantidad);
            this.panelControles.Controls.Add(this.lblCantidad);
            this.panelControles.Location = new System.Drawing.Point(150, 101);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(748, 448);
            this.panelControles.TabIndex = 28;
            // 
            // txtPrecioUnitario
            // 
            this.txtPrecioUnitario.Location = new System.Drawing.Point(324, 122);
            this.txtPrecioUnitario.MaxLength = 25;
            this.txtPrecioUnitario.Name = "txtPrecioUnitario";
            this.txtPrecioUnitario.Size = new System.Drawing.Size(212, 20);
            this.txtPrecioUnitario.TabIndex = 7;
            // 
            // lblPrecioUnitario
            // 
            this.lblPrecioUnitario.AutoSize = true;
            this.lblPrecioUnitario.Location = new System.Drawing.Point(196, 125);
            this.lblPrecioUnitario.Name = "lblPrecioUnitario";
            this.lblPrecioUnitario.Size = new System.Drawing.Size(102, 13);
            this.lblPrecioUnitario.TabIndex = 17;
            this.lblPrecioUnitario.Text = "PRECIO UNITARIO";
            // 
            // cmbProducto
            // 
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(152, 56);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(576, 21);
            this.cmbProducto.TabIndex = 5;
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(58, 59);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(68, 13);
            this.lblProducto.TabIndex = 2;
            this.lblProducto.Text = "PRODUCTO";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(232, 184);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 26);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(324, 184);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(212, 26);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Agregar cantidad";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(324, 94);
            this.txtCantidad.MaxLength = 50;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(212, 20);
            this.txtCantidad.TabIndex = 6;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(236, 97);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(62, 13);
            this.lblCantidad.TabIndex = 3;
            this.lblCantidad.Text = "CANTIDAD";
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvProductos);
            this.panelGrid.Location = new System.Drawing.Point(65, 112);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(934, 441);
            this.panelGrid.TabIndex = 30;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(3, 3);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.Size = new System.Drawing.Size(919, 478);
            this.dgvProductos.TabIndex = 0;
            // 
            // btnExitMant
            // 
            this.btnExitMant.Location = new System.Drawing.Point(150, 65);
            this.btnExitMant.Name = "btnExitMant";
            this.btnExitMant.Size = new System.Drawing.Size(179, 30);
            this.btnExitMant.TabIndex = 26;
            this.btnExitMant.Text = "Salir de mantenimiento";
            this.btnExitMant.UseVisualStyleBackColor = true;
            this.btnExitMant.Click += new System.EventHandler(this.btnExitMant_Click);
            // 
            // btnAgregarAInventario
            // 
            this.btnAgregarAInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAgregarAInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarAInventario.Location = new System.Drawing.Point(694, 65);
            this.btnAgregarAInventario.Name = "btnAgregarAInventario";
            this.btnAgregarAInventario.Size = new System.Drawing.Size(204, 30);
            this.btnAgregarAInventario.TabIndex = 27;
            this.btnAgregarAInventario.Text = "Agregar a inventario";
            this.btnAgregarAInventario.UseVisualStyleBackColor = false;
            this.btnAgregarAInventario.Click += new System.EventHandler(this.btnAgregarAInventario_Click);
            // 
            // lblMantenimientoSubCategoria
            // 
            this.lblMantenimientoSubCategoria.AutoSize = true;
            this.lblMantenimientoSubCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMantenimientoSubCategoria.Location = new System.Drawing.Point(360, 30);
            this.lblMantenimientoSubCategoria.Name = "lblMantenimientoSubCategoria";
            this.lblMantenimientoSubCategoria.Size = new System.Drawing.Size(201, 26);
            this.lblMantenimientoSubCategoria.TabIndex = 29;
            this.lblMantenimientoSubCategoria.Text = "Manejo Inventario";
            // 
            // txtPrecioCosto
            // 
            this.txtPrecioCosto.Location = new System.Drawing.Point(324, 151);
            this.txtPrecioCosto.MaxLength = 25;
            this.txtPrecioCosto.Name = "txtPrecioCosto";
            this.txtPrecioCosto.Size = new System.Drawing.Size(212, 20);
            this.txtPrecioCosto.TabIndex = 18;
            // 
            // lblPrecioCosto
            // 
            this.lblPrecioCosto.AutoSize = true;
            this.lblPrecioCosto.Location = new System.Drawing.Point(211, 154);
            this.lblPrecioCosto.Name = "lblPrecioCosto";
            this.lblPrecioCosto.Size = new System.Drawing.Size(87, 13);
            this.lblPrecioCosto.TabIndex = 19;
            this.lblPrecioCosto.Text = "PRECIO COSTO";
            // 
            // frmIngresoAInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 582);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.btnExitMant);
            this.Controls.Add(this.btnAgregarAInventario);
            this.Controls.Add(this.lblMantenimientoSubCategoria);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIngresoAInventario";
            this.Text = "frmIngresoAInventario";
            this.panelControles.ResumeLayout(false);
            this.panelControles.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.TextBox txtPrecioUnitario;
        private System.Windows.Forms.Label lblPrecioUnitario;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnExitMant;
        private System.Windows.Forms.Button btnAgregarAInventario;
        private System.Windows.Forms.Label lblMantenimientoSubCategoria;
        private System.Windows.Forms.TextBox txtPrecioCosto;
        private System.Windows.Forms.Label lblPrecioCosto;
    }
}