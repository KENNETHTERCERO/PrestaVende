namespace FerreteriaSolucion.FORMSINVENTARIO
{
    partial class frmMantenimientoCategorias
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
            this.btnCrearCategoria = new System.Windows.Forms.Button();
            this.lblMantenimientoSerie = new System.Windows.Forms.Label();
            this.panelControles = new System.Windows.Forms.Panel();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cmbEstados = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtCategoria = new System.Windows.Forms.TextBox();
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblIDCategoriaNumero = new System.Windows.Forms.Label();
            this.lblIdCategoria = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvCategorias = new System.Windows.Forms.DataGridView();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelControles.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCrearCategoria
            // 
            this.btnCrearCategoria.Location = new System.Drawing.Point(591, 31);
            this.btnCrearCategoria.Name = "btnCrearCategoria";
            this.btnCrearCategoria.Size = new System.Drawing.Size(204, 30);
            this.btnCrearCategoria.TabIndex = 6;
            this.btnCrearCategoria.Text = "Nueva Categoria";
            this.btnCrearCategoria.UseVisualStyleBackColor = true;
            this.btnCrearCategoria.Click += new System.EventHandler(this.btnCrearCategoria_Click);
            // 
            // lblMantenimientoSerie
            // 
            this.lblMantenimientoSerie.AutoSize = true;
            this.lblMantenimientoSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMantenimientoSerie.Location = new System.Drawing.Point(271, 9);
            this.lblMantenimientoSerie.Name = "lblMantenimientoSerie";
            this.lblMantenimientoSerie.Size = new System.Drawing.Size(290, 26);
            this.lblMantenimientoSerie.TabIndex = 4;
            this.lblMantenimientoSerie.Text = "Mantenimiento Categorias";
            // 
            // panelControles
            // 
            this.panelControles.Controls.Add(this.btnCancelar);
            this.panelControles.Controls.Add(this.btnGuardar);
            this.panelControles.Controls.Add(this.cmbEstados);
            this.panelControles.Controls.Add(this.lblEstado);
            this.panelControles.Controls.Add(this.txtCategoria);
            this.panelControles.Controls.Add(this.lblSerie);
            this.panelControles.Controls.Add(this.lblIDCategoriaNumero);
            this.panelControles.Controls.Add(this.lblIdCategoria);
            this.panelControles.Location = new System.Drawing.Point(42, 64);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(748, 440);
            this.panelControles.TabIndex = 3;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(307, 102);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(212, 26);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cmbEstados
            // 
            this.cmbEstados.FormattingEnabled = true;
            this.cmbEstados.Location = new System.Drawing.Point(307, 75);
            this.cmbEstados.Name = "cmbEstados";
            this.cmbEstados.Size = new System.Drawing.Size(212, 21);
            this.cmbEstados.TabIndex = 14;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(230, 78);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(51, 13);
            this.lblEstado.TabIndex = 12;
            this.lblEstado.Text = "ESTADO";
            // 
            // txtCategoria
            // 
            this.txtCategoria.Location = new System.Drawing.Point(307, 49);
            this.txtCategoria.MaxLength = 25;
            this.txtCategoria.Name = "txtCategoria";
            this.txtCategoria.Size = new System.Drawing.Size(212, 20);
            this.txtCategoria.TabIndex = 7;
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(212, 52);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(69, 13);
            this.lblSerie.TabIndex = 2;
            this.lblSerie.Text = "CATEGORIA";
            // 
            // lblIDCategoriaNumero
            // 
            this.lblIDCategoriaNumero.AutoSize = true;
            this.lblIDCategoriaNumero.Location = new System.Drawing.Point(315, 18);
            this.lblIDCategoriaNumero.Name = "lblIDCategoriaNumero";
            this.lblIDCategoriaNumero.Size = new System.Drawing.Size(13, 13);
            this.lblIDCategoriaNumero.TabIndex = 1;
            this.lblIDCategoriaNumero.Text = "0";
            // 
            // lblIdCategoria
            // 
            this.lblIdCategoria.AutoSize = true;
            this.lblIdCategoria.Location = new System.Drawing.Point(242, 18);
            this.lblIdCategoria.Name = "lblIdCategoria";
            this.lblIdCategoria.Size = new System.Drawing.Size(66, 13);
            this.lblIdCategoria.TabIndex = 0;
            this.lblIdCategoria.Text = "ID Categoria";
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvCategorias);
            this.panelGrid.Location = new System.Drawing.Point(47, 64);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(748, 441);
            this.panelGrid.TabIndex = 5;
            // 
            // dgvCategorias
            // 
            this.dgvCategorias.AllowUserToAddRows = false;
            this.dgvCategorias.AllowUserToDeleteRows = false;
            this.dgvCategorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategorias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Edit});
            this.dgvCategorias.Location = new System.Drawing.Point(3, 3);
            this.dgvCategorias.Name = "dgvCategorias";
            this.dgvCategorias.ReadOnly = true;
            this.dgvCategorias.Size = new System.Drawing.Size(737, 478);
            this.dgvCategorias.TabIndex = 0;
            this.dgvCategorias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategorias_CellContentClick);
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Editar";
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(215, 102);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 26);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 30);
            this.button1.TabIndex = 17;
            this.button1.Text = "Salir de mantenimiento";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMantenimientoCategorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 596);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCrearCategoria);
            this.Controls.Add(this.lblMantenimientoSerie);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.panelGrid);
            this.Name = "frmMantenimientoCategorias";
            this.Text = "frmMantenimientoCategorias";
            this.panelControles.ResumeLayout(false);
            this.panelControles.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCrearCategoria;
        private System.Windows.Forms.Label lblMantenimientoSerie;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox cmbEstados;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtCategoria;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblIDCategoriaNumero;
        private System.Windows.Forms.Label lblIdCategoria;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button button1;
    }
}