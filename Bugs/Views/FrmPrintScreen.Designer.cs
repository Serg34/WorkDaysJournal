namespace Furmanov.Views
{
	partial class FrmPrintScreen
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintScreen));
			this.picPrintScreen = new PictureEditor();
			this.SuspendLayout();
			// 
			// picPrintScreen
			// 
			this.picPrintScreen.AllowDrop = true;
			this.picPrintScreen.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picPrintScreen.Image = null;
			this.picPrintScreen.Location = new System.Drawing.Point(0, 0);
			this.picPrintScreen.Name = "picPrintScreen";
			this.picPrintScreen.Size = new System.Drawing.Size(487, 292);
			this.picPrintScreen.TabIndex = 0;
			// 
			// FrmPrintScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 292);
			this.Controls.Add(this.picPrintScreen);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "FrmPrintScreen";
			this.Text = "Снимок экрана";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}

		#endregion

		private PictureEditor picPrintScreen;
	}
}