namespace PruebaBascula
{
	partial class Form1
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose ( );
			}
			base.Dispose ( disposing );
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.lblBascula = new System.Windows.Forms.ToolStripStatusLabel();
			this.pbBascula = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.ctlBascula = new libBascula.ControlBascula(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pbBascula)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblBascula
			// 
			this.lblBascula.Name = "lblBascula";
			this.lblBascula.Size = new System.Drawing.Size(40, 17);
			this.lblBascula.Text = "{peso}";
			// 
			// pbBascula
			// 
			this.pbBascula.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pbBascula.Image = ((System.Drawing.Image)(resources.GetObject("pbBascula.Image")));
			this.pbBascula.Location = new System.Drawing.Point(308, 260);
			this.pbBascula.Name = "pbBascula";
			this.pbBascula.Size = new System.Drawing.Size(53, 50);
			this.pbBascula.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbBascula.TabIndex = 3;
			this.pbBascula.TabStop = false;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(367, 260);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(156, 50);
			this.button1.TabIndex = 0;
			this.button1.Text = "Conectar";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(529, 63);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblBascula});
			this.statusStrip1.Location = new System.Drawing.Point(0, 313);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(805, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(529, 63);
			this.label2.TabIndex = 5;
			this.label2.Text = "label2";
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(0, 126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(529, 63);
			this.label3.TabIndex = 6;
			this.label3.Text = "label3";
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
			this.propertyGrid1.Location = new System.Drawing.Point(529, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.SelectedObject = this.ctlBascula;
			this.propertyGrid1.Size = new System.Drawing.Size(276, 313);
			this.propertyGrid1.TabIndex = 4;
			// 
			// ctlBascula
			// 
			this.ctlBascula.ActivarEnvio = global::PruebaBascula.Properties.Settings.Default.ActivarEnvio;
			this.ctlBascula.CaracterFinLinea = "0A";
			this.ctlBascula.ConnectedImage = ((System.Drawing.Image)(resources.GetObject("ctlBascula.ConnectedImage")));
			this.ctlBascula.DataBindings.Add(new System.Windows.Forms.Binding("ActivarEnvio", global::PruebaBascula.Properties.Settings.Default, "ActivarEnvio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.ctlBascula.DisconnectedImage = ((System.Drawing.Image)(resources.GetObject("ctlBascula.DisconnectedImage")));
			this.ctlBascula.Puerto = libBascula.Puertos.COM9;
			this.ctlBascula.StatusLabel = this.lblBascula;
			this.ctlBascula.StatusPicture = this.pbBascula;
			this.ctlBascula.TextoAEnviar = "LB";
			this.ctlBascula.CambioValor += new libBascula.CambioValorEvenHandler(this.ctlBascula_CambioValor);
			this.ctlBascula.CambioEstado += new libBascula.CambioEstadoEventHandler(this.ctlBascula_CambioEstado);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(805, 335);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pbBascula);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.propertyGrid1);
			this.Controls.Add(this.statusStrip1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbBascula)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private libBascula.ControlBascula ctlBascula;
		private System.Windows.Forms.PictureBox pbBascula;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.ToolStripStatusLabel lblBascula;
	}
}

