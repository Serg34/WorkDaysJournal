namespace Furmanov.UI
{
	partial class LoginView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbLogins = new System.Windows.Forms.ComboBox();
			this.tbPass = new System.Windows.Forms.TextBox();
			this.chbIsRememberLogin = new System.Windows.Forms.CheckBox();
			this.chbIsRememberPassword = new System.Windows.Forms.CheckBox();
			this.btCancel = new System.Windows.Forms.Button();
			this.btOk = new System.Windows.Forms.Button();
			this.btDeleteLogin = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.btDeleteLogin, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbLogins, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.tbPass, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.chbIsRememberLogin, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.chbIsRememberPassword, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.btCancel, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.btOk, 1, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 249);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::Furmanov.UI.Properties.Resources.login;
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.tableLayoutPanel1.SetRowSpan(this.pictureBox1, 4);
			this.pictureBox1.Size = new System.Drawing.Size(194, 182);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(212, 12);
			this.label1.Margin = new System.Windows.Forms.Padding(7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Логин:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(212, 59);
			this.label2.Margin = new System.Windows.Forms.Padding(7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Пароль:";
			// 
			// cbLogins
			// 
			this.cbLogins.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbLogins.FormattingEnabled = true;
			this.cbLogins.Location = new System.Drawing.Point(334, 8);
			this.cbLogins.Name = "cbLogins";
			this.cbLogins.Size = new System.Drawing.Size(308, 28);
			this.cbLogins.TabIndex = 3;
			this.cbLogins.SelectedIndexChanged += new System.EventHandler(this.cbLogins_SelectedIndexChanged);
			// 
			// tbPass
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.tbPass, 2);
			this.tbPass.Dock = System.Windows.Forms.DockStyle.Top;
			this.tbPass.Location = new System.Drawing.Point(334, 55);
			this.tbPass.Name = "tbPass";
			this.tbPass.Size = new System.Drawing.Size(342, 27);
			this.tbPass.TabIndex = 4;
			this.tbPass.UseSystemPasswordChar = true;
			// 
			// chbIsRememberLogin
			// 
			this.chbIsRememberLogin.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.chbIsRememberLogin, 3);
			this.chbIsRememberLogin.Location = new System.Drawing.Point(212, 106);
			this.chbIsRememberLogin.Margin = new System.Windows.Forms.Padding(7);
			this.chbIsRememberLogin.Name = "chbIsRememberLogin";
			this.chbIsRememberLogin.Size = new System.Drawing.Size(150, 24);
			this.chbIsRememberLogin.TabIndex = 5;
			this.chbIsRememberLogin.Text = "Запомнить логин";
			this.chbIsRememberLogin.UseVisualStyleBackColor = true;
			this.chbIsRememberLogin.CheckedChanged += new System.EventHandler(this.chbIsRememberLogin_CheckedChanged);
			// 
			// chbIsRememberPassword
			// 
			this.chbIsRememberPassword.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.chbIsRememberPassword, 3);
			this.chbIsRememberPassword.Location = new System.Drawing.Point(212, 153);
			this.chbIsRememberPassword.Margin = new System.Windows.Forms.Padding(7);
			this.chbIsRememberPassword.Name = "chbIsRememberPassword";
			this.chbIsRememberPassword.Size = new System.Drawing.Size(323, 24);
			this.chbIsRememberPassword.TabIndex = 6;
			this.chbIsRememberPassword.Text = "Запомнить пароль (автоматический вход)";
			this.chbIsRememberPassword.UseVisualStyleBackColor = true;
			// 
			// btCancel
			// 
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Location = new System.Drawing.Point(334, 196);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new System.Drawing.Size(120, 30);
			this.btCancel.TabIndex = 7;
			this.btCancel.Text = "Отмена";
			this.btCancel.UseVisualStyleBackColor = true;
			// 
			// btOk
			// 
			this.btOk.Location = new System.Drawing.Point(208, 196);
			this.btOk.Name = "btOk";
			this.btOk.Size = new System.Drawing.Size(120, 30);
			this.btOk.TabIndex = 8;
			this.btOk.Text = "Вход";
			this.btOk.UseVisualStyleBackColor = true;
			this.btOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btDeleteLogin
			// 
			this.btDeleteLogin.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btDeleteLogin.Image = global::Furmanov.UI.Properties.Resources.cancel_32x32;
			this.btDeleteLogin.Location = new System.Drawing.Point(648, 8);
			this.btDeleteLogin.Name = "btDeleteLogin";
			this.btDeleteLogin.Size = new System.Drawing.Size(28, 28);
			this.btDeleteLogin.TabIndex = 9;
			this.btDeleteLogin.UseVisualStyleBackColor = true;
			this.btDeleteLogin.Click += new System.EventHandler(this.btDeleteLogin_Click);
			// 
			// LoginView
			// 
			this.AcceptButton = this.btOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btCancel;
			this.ClientSize = new System.Drawing.Size(684, 249);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "LoginView";
			this.Text = "Вход";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbLogins;
		private System.Windows.Forms.TextBox tbPass;
		private System.Windows.Forms.CheckBox chbIsRememberLogin;
		private System.Windows.Forms.CheckBox chbIsRememberPassword;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.Button btOk;
		private System.Windows.Forms.Button btDeleteLogin;
	}
}