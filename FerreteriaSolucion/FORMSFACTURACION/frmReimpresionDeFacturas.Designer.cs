namespace FerreteriaSolucion.FORMSFACTURACION
{
    partial class frmReimpresionDeFacturas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReimpresionDeFacturas));
            this.lblReimpresionDeFacturas = new System.Windows.Forms.Label();
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.cmbSerie = new System.Windows.Forms.ComboBox();
            this.txtNoFactura = new System.Windows.Forms.TextBox();
            this.btnImprimirFactura = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblReimpresionDeFacturas
            // 
            this.lblReimpresionDeFacturas.AutoSize = true;
            this.lblReimpresionDeFacturas.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReimpresionDeFacturas.Location = new System.Drawing.Point(191, 64);
            this.lblReimpresionDeFacturas.Name = "lblReimpresionDeFacturas";
            this.lblReimpresionDeFacturas.Size = new System.Drawing.Size(264, 26);
            this.lblReimpresionDeFacturas.TabIndex = 0;
            this.lblReimpresionDeFacturas.Text = "Reimprision de facturas";
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(178, 108);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(31, 13);
            this.lblSerie.TabIndex = 1;
            this.lblSerie.Text = "Serie";
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.AutoSize = true;
            this.lblNumeroFactura.Location = new System.Drawing.Point(146, 136);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(63, 13);
            this.lblNumeroFactura.TabIndex = 2;
            this.lblNumeroFactura.Text = "No. Factura";
            // 
            // cmbSerie
            // 
            this.cmbSerie.FormattingEnabled = true;
            this.cmbSerie.Location = new System.Drawing.Point(233, 105);
            this.cmbSerie.Name = "cmbSerie";
            this.cmbSerie.Size = new System.Drawing.Size(235, 21);
            this.cmbSerie.TabIndex = 3;
            // 
            // txtNoFactura
            // 
            this.txtNoFactura.Location = new System.Drawing.Point(233, 133);
            this.txtNoFactura.MaxLength = 12;
            this.txtNoFactura.Name = "txtNoFactura";
            this.txtNoFactura.Size = new System.Drawing.Size(235, 20);
            this.txtNoFactura.TabIndex = 4;
            this.txtNoFactura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoFactura_KeyPress);
            // 
            // btnImprimirFactura
            // 
            this.btnImprimirFactura.Location = new System.Drawing.Point(233, 160);
            this.btnImprimirFactura.Name = "btnImprimirFactura";
            this.btnImprimirFactura.Size = new System.Drawing.Size(235, 23);
            this.btnImprimirFactura.TabIndex = 5;
            this.btnImprimirFactura.Text = "Imprimir factura";
            this.btnImprimirFactura.UseVisualStyleBackColor = true;
            this.btnImprimirFactura.Click += new System.EventHandler(this.btnImprimirFactura_Click);
            // 
            // frmReimpresionDeFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 447);
            this.Controls.Add(this.btnImprimirFactura);
            this.Controls.Add(this.txtNoFactura);
            this.Controls.Add(this.cmbSerie);
            this.Controls.Add(this.lblNumeroFactura);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.lblReimpresionDeFacturas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReimpresionDeFacturas";
            this.Text = "frmReimpresionDeFacturas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReimpresionDeFacturas;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.ComboBox cmbSerie;
        private System.Windows.Forms.TextBox txtNoFactura;
        private System.Windows.Forms.Button btnImprimirFactura;
    }
}