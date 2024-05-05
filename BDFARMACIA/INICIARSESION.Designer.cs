namespace BDFARMACIA
{
    partial class INICIARSESION
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(INICIARSESION));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxusuario = new System.Windows.Forms.TextBox();
            this.textBoxcontraseña = new System.Windows.Forms.TextBox();
            this.buttoningresar = new System.Windows.Forms.Button();
            this.buttoncerrar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "USUARIO";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "CONTRASEÑA";
            // 
            // textBoxusuario
            // 
            this.textBoxusuario.Location = new System.Drawing.Point(245, 199);
            this.textBoxusuario.Name = "textBoxusuario";
            this.textBoxusuario.Size = new System.Drawing.Size(100, 20);
            this.textBoxusuario.TabIndex = 2;
            this.textBoxusuario.TextChanged += new System.EventHandler(this.textBoxusuario_TextChanged);
            // 
            // textBoxcontraseña
            // 
            this.textBoxcontraseña.Location = new System.Drawing.Point(245, 275);
            this.textBoxcontraseña.Name = "textBoxcontraseña";
            this.textBoxcontraseña.Size = new System.Drawing.Size(100, 20);
            this.textBoxcontraseña.TabIndex = 3;
            // 
            // buttoningresar
            // 
            this.buttoningresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.buttoningresar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoningresar.Location = new System.Drawing.Point(120, 343);
            this.buttoningresar.Name = "buttoningresar";
            this.buttoningresar.Size = new System.Drawing.Size(88, 32);
            this.buttoningresar.TabIndex = 4;
            this.buttoningresar.Text = "INGRESAR";
            this.buttoningresar.UseVisualStyleBackColor = false;
            this.buttoningresar.Click += new System.EventHandler(this.buttoningresar_Click);
            // 
            // buttoncerrar
            // 
            this.buttoncerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.buttoncerrar.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncerrar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttoncerrar.Location = new System.Drawing.Point(259, 343);
            this.buttoncerrar.Name = "buttoncerrar";
            this.buttoncerrar.Size = new System.Drawing.Size(72, 32);
            this.buttoncerrar.TabIndex = 5;
            this.buttoncerrar.Text = "CERRAR";
            this.buttoncerrar.UseVisualStyleBackColor = false;
            this.buttoncerrar.Click += new System.EventHandler(this.buttoncerrar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(424, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // INICIARSESION
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(219)))), ((int)(((byte)(168)))));
            this.ClientSize = new System.Drawing.Size(444, 397);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttoncerrar);
            this.Controls.Add(this.buttoningresar);
            this.Controls.Add(this.textBoxcontraseña);
            this.Controls.Add(this.textBoxusuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "INICIARSESION";
            this.Text = "FARMACIA";
            this.Load += new System.EventHandler(this.INICIARSESION_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxusuario;
        private System.Windows.Forms.TextBox textBoxcontraseña;
        private System.Windows.Forms.Button buttoningresar;
        private System.Windows.Forms.Button buttoncerrar;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

