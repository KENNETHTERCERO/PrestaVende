namespace FerreteriaSolucion.FORMSFACTURACION
{
    partial class frmFacturacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacturacion));
            this.panelSerieFactura = new System.Windows.Forms.Panel();
            this.lblFacturasRestantesNumero = new System.Windows.Forms.Label();
            this.lblFacturasRestantes = new System.Windows.Forms.Label();
            this.lblNoFacturaNumero = new System.Windows.Forms.Label();
            this.lblDescripcionNoFactura = new System.Windows.Forms.Label();
            this.cmbSerie = new System.Windows.Forms.ComboBox();
            this.lblSerie = new System.Windows.Forms.Label();
            this.panelBusquedaCliente = new System.Windows.Forms.Panel();
            this.lblNitSeleccionado = new System.Windows.Forms.Label();
            this.lblNitClienteSeleccionado = new System.Windows.Forms.Label();
            this.btnClienteNuevo = new System.Windows.Forms.Button();
            this.cmbBusquedaCliente = new System.Windows.Forms.ComboBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.lblDireccionSeleccionadoDescripcion = new System.Windows.Forms.Label();
            this.lblDireccionSeleccionado = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.lblBusqueda = new System.Windows.Forms.Label();
            this.lblIdClienteNumero = new System.Windows.Forms.Label();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.panelNuevoCliente = new System.Windows.Forms.Panel();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.lblNit = new System.Windows.Forms.Label();
            this.lblClienteNuevoNumero = new System.Windows.Forms.Label();
            this.lblIdClienteNuevo = new System.Windows.Forms.Label();
            this.cmbTipoCliente = new System.Windows.Forms.ComboBox();
            this.lblTipoCliente = new System.Windows.Forms.Label();
            this.btnCancelarCliente = new System.Windows.Forms.Button();
            this.btnGuardarCliente = new System.Windows.Forms.Button();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtSegundoApellido = new System.Windows.Forms.TextBox();
            this.lblSegundoApellido = new System.Windows.Forms.Label();
            this.txtPrimeroApellido = new System.Windows.Forms.TextBox();
            this.lblPrimerApellido = new System.Windows.Forms.Label();
            this.txtSegundoNombre = new System.Windows.Forms.TextBox();
            this.lblSegundoNombre = new System.Windows.Forms.Label();
            this.txtPrimeroNombre = new System.Windows.Forms.TextBox();
            this.lblPrimerNombre = new System.Windows.Forms.Label();
            this.panelDetalleFactura = new System.Windows.Forms.Panel();
            this.btnAplicarDescuento = new System.Windows.Forms.Button();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.chkDescuento = new System.Windows.Forms.CheckBox();
            this.btnGuardarFactura = new System.Windows.Forms.Button();
            this.cmbBusquedaPorDescripcion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.lblTotalFacturaNumero = new System.Windows.Forms.Label();
            this.lblTotalFactura = new System.Windows.Forms.Label();
            this.dgvDetalleFactura = new System.Windows.Forms.DataGridView();
            this.eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnAgregarDetalle = new System.Windows.Forms.Button();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.lblCodigoProducto = new System.Windows.Forms.Label();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.panelSerieFactura.SuspendLayout();
            this.panelBusquedaCliente.SuspendLayout();
            this.panelNuevoCliente.SuspendLayout();
            this.panelDetalleFactura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleFactura)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSerieFactura
            // 
            this.panelSerieFactura.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelSerieFactura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSerieFactura.Controls.Add(this.lblFacturasRestantesNumero);
            this.panelSerieFactura.Controls.Add(this.lblFacturasRestantes);
            this.panelSerieFactura.Controls.Add(this.lblNoFacturaNumero);
            this.panelSerieFactura.Controls.Add(this.lblDescripcionNoFactura);
            this.panelSerieFactura.Controls.Add(this.cmbSerie);
            this.panelSerieFactura.Controls.Add(this.lblSerie);
            this.panelSerieFactura.Location = new System.Drawing.Point(12, 4);
            this.panelSerieFactura.Name = "panelSerieFactura";
            this.panelSerieFactura.Size = new System.Drawing.Size(984, 47);
            this.panelSerieFactura.TabIndex = 0;
            // 
            // lblFacturasRestantesNumero
            // 
            this.lblFacturasRestantesNumero.AutoSize = true;
            this.lblFacturasRestantesNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacturasRestantesNumero.Location = new System.Drawing.Point(839, 13);
            this.lblFacturasRestantesNumero.Name = "lblFacturasRestantesNumero";
            this.lblFacturasRestantesNumero.Size = new System.Drawing.Size(17, 18);
            this.lblFacturasRestantesNumero.TabIndex = 5;
            this.lblFacturasRestantesNumero.Text = "0";
            // 
            // lblFacturasRestantes
            // 
            this.lblFacturasRestantes.AutoSize = true;
            this.lblFacturasRestantes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFacturasRestantes.Location = new System.Drawing.Point(635, 13);
            this.lblFacturasRestantes.Name = "lblFacturasRestantes";
            this.lblFacturasRestantes.Size = new System.Drawing.Size(198, 18);
            this.lblFacturasRestantes.TabIndex = 4;
            this.lblFacturasRestantes.Text = "FACTURAS RESTANTES";
            // 
            // lblNoFacturaNumero
            // 
            this.lblNoFacturaNumero.AutoSize = true;
            this.lblNoFacturaNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoFacturaNumero.Location = new System.Drawing.Point(514, 13);
            this.lblNoFacturaNumero.Name = "lblNoFacturaNumero";
            this.lblNoFacturaNumero.Size = new System.Drawing.Size(17, 18);
            this.lblNoFacturaNumero.TabIndex = 3;
            this.lblNoFacturaNumero.Text = "0";
            // 
            // lblDescripcionNoFactura
            // 
            this.lblDescripcionNoFactura.AutoSize = true;
            this.lblDescripcionNoFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcionNoFactura.Location = new System.Drawing.Point(397, 13);
            this.lblDescripcionNoFactura.Name = "lblDescripcionNoFactura";
            this.lblDescripcionNoFactura.Size = new System.Drawing.Size(111, 18);
            this.lblDescripcionNoFactura.TabIndex = 2;
            this.lblDescripcionNoFactura.Text = "No FACTURA";
            // 
            // cmbSerie
            // 
            this.cmbSerie.FormattingEnabled = true;
            this.cmbSerie.Location = new System.Drawing.Point(117, 11);
            this.cmbSerie.Name = "cmbSerie";
            this.cmbSerie.Size = new System.Drawing.Size(235, 21);
            this.cmbSerie.TabIndex = 1;
            this.cmbSerie.SelectedIndexChanged += new System.EventHandler(this.cmbSerie_SelectedIndexChanged);
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerie.Location = new System.Drawing.Point(54, 13);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(57, 18);
            this.lblSerie.TabIndex = 0;
            this.lblSerie.Text = "SERIE";
            // 
            // panelBusquedaCliente
            // 
            this.panelBusquedaCliente.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelBusquedaCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBusquedaCliente.Controls.Add(this.lblNitSeleccionado);
            this.panelBusquedaCliente.Controls.Add(this.lblNitClienteSeleccionado);
            this.panelBusquedaCliente.Controls.Add(this.btnClienteNuevo);
            this.panelBusquedaCliente.Controls.Add(this.cmbBusquedaCliente);
            this.panelBusquedaCliente.Controls.Add(this.btnEditar);
            this.panelBusquedaCliente.Controls.Add(this.lblDireccionSeleccionadoDescripcion);
            this.panelBusquedaCliente.Controls.Add(this.lblDireccionSeleccionado);
            this.panelBusquedaCliente.Controls.Add(this.txtBusqueda);
            this.panelBusquedaCliente.Controls.Add(this.lblBusqueda);
            this.panelBusquedaCliente.Controls.Add(this.lblIdClienteNumero);
            this.panelBusquedaCliente.Controls.Add(this.lblIdCliente);
            this.panelBusquedaCliente.Controls.Add(this.lblNombreCliente);
            this.panelBusquedaCliente.Controls.Add(this.lblCliente);
            this.panelBusquedaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelBusquedaCliente.Location = new System.Drawing.Point(12, 65);
            this.panelBusquedaCliente.Name = "panelBusquedaCliente";
            this.panelBusquedaCliente.Size = new System.Drawing.Size(984, 67);
            this.panelBusquedaCliente.TabIndex = 1;
            // 
            // lblNitSeleccionado
            // 
            this.lblNitSeleccionado.AutoSize = true;
            this.lblNitSeleccionado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNitSeleccionado.ForeColor = System.Drawing.Color.White;
            this.lblNitSeleccionado.Location = new System.Drawing.Point(69, 49);
            this.lblNitSeleccionado.Name = "lblNitSeleccionado";
            this.lblNitSeleccionado.Size = new System.Drawing.Size(14, 13);
            this.lblNitSeleccionado.TabIndex = 14;
            this.lblNitSeleccionado.Text = "0";
            // 
            // lblNitClienteSeleccionado
            // 
            this.lblNitClienteSeleccionado.AutoSize = true;
            this.lblNitClienteSeleccionado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNitClienteSeleccionado.ForeColor = System.Drawing.Color.White;
            this.lblNitClienteSeleccionado.Location = new System.Drawing.Point(35, 48);
            this.lblNitClienteSeleccionado.Name = "lblNitClienteSeleccionado";
            this.lblNitClienteSeleccionado.Size = new System.Drawing.Size(28, 13);
            this.lblNitClienteSeleccionado.TabIndex = 13;
            this.lblNitClienteSeleccionado.Text = "NIT";
            // 
            // btnClienteNuevo
            // 
            this.btnClienteNuevo.Location = new System.Drawing.Point(755, 39);
            this.btnClienteNuevo.Name = "btnClienteNuevo";
            this.btnClienteNuevo.Size = new System.Drawing.Size(122, 23);
            this.btnClienteNuevo.TabIndex = 13;
            this.btnClienteNuevo.Text = "NUEVO CLIENTE";
            this.btnClienteNuevo.UseVisualStyleBackColor = true;
            this.btnClienteNuevo.Click += new System.EventHandler(this.btnClienteNuevo_Click);
            // 
            // cmbBusquedaCliente
            // 
            this.cmbBusquedaCliente.FormattingEnabled = true;
            this.cmbBusquedaCliente.Location = new System.Drawing.Point(311, 3);
            this.cmbBusquedaCliente.Name = "cmbBusquedaCliente";
            this.cmbBusquedaCliente.Size = new System.Drawing.Size(658, 21);
            this.cmbBusquedaCliente.TabIndex = 12;
            this.cmbBusquedaCliente.SelectedIndexChanged += new System.EventHandler(this.cmbBusquedaCliente_SelectedIndexChanged);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(883, 39);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 23);
            this.btnEditar.TabIndex = 14;
            this.btnEditar.Text = "EDITAR";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // lblDireccionSeleccionadoDescripcion
            // 
            this.lblDireccionSeleccionadoDescripcion.AutoSize = true;
            this.lblDireccionSeleccionadoDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccionSeleccionadoDescripcion.ForeColor = System.Drawing.Color.White;
            this.lblDireccionSeleccionadoDescripcion.Location = new System.Drawing.Point(323, 48);
            this.lblDireccionSeleccionadoDescripcion.Name = "lblDireccionSeleccionadoDescripcion";
            this.lblDireccionSeleccionadoDescripcion.Size = new System.Drawing.Size(14, 13);
            this.lblDireccionSeleccionadoDescripcion.TabIndex = 9;
            this.lblDireccionSeleccionadoDescripcion.Text = "0";
            // 
            // lblDireccionSeleccionado
            // 
            this.lblDireccionSeleccionado.AutoSize = true;
            this.lblDireccionSeleccionado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccionSeleccionado.ForeColor = System.Drawing.Color.White;
            this.lblDireccionSeleccionado.Location = new System.Drawing.Point(236, 48);
            this.lblDireccionSeleccionado.Name = "lblDireccionSeleccionado";
            this.lblDireccionSeleccionado.Size = new System.Drawing.Size(75, 13);
            this.lblDireccionSeleccionado.TabIndex = 8;
            this.lblDireccionSeleccionado.Text = "DIRECCION";
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(93, 3);
            this.txtBusqueda.MaxLength = 75;
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(202, 20);
            this.txtBusqueda.TabIndex = 11;
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
            this.txtBusqueda.Leave += new System.EventHandler(this.txtBusqueda_Leave);
            // 
            // lblBusqueda
            // 
            this.lblBusqueda.AutoSize = true;
            this.lblBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusqueda.ForeColor = System.Drawing.Color.White;
            this.lblBusqueda.Location = new System.Drawing.Point(13, 6);
            this.lblBusqueda.Name = "lblBusqueda";
            this.lblBusqueda.Size = new System.Drawing.Size(75, 13);
            this.lblBusqueda.TabIndex = 6;
            this.lblBusqueda.Text = "BUSQUEDA";
            // 
            // lblIdClienteNumero
            // 
            this.lblIdClienteNumero.AutoSize = true;
            this.lblIdClienteNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdClienteNumero.ForeColor = System.Drawing.Color.White;
            this.lblIdClienteNumero.Location = new System.Drawing.Point(122, 30);
            this.lblIdClienteNumero.Name = "lblIdClienteNumero";
            this.lblIdClienteNumero.Size = new System.Drawing.Size(14, 13);
            this.lblIdClienteNumero.TabIndex = 5;
            this.lblIdClienteNumero.Text = "0";
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.AutoSize = true;
            this.lblIdCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdCliente.ForeColor = System.Drawing.Color.White;
            this.lblIdCliente.Location = new System.Drawing.Point(36, 30);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(76, 13);
            this.lblIdCliente.TabIndex = 4;
            this.lblIdCliente.Text = "ID CLIENTE";
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCliente.ForeColor = System.Drawing.Color.White;
            this.lblNombreCliente.Location = new System.Drawing.Point(236, 29);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(59, 13);
            this.lblNombreCliente.TabIndex = 3;
            this.lblNombreCliente.Text = "CLIENTE";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.ForeColor = System.Drawing.Color.White;
            this.lblCliente.Location = new System.Drawing.Point(162, 30);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(59, 13);
            this.lblCliente.TabIndex = 2;
            this.lblCliente.Text = "CLIENTE";
            // 
            // panelNuevoCliente
            // 
            this.panelNuevoCliente.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelNuevoCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNuevoCliente.Controls.Add(this.txtNit);
            this.panelNuevoCliente.Controls.Add(this.lblNit);
            this.panelNuevoCliente.Controls.Add(this.lblClienteNuevoNumero);
            this.panelNuevoCliente.Controls.Add(this.lblIdClienteNuevo);
            this.panelNuevoCliente.Controls.Add(this.cmbTipoCliente);
            this.panelNuevoCliente.Controls.Add(this.lblTipoCliente);
            this.panelNuevoCliente.Controls.Add(this.btnCancelarCliente);
            this.panelNuevoCliente.Controls.Add(this.btnGuardarCliente);
            this.panelNuevoCliente.Controls.Add(this.txtDireccion);
            this.panelNuevoCliente.Controls.Add(this.lblDireccion);
            this.panelNuevoCliente.Controls.Add(this.txtSegundoApellido);
            this.panelNuevoCliente.Controls.Add(this.lblSegundoApellido);
            this.panelNuevoCliente.Controls.Add(this.txtPrimeroApellido);
            this.panelNuevoCliente.Controls.Add(this.lblPrimerApellido);
            this.panelNuevoCliente.Controls.Add(this.txtSegundoNombre);
            this.panelNuevoCliente.Controls.Add(this.lblSegundoNombre);
            this.panelNuevoCliente.Controls.Add(this.txtPrimeroNombre);
            this.panelNuevoCliente.Controls.Add(this.lblPrimerNombre);
            this.panelNuevoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelNuevoCliente.Location = new System.Drawing.Point(12, 57);
            this.panelNuevoCliente.Name = "panelNuevoCliente";
            this.panelNuevoCliente.Size = new System.Drawing.Size(984, 108);
            this.panelNuevoCliente.TabIndex = 8;
            this.panelNuevoCliente.Visible = false;
            // 
            // txtNit
            // 
            this.txtNit.Location = new System.Drawing.Point(131, 3);
            this.txtNit.MaxLength = 13;
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(202, 20);
            this.txtNit.TabIndex = 2;
            this.txtNit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNit_KeyPress);
            this.txtNit.Leave += new System.EventHandler(this.txtNit_Leave);
            // 
            // lblNit
            // 
            this.lblNit.AutoSize = true;
            this.lblNit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNit.ForeColor = System.Drawing.Color.White;
            this.lblNit.Location = new System.Drawing.Point(51, 6);
            this.lblNit.Name = "lblNit";
            this.lblNit.Size = new System.Drawing.Size(28, 13);
            this.lblNit.TabIndex = 18;
            this.lblNit.Text = "NIT";
            // 
            // lblClienteNuevoNumero
            // 
            this.lblClienteNuevoNumero.AutoSize = true;
            this.lblClienteNuevoNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClienteNuevoNumero.ForeColor = System.Drawing.Color.White;
            this.lblClienteNuevoNumero.Location = new System.Drawing.Point(570, 7);
            this.lblClienteNuevoNumero.Name = "lblClienteNuevoNumero";
            this.lblClienteNuevoNumero.Size = new System.Drawing.Size(14, 13);
            this.lblClienteNuevoNumero.TabIndex = 17;
            this.lblClienteNuevoNumero.Text = "0";
            // 
            // lblIdClienteNuevo
            // 
            this.lblIdClienteNuevo.AutoSize = true;
            this.lblIdClienteNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdClienteNuevo.ForeColor = System.Drawing.Color.White;
            this.lblIdClienteNuevo.Location = new System.Drawing.Point(488, 7);
            this.lblIdClienteNuevo.Name = "lblIdClienteNuevo";
            this.lblIdClienteNuevo.Size = new System.Drawing.Size(76, 13);
            this.lblIdClienteNuevo.TabIndex = 16;
            this.lblIdClienteNuevo.Text = "ID CLIENTE";
            // 
            // cmbTipoCliente
            // 
            this.cmbTipoCliente.FormattingEnabled = true;
            this.cmbTipoCliente.Location = new System.Drawing.Point(468, 81);
            this.cmbTipoCliente.Name = "cmbTipoCliente";
            this.cmbTipoCliente.Size = new System.Drawing.Size(202, 21);
            this.cmbTipoCliente.TabIndex = 8;
            // 
            // lblTipoCliente
            // 
            this.lblTipoCliente.AutoSize = true;
            this.lblTipoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoCliente.ForeColor = System.Drawing.Color.White;
            this.lblTipoCliente.Location = new System.Drawing.Point(369, 85);
            this.lblTipoCliente.Name = "lblTipoCliente";
            this.lblTipoCliente.Size = new System.Drawing.Size(92, 13);
            this.lblTipoCliente.TabIndex = 14;
            this.lblTipoCliente.Text = "TIPO CLIENTE";
            // 
            // btnCancelarCliente
            // 
            this.btnCancelarCliente.Location = new System.Drawing.Point(836, 30);
            this.btnCancelarCliente.Name = "btnCancelarCliente";
            this.btnCancelarCliente.Size = new System.Drawing.Size(145, 50);
            this.btnCancelarCliente.TabIndex = 10;
            this.btnCancelarCliente.Text = "Cancelar";
            this.btnCancelarCliente.UseVisualStyleBackColor = true;
            this.btnCancelarCliente.Click += new System.EventHandler(this.btnCancelarCliente_Click);
            // 
            // btnGuardarCliente
            // 
            this.btnGuardarCliente.Location = new System.Drawing.Point(676, 30);
            this.btnGuardarCliente.Name = "btnGuardarCliente";
            this.btnGuardarCliente.Size = new System.Drawing.Size(145, 50);
            this.btnGuardarCliente.TabIndex = 9;
            this.btnGuardarCliente.Text = "Guardar Cliente";
            this.btnGuardarCliente.UseVisualStyleBackColor = true;
            this.btnGuardarCliente.Click += new System.EventHandler(this.btnGuardarCliente_Click);
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(130, 82);
            this.txtDireccion.MaxLength = 150;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(203, 20);
            this.txtDireccion.TabIndex = 7;
            this.txtDireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrimeroNombre_KeyPress);
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.ForeColor = System.Drawing.Color.White;
            this.lblDireccion.Location = new System.Drawing.Point(49, 85);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(75, 13);
            this.lblDireccion.TabIndex = 10;
            this.lblDireccion.Text = "DIRECCION";
            // 
            // txtSegundoApellido
            // 
            this.txtSegundoApellido.Location = new System.Drawing.Point(468, 56);
            this.txtSegundoApellido.MaxLength = 25;
            this.txtSegundoApellido.Name = "txtSegundoApellido";
            this.txtSegundoApellido.Size = new System.Drawing.Size(202, 20);
            this.txtSegundoApellido.TabIndex = 6;
            this.txtSegundoApellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrimeroNombre_KeyPress);
            // 
            // lblSegundoApellido
            // 
            this.lblSegundoApellido.AutoSize = true;
            this.lblSegundoApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSegundoApellido.ForeColor = System.Drawing.Color.White;
            this.lblSegundoApellido.Location = new System.Drawing.Point(332, 59);
            this.lblSegundoApellido.Name = "lblSegundoApellido";
            this.lblSegundoApellido.Size = new System.Drawing.Size(132, 13);
            this.lblSegundoApellido.TabIndex = 8;
            this.lblSegundoApellido.Text = "SEGUNDO APELLIDO";
            // 
            // txtPrimeroApellido
            // 
            this.txtPrimeroApellido.Location = new System.Drawing.Point(131, 56);
            this.txtPrimeroApellido.MaxLength = 25;
            this.txtPrimeroApellido.Name = "txtPrimeroApellido";
            this.txtPrimeroApellido.Size = new System.Drawing.Size(202, 20);
            this.txtPrimeroApellido.TabIndex = 5;
            this.txtPrimeroApellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrimeroApellido_KeyPress);
            // 
            // lblPrimerApellido
            // 
            this.lblPrimerApellido.AutoSize = true;
            this.lblPrimerApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimerApellido.ForeColor = System.Drawing.Color.White;
            this.lblPrimerApellido.Location = new System.Drawing.Point(6, 59);
            this.lblPrimerApellido.Name = "lblPrimerApellido";
            this.lblPrimerApellido.Size = new System.Drawing.Size(119, 13);
            this.lblPrimerApellido.TabIndex = 6;
            this.lblPrimerApellido.Text = "PRIMER APELLIDO";
            // 
            // txtSegundoNombre
            // 
            this.txtSegundoNombre.Location = new System.Drawing.Point(468, 30);
            this.txtSegundoNombre.MaxLength = 25;
            this.txtSegundoNombre.Name = "txtSegundoNombre";
            this.txtSegundoNombre.Size = new System.Drawing.Size(202, 20);
            this.txtSegundoNombre.TabIndex = 4;
            this.txtSegundoNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSegundoNombre_KeyPress);
            // 
            // lblSegundoNombre
            // 
            this.lblSegundoNombre.AutoSize = true;
            this.lblSegundoNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSegundoNombre.ForeColor = System.Drawing.Color.White;
            this.lblSegundoNombre.Location = new System.Drawing.Point(337, 33);
            this.lblSegundoNombre.Name = "lblSegundoNombre";
            this.lblSegundoNombre.Size = new System.Drawing.Size(125, 13);
            this.lblSegundoNombre.TabIndex = 4;
            this.lblSegundoNombre.Text = "SEGUNDO NOMBRE";
            // 
            // txtPrimeroNombre
            // 
            this.txtPrimeroNombre.Location = new System.Drawing.Point(131, 30);
            this.txtPrimeroNombre.MaxLength = 25;
            this.txtPrimeroNombre.Name = "txtPrimeroNombre";
            this.txtPrimeroNombre.Size = new System.Drawing.Size(202, 20);
            this.txtPrimeroNombre.TabIndex = 3;
            this.txtPrimeroNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrimeroNombre_KeyPress);
            // 
            // lblPrimerNombre
            // 
            this.lblPrimerNombre.AutoSize = true;
            this.lblPrimerNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimerNombre.ForeColor = System.Drawing.Color.White;
            this.lblPrimerNombre.Location = new System.Drawing.Point(13, 33);
            this.lblPrimerNombre.Name = "lblPrimerNombre";
            this.lblPrimerNombre.Size = new System.Drawing.Size(112, 13);
            this.lblPrimerNombre.TabIndex = 2;
            this.lblPrimerNombre.Text = "PRIMER NOMBRE";
            // 
            // panelDetalleFactura
            // 
            this.panelDetalleFactura.BackColor = System.Drawing.Color.LightGreen;
            this.panelDetalleFactura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalleFactura.Controls.Add(this.btnAplicarDescuento);
            this.panelDetalleFactura.Controls.Add(this.txtDescuento);
            this.panelDetalleFactura.Controls.Add(this.chkDescuento);
            this.panelDetalleFactura.Controls.Add(this.btnGuardarFactura);
            this.panelDetalleFactura.Controls.Add(this.cmbBusquedaPorDescripcion);
            this.panelDetalleFactura.Controls.Add(this.label1);
            this.panelDetalleFactura.Controls.Add(this.lblProducto);
            this.panelDetalleFactura.Controls.Add(this.lblTotalFacturaNumero);
            this.panelDetalleFactura.Controls.Add(this.lblTotalFactura);
            this.panelDetalleFactura.Controls.Add(this.dgvDetalleFactura);
            this.panelDetalleFactura.Controls.Add(this.btnAgregarDetalle);
            this.panelDetalleFactura.Controls.Add(this.lblCantidad);
            this.panelDetalleFactura.Controls.Add(this.txtCantidad);
            this.panelDetalleFactura.Controls.Add(this.lblCodigoProducto);
            this.panelDetalleFactura.Controls.Add(this.txtCodigoProducto);
            this.panelDetalleFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelDetalleFactura.Location = new System.Drawing.Point(12, 171);
            this.panelDetalleFactura.Name = "panelDetalleFactura";
            this.panelDetalleFactura.Size = new System.Drawing.Size(984, 556);
            this.panelDetalleFactura.TabIndex = 8;
            // 
            // btnAplicarDescuento
            // 
            this.btnAplicarDescuento.Location = new System.Drawing.Point(579, 467);
            this.btnAplicarDescuento.Name = "btnAplicarDescuento";
            this.btnAplicarDescuento.Size = new System.Drawing.Size(75, 35);
            this.btnAplicarDescuento.TabIndex = 22;
            this.btnAplicarDescuento.Text = "Aplicar descuento";
            this.btnAplicarDescuento.UseVisualStyleBackColor = true;
            this.btnAplicarDescuento.Visible = false;
            this.btnAplicarDescuento.Click += new System.EventHandler(this.btnAplicarDescuento_Click);
            // 
            // txtDescuento
            // 
            this.txtDescuento.Location = new System.Drawing.Point(468, 474);
            this.txtDescuento.MaxLength = 4;
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(105, 20);
            this.txtDescuento.TabIndex = 21;
            this.txtDescuento.Visible = false;
            this.txtDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescuento_KeyPress);
            // 
            // chkDescuento
            // 
            this.chkDescuento.AutoSize = true;
            this.chkDescuento.Location = new System.Drawing.Point(372, 476);
            this.chkDescuento.Name = "chkDescuento";
            this.chkDescuento.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkDescuento.Size = new System.Drawing.Size(87, 17);
            this.chkDescuento.TabIndex = 20;
            this.chkDescuento.Text = "Descuento";
            this.chkDescuento.UseVisualStyleBackColor = true;
            this.chkDescuento.Visible = false;
            // 
            // btnGuardarFactura
            // 
            this.btnGuardarFactura.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGuardarFactura.Location = new System.Drawing.Point(756, 465);
            this.btnGuardarFactura.Name = "btnGuardarFactura";
            this.btnGuardarFactura.Size = new System.Drawing.Size(214, 28);
            this.btnGuardarFactura.TabIndex = 23;
            this.btnGuardarFactura.Text = "GUARDAR FACTURA (F3)";
            this.btnGuardarFactura.UseVisualStyleBackColor = false;
            this.btnGuardarFactura.Click += new System.EventHandler(this.btnGuardarFactura_Click);
            // 
            // cmbBusquedaPorDescripcion
            // 
            this.cmbBusquedaPorDescripcion.FormattingEnabled = true;
            this.cmbBusquedaPorDescripcion.Location = new System.Drawing.Point(147, 49);
            this.cmbBusquedaPorDescripcion.Name = "cmbBusquedaPorDescripcion";
            this.cmbBusquedaPorDescripcion.Size = new System.Drawing.Size(523, 21);
            this.cmbBusquedaPorDescripcion.TabIndex = 16;
            this.cmbBusquedaPorDescripcion.SelectedIndexChanged += new System.EventHandler(this.cmbBusquedaPorDescripcion_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "CODIGO PRODUCTO";
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducto.Location = new System.Drawing.Point(283, 2);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(76, 13);
            this.lblProducto.TabIndex = 15;
            this.lblProducto.Text = "PRODUCTO";
            // 
            // lblTotalFacturaNumero
            // 
            this.lblTotalFacturaNumero.AutoSize = true;
            this.lblTotalFacturaNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFacturaNumero.Location = new System.Drawing.Point(281, 467);
            this.lblTotalFacturaNumero.Name = "lblTotalFacturaNumero";
            this.lblTotalFacturaNumero.Size = new System.Drawing.Size(25, 26);
            this.lblTotalFacturaNumero.TabIndex = 14;
            this.lblTotalFacturaNumero.Text = "0";
            // 
            // lblTotalFactura
            // 
            this.lblTotalFactura.AutoSize = true;
            this.lblTotalFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFactura.Location = new System.Drawing.Point(41, 467);
            this.lblTotalFactura.Name = "lblTotalFactura";
            this.lblTotalFactura.Size = new System.Drawing.Size(234, 26);
            this.lblTotalFactura.TabIndex = 13;
            this.lblTotalFactura.Text = "TOTAL FACTURA  Q";
            // 
            // dgvDetalleFactura
            // 
            this.dgvDetalleFactura.AllowUserToAddRows = false;
            this.dgvDetalleFactura.AllowUserToDeleteRows = false;
            this.dgvDetalleFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleFactura.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eliminar});
            this.dgvDetalleFactura.Location = new System.Drawing.Point(12, 78);
            this.dgvDetalleFactura.Name = "dgvDetalleFactura";
            this.dgvDetalleFactura.RowHeadersVisible = false;
            this.dgvDetalleFactura.Size = new System.Drawing.Size(957, 386);
            this.dgvDetalleFactura.TabIndex = 19;
            this.dgvDetalleFactura.Tag = "";
            this.dgvDetalleFactura.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleFactura_CellContentClick);
            this.dgvDetalleFactura.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleFactura_CellEndEdit);
            this.dgvDetalleFactura.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleFactura_CellEnter);
            this.dgvDetalleFactura.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetalleFactura_DataError_1);
            // 
            // eliminar
            // 
            this.eliminar.HeaderText = "Eliminar";
            this.eliminar.Name = "eliminar";
            this.eliminar.Text = "X";
            // 
            // btnAgregarDetalle
            // 
            this.btnAgregarDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnAgregarDetalle.Location = new System.Drawing.Point(811, 4);
            this.btnAgregarDetalle.Name = "btnAgregarDetalle";
            this.btnAgregarDetalle.Size = new System.Drawing.Size(159, 68);
            this.btnAgregarDetalle.TabIndex = 18;
            this.btnAgregarDetalle.Text = "AGREGAR";
            this.btnAgregarDetalle.UseVisualStyleBackColor = false;
            this.btnAgregarDetalle.Click += new System.EventHandler(this.btnAgregarDetalle_Click);
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Location = new System.Drawing.Point(700, 32);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(70, 13);
            this.lblCantidad.TabIndex = 10;
            this.lblCantidad.Text = "CANTIDAD";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Enabled = false;
            this.txtCantidad.Location = new System.Drawing.Point(677, 48);
            this.txtCantidad.MaxLength = 10;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(128, 20);
            this.txtCantidad.TabIndex = 17;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // lblCodigoProducto
            // 
            this.lblCodigoProducto.AutoSize = true;
            this.lblCodigoProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoProducto.Location = new System.Drawing.Point(13, 14);
            this.lblCodigoProducto.Name = "lblCodigoProducto";
            this.lblCodigoProducto.Size = new System.Drawing.Size(128, 13);
            this.lblCodigoProducto.TabIndex = 8;
            this.lblCodigoProducto.Text = "CODIGO PRODUCTO";
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.Location = new System.Drawing.Point(147, 11);
            this.txtCodigoProducto.MaxLength = 7;
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(128, 20);
            this.txtCodigoProducto.TabIndex = 15;
            this.txtCodigoProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoProducto_KeyPress);
            this.txtCodigoProducto.Leave += new System.EventHandler(this.txtCodigoProducto_Leave);
            // 
            // frmFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.panelSerieFactura);
            this.Controls.Add(this.panelDetalleFactura);
            this.Controls.Add(this.panelBusquedaCliente);
            this.Controls.Add(this.panelNuevoCliente);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.Name = "frmFacturacion";
            this.Text = "frmFacturacion";
            this.Load += new System.EventHandler(this.frmFacturacion_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmFacturacion_KeyPress);
            this.panelSerieFactura.ResumeLayout(false);
            this.panelSerieFactura.PerformLayout();
            this.panelBusquedaCliente.ResumeLayout(false);
            this.panelBusquedaCliente.PerformLayout();
            this.panelNuevoCliente.ResumeLayout(false);
            this.panelNuevoCliente.PerformLayout();
            this.panelDetalleFactura.ResumeLayout(false);
            this.panelDetalleFactura.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleFactura)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSerieFactura;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.ComboBox cmbSerie;
        private System.Windows.Forms.Label lblNoFacturaNumero;
        private System.Windows.Forms.Label lblDescripcionNoFactura;
        private System.Windows.Forms.Label lblFacturasRestantesNumero;
        private System.Windows.Forms.Label lblFacturasRestantes;
        private System.Windows.Forms.Panel panelBusquedaCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblNombreCliente;
        private System.Windows.Forms.Label lblIdClienteNumero;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Label lblBusqueda;
        private System.Windows.Forms.Panel panelNuevoCliente;
        private System.Windows.Forms.TextBox txtPrimeroNombre;
        private System.Windows.Forms.Label lblPrimerNombre;
        private System.Windows.Forms.TextBox txtSegundoNombre;
        private System.Windows.Forms.Label lblSegundoNombre;
        private System.Windows.Forms.TextBox txtSegundoApellido;
        private System.Windows.Forms.Label lblSegundoApellido;
        private System.Windows.Forms.TextBox txtPrimeroApellido;
        private System.Windows.Forms.Label lblPrimerApellido;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Button btnCancelarCliente;
        private System.Windows.Forms.Button btnGuardarCliente;
        private System.Windows.Forms.Panel panelDetalleFactura;
        private System.Windows.Forms.Label lblTipoCliente;
        private System.Windows.Forms.ComboBox cmbTipoCliente;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnAgregarDetalle;
        private System.Windows.Forms.DataGridView dgvDetalleFactura;
        private System.Windows.Forms.Label lblTotalFactura;
        private System.Windows.Forms.Label lblTotalFacturaNumero;
        private System.Windows.Forms.Label lblIdClienteNuevo;
        private System.Windows.Forms.Label lblClienteNuevoNumero;
        private System.Windows.Forms.Label lblDireccionSeleccionadoDescripcion;
        private System.Windows.Forms.Label lblDireccionSeleccionado;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.DataGridViewButtonColumn eliminar;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCodigoProducto;
        private System.Windows.Forms.ComboBox cmbBusquedaPorDescripcion;
        private System.Windows.Forms.Button btnGuardarFactura;
        private System.Windows.Forms.TextBox txtNit;
        private System.Windows.Forms.Label lblNit;
        private System.Windows.Forms.ComboBox cmbBusquedaCliente;
        private System.Windows.Forms.Button btnClienteNuevo;
        private System.Windows.Forms.CheckBox chkDescuento;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Button btnAplicarDescuento;
        private System.Windows.Forms.Label lblNitSeleccionado;
        private System.Windows.Forms.Label lblNitClienteSeleccionado;
    }
}