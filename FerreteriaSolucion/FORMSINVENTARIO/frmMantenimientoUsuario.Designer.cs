namespace FerreteriaSolucion.FORMSINVENTARIO
{
    partial class frmMantenimientoUsuario
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
            this.dgvUsuario = new System.Windows.Forms.DataGridView();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnExitMant = new System.Windows.Forms.Button();
            this.btnCrearUsuario = new System.Windows.Forms.Button();
            this.lblMantenimientoUsuarios = new System.Windows.Forms.Label();
            this.panelControles = new System.Windows.Forms.Panel();
            this.txtPrimerApellido = new System.Windows.Forms.TextBox();
            this.lblPrimerApellido = new System.Windows.Forms.Label();
            this.txtPrimerNombre = new System.Windows.Forms.TextBox();
            this.lblPrimerNombre = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblContrasenia = new System.Windows.Forms.Label();
            this.cmbTipoAcceso = new System.Windows.Forms.ComboBox();
            this.lblTipoAcceso = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cmbEstados = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblIDUsuarioNumero = new System.Windows.Forms.Label();
            this.lblIdUsuario = new System.Windows.Forms.Label();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuario)).BeginInit();
            this.panelControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvUsuario);
            this.panelGrid.Location = new System.Drawing.Point(12, 100);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(829, 441);
            this.panelGrid.TabIndex = 25;
            // 
            // dgvUsuario
            // 
            this.dgvUsuario.AllowUserToAddRows = false;
            this.dgvUsuario.AllowUserToDeleteRows = false;
            this.dgvUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Edit});
            this.dgvUsuario.Location = new System.Drawing.Point(3, 3);
            this.dgvUsuario.Name = "dgvUsuario";
            this.dgvUsuario.ReadOnly = true;
            this.dgvUsuario.Size = new System.Drawing.Size(802, 434);
            this.dgvUsuario.TabIndex = 0;
            this.dgvUsuario.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuario_CellContentClick);
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
            this.btnExitMant.Location = new System.Drawing.Point(23, 64);
            this.btnExitMant.Name = "btnExitMant";
            this.btnExitMant.Size = new System.Drawing.Size(179, 30);
            this.btnExitMant.TabIndex = 13;
            this.btnExitMant.Text = "Salir de mantenimiento";
            this.btnExitMant.UseVisualStyleBackColor = true;
            this.btnExitMant.Click += new System.EventHandler(this.btnExitMant_Click);
            // 
            // btnCrearUsuario
            // 
            this.btnCrearUsuario.Location = new System.Drawing.Point(567, 64);
            this.btnCrearUsuario.Name = "btnCrearUsuario";
            this.btnCrearUsuario.Size = new System.Drawing.Size(204, 30);
            this.btnCrearUsuario.TabIndex = 14;
            this.btnCrearUsuario.Text = "Nuevo Usuario";
            this.btnCrearUsuario.UseVisualStyleBackColor = true;
            this.btnCrearUsuario.Click += new System.EventHandler(this.btnCrearUsuario_Click);
            // 
            // lblMantenimientoUsuarios
            // 
            this.lblMantenimientoUsuarios.AutoSize = true;
            this.lblMantenimientoUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMantenimientoUsuarios.Location = new System.Drawing.Point(251, 9);
            this.lblMantenimientoUsuarios.Name = "lblMantenimientoUsuarios";
            this.lblMantenimientoUsuarios.Size = new System.Drawing.Size(269, 26);
            this.lblMantenimientoUsuarios.TabIndex = 24;
            this.lblMantenimientoUsuarios.Text = "Mantenimiento Usuarios";
            // 
            // panelControles
            // 
            this.panelControles.Controls.Add(this.txtPrimerApellido);
            this.panelControles.Controls.Add(this.lblPrimerApellido);
            this.panelControles.Controls.Add(this.txtPrimerNombre);
            this.panelControles.Controls.Add(this.lblPrimerNombre);
            this.panelControles.Controls.Add(this.txtPassword);
            this.panelControles.Controls.Add(this.lblContrasenia);
            this.panelControles.Controls.Add(this.cmbTipoAcceso);
            this.panelControles.Controls.Add(this.lblTipoAcceso);
            this.panelControles.Controls.Add(this.btnCancelar);
            this.panelControles.Controls.Add(this.btnGuardar);
            this.panelControles.Controls.Add(this.cmbEstados);
            this.panelControles.Controls.Add(this.lblEstado);
            this.panelControles.Controls.Add(this.txtUsuario);
            this.panelControles.Controls.Add(this.lblUsuario);
            this.panelControles.Controls.Add(this.lblIDUsuarioNumero);
            this.panelControles.Controls.Add(this.lblIdUsuario);
            this.panelControles.Location = new System.Drawing.Point(23, 100);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(748, 440);
            this.panelControles.TabIndex = 23;
            // 
            // txtPrimerApellido
            // 
            this.txtPrimerApellido.Location = new System.Drawing.Point(307, 162);
            this.txtPrimerApellido.MaxLength = 25;
            this.txtPrimerApellido.Name = "txtPrimerApellido";
            this.txtPrimerApellido.Size = new System.Drawing.Size(212, 20);
            this.txtPrimerApellido.TabIndex = 8;
            // 
            // lblPrimerApellido
            // 
            this.lblPrimerApellido.AutoSize = true;
            this.lblPrimerApellido.Location = new System.Drawing.Point(177, 165);
            this.lblPrimerApellido.Name = "lblPrimerApellido";
            this.lblPrimerApellido.Size = new System.Drawing.Size(104, 13);
            this.lblPrimerApellido.TabIndex = 21;
            this.lblPrimerApellido.Text = "PRIMER APELLIDO";
            // 
            // txtPrimerNombre
            // 
            this.txtPrimerNombre.Location = new System.Drawing.Point(307, 136);
            this.txtPrimerNombre.MaxLength = 25;
            this.txtPrimerNombre.Name = "txtPrimerNombre";
            this.txtPrimerNombre.Size = new System.Drawing.Size(212, 20);
            this.txtPrimerNombre.TabIndex = 7;
            // 
            // lblPrimerNombre
            // 
            this.lblPrimerNombre.AutoSize = true;
            this.lblPrimerNombre.Location = new System.Drawing.Point(182, 139);
            this.lblPrimerNombre.Name = "lblPrimerNombre";
            this.lblPrimerNombre.Size = new System.Drawing.Size(99, 13);
            this.lblPrimerNombre.TabIndex = 19;
            this.lblPrimerNombre.Text = "PRIMER NOMBRE";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(307, 110);
            this.txtPassword.MaxLength = 25;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(212, 20);
            this.txtPassword.TabIndex = 6;
            // 
            // lblContrasenia
            // 
            this.lblContrasenia.AutoSize = true;
            this.lblContrasenia.Location = new System.Drawing.Point(211, 113);
            this.lblContrasenia.Name = "lblContrasenia";
            this.lblContrasenia.Size = new System.Drawing.Size(70, 13);
            this.lblContrasenia.TabIndex = 17;
            this.lblContrasenia.Text = "PASSWORD";
            // 
            // cmbTipoAcceso
            // 
            this.cmbTipoAcceso.FormattingEnabled = true;
            this.cmbTipoAcceso.Location = new System.Drawing.Point(307, 188);
            this.cmbTipoAcceso.Name = "cmbTipoAcceso";
            this.cmbTipoAcceso.Size = new System.Drawing.Size(212, 21);
            this.cmbTipoAcceso.TabIndex = 9;
            // 
            // lblTipoAcceso
            // 
            this.lblTipoAcceso.AutoSize = true;
            this.lblTipoAcceso.Location = new System.Drawing.Point(203, 191);
            this.lblTipoAcceso.Name = "lblTipoAcceso";
            this.lblTipoAcceso.Size = new System.Drawing.Size(78, 13);
            this.lblTipoAcceso.TabIndex = 2;
            this.lblTipoAcceso.Text = "TIPO ACCESO";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(215, 247);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 26);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(307, 247);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(212, 26);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cmbEstados
            // 
            this.cmbEstados.FormattingEnabled = true;
            this.cmbEstados.Location = new System.Drawing.Point(307, 215);
            this.cmbEstados.Name = "cmbEstados";
            this.cmbEstados.Size = new System.Drawing.Size(212, 21);
            this.cmbEstados.TabIndex = 10;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(230, 218);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(51, 13);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "ESTADO";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(307, 84);
            this.txtUsuario.MaxLength = 25;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(212, 20);
            this.txtUsuario.TabIndex = 5;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(225, 87);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(56, 13);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "USUARIO";
            // 
            // lblIDUsuarioNumero
            // 
            this.lblIDUsuarioNumero.AutoSize = true;
            this.lblIDUsuarioNumero.Location = new System.Drawing.Point(315, 18);
            this.lblIDUsuarioNumero.Name = "lblIDUsuarioNumero";
            this.lblIDUsuarioNumero.Size = new System.Drawing.Size(13, 13);
            this.lblIDUsuarioNumero.TabIndex = 1;
            this.lblIDUsuarioNumero.Text = "0";
            // 
            // lblIdUsuario
            // 
            this.lblIdUsuario.AutoSize = true;
            this.lblIdUsuario.Location = new System.Drawing.Point(211, 18);
            this.lblIdUsuario.Name = "lblIdUsuario";
            this.lblIdUsuario.Size = new System.Drawing.Size(70, 13);
            this.lblIdUsuario.TabIndex = 0;
            this.lblIdUsuario.Text = "ID USUARIO";
            // 
            // frmMantenimientoUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 563);
            this.Controls.Add(this.btnExitMant);
            this.Controls.Add(this.btnCrearUsuario);
            this.Controls.Add(this.lblMantenimientoUsuarios);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.panelGrid);
            this.Name = "frmMantenimientoUsuario";
            this.Text = "frmMantenimientoUsuario";
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuario)).EndInit();
            this.panelControles.ResumeLayout(false);
            this.panelControles.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvUsuario;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.Button btnExitMant;
        private System.Windows.Forms.Button btnCrearUsuario;
        private System.Windows.Forms.Label lblMantenimientoUsuarios;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.ComboBox cmbTipoAcceso;
        private System.Windows.Forms.Label lblTipoAcceso;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox cmbEstados;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblIDUsuarioNumero;
        private System.Windows.Forms.Label lblIdUsuario;
        private System.Windows.Forms.TextBox txtPrimerApellido;
        private System.Windows.Forms.Label lblPrimerApellido;
        private System.Windows.Forms.TextBox txtPrimerNombre;
        private System.Windows.Forms.Label lblPrimerNombre;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblContrasenia;
    }
}