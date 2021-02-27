namespace FerreteriaSolucion.FORMSFACTURACION
{
    partial class frmAnularFactura
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
            this.btnAnularFactura = new System.Windows.Forms.Button();
            this.txtNoFactura = new System.Windows.Forms.TextBox();
            this.cmbSerie = new System.Windows.Forms.ComboBox();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblAnularFactura = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAnularFactura
            // 
            this.btnAnularFactura.Location = new System.Drawing.Point(231, 152);
            this.btnAnularFactura.Name = "btnAnularFactura";
            this.btnAnularFactura.Size = new System.Drawing.Size(235, 23);
            this.btnAnularFactura.TabIndex = 11;
            this.btnAnularFactura.Text = "Anular factura";
            this.btnAnularFactura.UseVisualStyleBackColor = true;
            this.btnAnularFactura.Click += new System.EventHandler(this.btnAnularFactura_Click);
            // 
            // txtNoFactura
            // 
            this.txtNoFactura.Location = new System.Drawing.Point(231, 125);
            this.txtNoFactura.MaxLength = 12;
            this.txtNoFactura.Name = "txtNoFactura";
            this.txtNoFactura.Size = new System.Drawing.Size(235, 20);
            this.txtNoFactura.TabIndex = 10;
            this.txtNoFactura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoFactura_KeyPress);
            // 
            // cmbSerie
            // 
            this.cmbSerie.FormattingEnabled = true;
            this.cmbSerie.Location = new System.Drawing.Point(231, 97);
            this.cmbSerie.Name = "cmbSerie";
            this.cmbSerie.Size = new System.Drawing.Size(235, 21);
            this.cmbSerie.TabIndex = 9;
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.AutoSize = true;
            this.lblNumeroFactura.Location = new System.Drawing.Point(144, 128);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(63, 13);
            this.lblNumeroFactura.TabIndex = 8;
            this.lblNumeroFactura.Text = "No. Factura";
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(176, 100);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(31, 13);
            this.lblSerie.TabIndex = 7;
            this.lblSerie.Text = "Serie";
            // 
            // lblAnularFactura
            // 
            this.lblAnularFactura.AutoSize = true;
            this.lblAnularFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnularFactura.Location = new System.Drawing.Point(208, 53);
            this.lblAnularFactura.Name = "lblAnularFactura";
            this.lblAnularFactura.Size = new System.Drawing.Size(194, 26);
            this.lblAnularFactura.TabIndex = 6;
            this.lblAnularFactura.Text = "Anular de factura";
            // 
            // frmAnularFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 314);
            this.Controls.Add(this.btnAnularFactura);
            this.Controls.Add(this.txtNoFactura);
            this.Controls.Add(this.cmbSerie);
            this.Controls.Add(this.lblNumeroFactura);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.lblAnularFactura);
            this.Name = "frmAnularFactura";
            this.Text = "frmAnularFactura";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnularFactura;
        private System.Windows.Forms.TextBox txtNoFactura;
        private System.Windows.Forms.ComboBox cmbSerie;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblAnularFactura;
    }
}