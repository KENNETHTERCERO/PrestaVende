namespace FerreteriaSolucion.FORMSINVENTARIO
{
    partial class frmMantenimientoSubCategoria
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
            this.btnExitMant = new System.Windows.Forms.Button();
            this.btnCrearCategoria = new System.Windows.Forms.Button();
            this.lblMantenimientoSubCategoria = new System.Windows.Forms.Label();
            this.panelControles = new System.Windows.Forms.Panel();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
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
            this.panelControles.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExitMant
            // 
            this.btnExitMant.Location = new System.Drawing.Point(35, 52);
            this.btnExitMant.Name = "btnExitMant";
            this.btnExitMant.Size = new System.Drawing.Size(179, 30);
            this.btnExitMant.TabIndex = 22;
            this.btnExitMant.Text = "Salir de mantenimiento";
            this.btnExitMant.UseVisualStyleBackColor = true;
            this.btnExitMant.Click += new System.EventHandler(this.btnExitMant_Click);
            // 
            // btnCrearCategoria
            // 
            this.btnCrearCategoria.Location = new System.Drawing.Point(584, 55);
            this.btnCrearCategoria.Name = "btnCrearCategoria";
            this.btnCrearCategoria.Size = new System.Drawing.Size(204, 30);
            this.btnCrearCategoria.TabIndex = 21;
            this.btnCrearCategoria.Text = "Nueva Sub categoria";
            this.btnCrearCategoria.UseVisualStyleBackColor = true;
            this.btnCrearCategoria.Click += new System.EventHandler(this.btnCrearCategoria_Click);
            // 
            // lblMantenimientoSubCategoria
            // 
            this.lblMantenimientoSubCategoria.AutoSize = true;
            this.lblMantenimientoSubCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMantenimientoSubCategoria.Location = new System.Drawing.Point(245, 9);
            this.lblMantenimientoSubCategoria.Name = "lblMantenimientoSubCategoria";
            this.lblMantenimientoSubCategoria.Size = new System.Drawing.Size(339, 26);
            this.lblMantenimientoSubCategoria.TabIndex = 19;
            this.lblMantenimientoSubCategoria.Text = "Mantenimiento Sub Categorias";
            // 
            // panelControles
            // 
            this.panelControles.Controls.Add(this.cmbCategoria);
            this.panelControles.Controls.Add(this.lblCategoria);
            this.panelControles.Controls.Add(this.btnCancelar);
            this.panelControles.Controls.Add(this.btnGuardar);
            this.panelControles.Controls.Add(this.cmbEstados);
            this.panelControles.Controls.Add(this.lblEstado);
            this.panelControles.Controls.Add(this.txtCategoria);
            this.panelControles.Controls.Add(this.lblSerie);
            this.panelControles.Controls.Add(this.lblIDCategoriaNumero);
            this.panelControles.Controls.Add(this.lblIdCategoria);
            this.panelControles.Location = new System.Drawing.Point(35, 88);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(748, 440);
            this.panelControles.TabIndex = 18;
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(307, 50);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(212, 21);
            this.cmbCategoria.TabIndex = 5;
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(212, 53);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(69, 13);
            this.lblCategoria.TabIndex = 2;
            this.lblCategoria.Text = "CATEGORIA";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(215, 150);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 26);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(307, 150);
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
            this.cmbEstados.Location = new System.Drawing.Point(307, 116);
            this.cmbEstados.Name = "cmbEstados";
            this.cmbEstados.Size = new System.Drawing.Size(212, 21);
            this.cmbEstados.TabIndex = 7;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(230, 119);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(51, 13);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "ESTADO";
            // 
            // txtCategoria
            // 
            this.txtCategoria.Location = new System.Drawing.Point(307, 84);
            this.txtCategoria.MaxLength = 25;
            this.txtCategoria.Name = "txtCategoria";
            this.txtCategoria.Size = new System.Drawing.Size(212, 20);
            this.txtCategoria.TabIndex = 6;
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(187, 84);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(94, 13);
            this.lblSerie.TabIndex = 3;
            this.lblSerie.Text = "SUB CATEGORIA";
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
            this.lblIdCategoria.Location = new System.Drawing.Point(173, 18);
            this.lblIdCategoria.Name = "lblIdCategoria";
            this.lblIdCategoria.Size = new System.Drawing.Size(108, 13);
            this.lblIdCategoria.TabIndex = 0;
            this.lblIdCategoria.Text = "ID SUB CATEGORIA";
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvCategorias);
            this.panelGrid.Location = new System.Drawing.Point(40, 88);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(748, 441);
            this.panelGrid.TabIndex = 20;
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
            // frmMantenimientoSubCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 562);
            this.Controls.Add(this.btnExitMant);
            this.Controls.Add(this.btnCrearCategoria);
            this.Controls.Add(this.lblMantenimientoSubCategoria);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.panelGrid);
            this.Name = "frmMantenimientoSubCategoria";
            this.Text = "frmMantenimientoSubCategoria";
            this.panelControles.ResumeLayout(false);
            this.panelControles.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExitMant;
        private System.Windows.Forms.Button btnCrearCategoria;
        private System.Windows.Forms.Label lblMantenimientoSubCategoria;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.Button btnCancelar;
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
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label lblCategoria;
    }
}