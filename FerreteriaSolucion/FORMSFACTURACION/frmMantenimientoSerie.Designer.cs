namespace FerreteriaSolucion.FORMSFACTURACION
{
    partial class frmMantenimientoSerie
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
            this.panelControles = new System.Windows.Forms.Panel();
            this.dtpFechaResolucion = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtResolucion = new System.Windows.Forms.TextBox();
            this.cmbEstados = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroDeFacturas = new System.Windows.Forms.TextBox();
            this.txtNumeroFinal = new System.Windows.Forms.TextBox();
            this.txtNumeroInicial = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.lblNumeroResolucion = new System.Windows.Forms.Label();
            this.lblNumeroDeFacturas = new System.Windows.Forms.Label();
            this.lblNumeroFinal = new System.Windows.Forms.Label();
            this.lblNumeroInicial = new System.Windows.Forms.Label();
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblIDSerieNumero = new System.Windows.Forms.Label();
            this.lblIdSerie = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvSeries = new System.Windows.Forms.DataGridView();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblMantenimientoSerie = new System.Windows.Forms.Label();
            this.btnCrearSerie = new System.Windows.Forms.Button();
            this.panelControles.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeries)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControles
            // 
            this.panelControles.Controls.Add(this.dtpFechaResolucion);
            this.panelControles.Controls.Add(this.label2);
            this.panelControles.Controls.Add(this.btnGuardar);
            this.panelControles.Controls.Add(this.txtResolucion);
            this.panelControles.Controls.Add(this.cmbEstados);
            this.panelControles.Controls.Add(this.label1);
            this.panelControles.Controls.Add(this.txtNumeroDeFacturas);
            this.panelControles.Controls.Add(this.txtNumeroFinal);
            this.panelControles.Controls.Add(this.txtNumeroInicial);
            this.panelControles.Controls.Add(this.txtSerie);
            this.panelControles.Controls.Add(this.lblNumeroResolucion);
            this.panelControles.Controls.Add(this.lblNumeroDeFacturas);
            this.panelControles.Controls.Add(this.lblNumeroFinal);
            this.panelControles.Controls.Add(this.lblNumeroInicial);
            this.panelControles.Controls.Add(this.lblSerie);
            this.panelControles.Controls.Add(this.lblIDSerieNumero);
            this.panelControles.Controls.Add(this.lblIdSerie);
            this.panelControles.Location = new System.Drawing.Point(12, 57);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(748, 440);
            this.panelControles.TabIndex = 0;
            // 
            // dtpFechaResolucion
            // 
            this.dtpFechaResolucion.Location = new System.Drawing.Point(309, 244);
            this.dtpFechaResolucion.Name = "dtpFechaResolucion";
            this.dtpFechaResolucion.Size = new System.Drawing.Size(210, 20);
            this.dtpFechaResolucion.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "RESOLUCION";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(307, 321);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(212, 26);
            this.btnGuardar.TabIndex = 16;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtResolucion
            // 
            this.txtResolucion.Location = new System.Drawing.Point(307, 210);
            this.txtResolucion.Name = "txtResolucion";
            this.txtResolucion.Size = new System.Drawing.Size(212, 20);
            this.txtResolucion.TabIndex = 11;
            // 
            // cmbEstados
            // 
            this.cmbEstados.FormattingEnabled = true;
            this.cmbEstados.Location = new System.Drawing.Point(307, 282);
            this.cmbEstados.Name = "cmbEstados";
            this.cmbEstados.Size = new System.Drawing.Size(212, 21);
            this.cmbEstados.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "ESTADO";
            // 
            // txtNumeroDeFacturas
            // 
            this.txtNumeroDeFacturas.Enabled = false;
            this.txtNumeroDeFacturas.Location = new System.Drawing.Point(307, 168);
            this.txtNumeroDeFacturas.Name = "txtNumeroDeFacturas";
            this.txtNumeroDeFacturas.Size = new System.Drawing.Size(212, 20);
            this.txtNumeroDeFacturas.TabIndex = 10;
            // 
            // txtNumeroFinal
            // 
            this.txtNumeroFinal.Location = new System.Drawing.Point(307, 129);
            this.txtNumeroFinal.MaxLength = 10;
            this.txtNumeroFinal.Name = "txtNumeroFinal";
            this.txtNumeroFinal.Size = new System.Drawing.Size(212, 20);
            this.txtNumeroFinal.TabIndex = 9;
            this.txtNumeroFinal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroFinal_KeyPress);
            this.txtNumeroFinal.Leave += new System.EventHandler(this.txtNumeroFinal_Leave);
            // 
            // txtNumeroInicial
            // 
            this.txtNumeroInicial.Location = new System.Drawing.Point(307, 90);
            this.txtNumeroInicial.MaxLength = 10;
            this.txtNumeroInicial.Name = "txtNumeroInicial";
            this.txtNumeroInicial.Size = new System.Drawing.Size(212, 20);
            this.txtNumeroInicial.TabIndex = 8;
            this.txtNumeroInicial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroInicial_KeyPress);
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(307, 52);
            this.txtSerie.MaxLength = 10;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(212, 20);
            this.txtSerie.TabIndex = 7;
            // 
            // lblNumeroResolucion
            // 
            this.lblNumeroResolucion.AutoSize = true;
            this.lblNumeroResolucion.Location = new System.Drawing.Point(204, 213);
            this.lblNumeroResolucion.Name = "lblNumeroResolucion";
            this.lblNumeroResolucion.Size = new System.Drawing.Size(77, 13);
            this.lblNumeroResolucion.TabIndex = 6;
            this.lblNumeroResolucion.Text = "RESOLUCION";
            // 
            // lblNumeroDeFacturas
            // 
            this.lblNumeroDeFacturas.AutoSize = true;
            this.lblNumeroDeFacturas.Location = new System.Drawing.Point(148, 171);
            this.lblNumeroDeFacturas.Name = "lblNumeroDeFacturas";
            this.lblNumeroDeFacturas.Size = new System.Drawing.Size(133, 13);
            this.lblNumeroDeFacturas.TabIndex = 5;
            this.lblNumeroDeFacturas.Text = "NUMERO DE FACTURAS";
            // 
            // lblNumeroFinal
            // 
            this.lblNumeroFinal.AutoSize = true;
            this.lblNumeroFinal.Location = new System.Drawing.Point(193, 132);
            this.lblNumeroFinal.Name = "lblNumeroFinal";
            this.lblNumeroFinal.Size = new System.Drawing.Size(88, 13);
            this.lblNumeroFinal.TabIndex = 4;
            this.lblNumeroFinal.Text = "NUMERO FINAL";
            // 
            // lblNumeroInicial
            // 
            this.lblNumeroInicial.AutoSize = true;
            this.lblNumeroInicial.Location = new System.Drawing.Point(186, 93);
            this.lblNumeroInicial.Name = "lblNumeroInicial";
            this.lblNumeroInicial.Size = new System.Drawing.Size(95, 13);
            this.lblNumeroInicial.TabIndex = 3;
            this.lblNumeroInicial.Text = "NUMERO INICIAL";
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(242, 55);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(39, 13);
            this.lblSerie.TabIndex = 2;
            this.lblSerie.Text = "SERIE";
            // 
            // lblIDSerieNumero
            // 
            this.lblIDSerieNumero.AutoSize = true;
            this.lblIDSerieNumero.Location = new System.Drawing.Point(315, 18);
            this.lblIDSerieNumero.Name = "lblIDSerieNumero";
            this.lblIDSerieNumero.Size = new System.Drawing.Size(13, 13);
            this.lblIDSerieNumero.TabIndex = 1;
            this.lblIDSerieNumero.Text = "0";
            // 
            // lblIdSerie
            // 
            this.lblIdSerie.AutoSize = true;
            this.lblIdSerie.Location = new System.Drawing.Point(242, 18);
            this.lblIdSerie.Name = "lblIdSerie";
            this.lblIdSerie.Size = new System.Drawing.Size(45, 13);
            this.lblIdSerie.TabIndex = 0;
            this.lblIdSerie.Text = "ID Serie";
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvSeries);
            this.panelGrid.Location = new System.Drawing.Point(17, 57);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(748, 441);
            this.panelGrid.TabIndex = 1;
            // 
            // dgvSeries
            // 
            this.dgvSeries.AllowUserToAddRows = false;
            this.dgvSeries.AllowUserToDeleteRows = false;
            this.dgvSeries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Edit});
            this.dgvSeries.Location = new System.Drawing.Point(3, 3);
            this.dgvSeries.Name = "dgvSeries";
            this.dgvSeries.ReadOnly = true;
            this.dgvSeries.Size = new System.Drawing.Size(737, 478);
            this.dgvSeries.TabIndex = 0;
            this.dgvSeries.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSeries_CellContentClick);
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Editar";
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // lblMantenimientoSerie
            // 
            this.lblMantenimientoSerie.AutoSize = true;
            this.lblMantenimientoSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMantenimientoSerie.Location = new System.Drawing.Point(264, 9);
            this.lblMantenimientoSerie.Name = "lblMantenimientoSerie";
            this.lblMantenimientoSerie.Size = new System.Drawing.Size(231, 26);
            this.lblMantenimientoSerie.TabIndex = 1;
            this.lblMantenimientoSerie.Text = "Mantenimiento Serie";
            // 
            // btnCrearSerie
            // 
            this.btnCrearSerie.Location = new System.Drawing.Point(525, 15);
            this.btnCrearSerie.Name = "btnCrearSerie";
            this.btnCrearSerie.Size = new System.Drawing.Size(204, 30);
            this.btnCrearSerie.TabIndex = 2;
            this.btnCrearSerie.Text = "Nueva serie";
            this.btnCrearSerie.UseVisualStyleBackColor = true;
            this.btnCrearSerie.Click += new System.EventHandler(this.btnCrearSerie_Click);
            // 
            // frmMantenimientoSerie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 509);
            this.Controls.Add(this.btnCrearSerie);
            this.Controls.Add(this.lblMantenimientoSerie);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.panelGrid);
            this.Name = "frmMantenimientoSerie";
            this.Text = "frmMantenimientoSerie";
            this.Load += new System.EventHandler(this.frmMantenimientoSerie_Load);
            this.panelControles.ResumeLayout(false);
            this.panelControles.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvSeries;
        private System.Windows.Forms.Label lblIdSerie;
        private System.Windows.Forms.Label lblMantenimientoSerie;
        private System.Windows.Forms.Label lblIDSerieNumero;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblNumeroInicial;
        private System.Windows.Forms.Label lblNumeroDeFacturas;
        private System.Windows.Forms.Label lblNumeroFinal;
        private System.Windows.Forms.Label lblNumeroResolucion;
        private System.Windows.Forms.TextBox txtNumeroDeFacturas;
        private System.Windows.Forms.TextBox txtNumeroFinal;
        private System.Windows.Forms.TextBox txtNumeroInicial;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.ComboBox cmbEstados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResolucion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaResolucion;
        private System.Windows.Forms.Button btnCrearSerie;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
    }
}