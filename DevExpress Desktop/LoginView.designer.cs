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
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginView));
			this.btnOk = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.tbPass = new DevExpress.XtraEditors.TextEdit();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.chbIsRememberPassword = new DevExpress.XtraEditors.CheckEdit();
			this.chbIsRememberLogin = new DevExpress.XtraEditors.CheckEdit();
			this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
			this.cbLogins = new DevExpress.XtraEditors.ComboBoxEdit();
			this.btDeleteLogin = new DevExpress.XtraEditors.SimpleButton();
			this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
			((System.ComponentModel.ISupportInitialize)(this.tbPass.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chbIsRememberPassword.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chbIsRememberLogin.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
			this.tablePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cbLogins.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.tablePanel1.SetColumn(this.btnOk, 1);
			this.btnOk.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.btnOk.ImageOptions.ImageToTextIndent = 5;
			this.btnOk.ImageOptions.SvgImage = global::Furmanov.UI.Properties.Resources.security_key;
			this.btnOk.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
			this.btnOk.Location = new System.Drawing.Point(154, 184);
			this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnOk.Name = "btnOk";
			this.tablePanel1.SetRow(this.btnOk, 4);
			this.btnOk.Size = new System.Drawing.Size(120, 30);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Войти";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// labelControl1
			// 
			this.tablePanel1.SetColumn(this.labelControl1, 1);
			this.labelControl1.Location = new System.Drawing.Point(160, 10);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(10);
			this.labelControl1.Name = "labelControl1";
			this.tablePanel1.SetRow(this.labelControl1, 0);
			this.labelControl1.Size = new System.Drawing.Size(46, 20);
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "Логин:";
			// 
			// labelControl2
			// 
			this.tablePanel1.SetColumn(this.labelControl2, 1);
			this.labelControl2.Location = new System.Drawing.Point(160, 56);
			this.labelControl2.Margin = new System.Windows.Forms.Padding(10);
			this.labelControl2.Name = "labelControl2";
			this.tablePanel1.SetRow(this.labelControl2, 1);
			this.labelControl2.Size = new System.Drawing.Size(56, 20);
			this.labelControl2.TabIndex = 1;
			this.labelControl2.Text = "Пароль:";
			// 
			// tbPass
			// 
			this.tablePanel1.SetColumn(this.tbPass, 2);
			this.tablePanel1.SetColumnSpan(this.tbPass, 2);
			this.tbPass.Location = new System.Drawing.Point(288, 56);
			this.tbPass.Margin = new System.Windows.Forms.Padding(10);
			this.tbPass.Name = "tbPass";
			this.tbPass.Properties.PasswordChar = '*';
			this.tablePanel1.SetRow(this.tbPass, 1);
			this.tbPass.Size = new System.Drawing.Size(236, 26);
			this.tbPass.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.tablePanel1.SetColumn(this.btnCancel, 2);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(282, 184);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btnCancel.MaximumSize = new System.Drawing.Size(120, 30);
			this.btnCancel.Name = "btnCancel";
			this.tablePanel1.SetRow(this.btnCancel, 4);
			this.btnCancel.Size = new System.Drawing.Size(120, 30);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Отмена";
			// 
			// chbIsRememberPassword
			// 
			this.tablePanel1.SetColumn(this.chbIsRememberPassword, 1);
			this.tablePanel1.SetColumnSpan(this.chbIsRememberPassword, 3);
			this.chbIsRememberPassword.Enabled = false;
			this.chbIsRememberPassword.Location = new System.Drawing.Point(160, 136);
			this.chbIsRememberPassword.Margin = new System.Windows.Forms.Padding(10);
			this.chbIsRememberPassword.Name = "chbIsRememberPassword";
			this.chbIsRememberPassword.Properties.Caption = "Запомнить пароль (автоматический вход)";
			this.tablePanel1.SetRow(this.chbIsRememberPassword, 3);
			this.chbIsRememberPassword.Size = new System.Drawing.Size(364, 24);
			this.chbIsRememberPassword.TabIndex = 22;
			// 
			// chbIsRememberLogin
			// 
			this.tablePanel1.SetColumn(this.chbIsRememberLogin, 1);
			this.tablePanel1.SetColumnSpan(this.chbIsRememberLogin, 3);
			this.chbIsRememberLogin.Location = new System.Drawing.Point(160, 102);
			this.chbIsRememberLogin.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.chbIsRememberLogin.Name = "chbIsRememberLogin";
			this.chbIsRememberLogin.Properties.Caption = "Запомнить логин";
			this.tablePanel1.SetRow(this.chbIsRememberLogin, 2);
			this.chbIsRememberLogin.Size = new System.Drawing.Size(145, 24);
			this.chbIsRememberLogin.TabIndex = 23;
			this.chbIsRememberLogin.CheckedChanged += new System.EventHandler(this.chbIsRememberLogin_CheckedChanged);
			// 
			// tablePanel1
			// 
			this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 150F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 50F)});
			this.tablePanel1.Controls.Add(this.cbLogins);
			this.tablePanel1.Controls.Add(this.btDeleteLogin);
			this.tablePanel1.Controls.Add(this.pictureEdit1);
			this.tablePanel1.Controls.Add(this.btnOk);
			this.tablePanel1.Controls.Add(this.btnCancel);
			this.tablePanel1.Controls.Add(this.chbIsRememberPassword);
			this.tablePanel1.Controls.Add(this.labelControl1);
			this.tablePanel1.Controls.Add(this.chbIsRememberLogin);
			this.tablePanel1.Controls.Add(this.labelControl2);
			this.tablePanel1.Controls.Add(this.tbPass);
			this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tablePanel1.Location = new System.Drawing.Point(0, 0);
			this.tablePanel1.Name = "tablePanel1";
			this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
			this.tablePanel1.Size = new System.Drawing.Size(534, 228);
			this.tablePanel1.TabIndex = 25;
			// 
			// cbLogins
			// 
			this.tablePanel1.SetColumn(this.cbLogins, 2);
			this.cbLogins.Location = new System.Drawing.Point(288, 10);
			this.cbLogins.Margin = new System.Windows.Forms.Padding(10);
			this.cbLogins.Name = "cbLogins";
			this.cbLogins.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.tablePanel1.SetRow(this.cbLogins, 0);
			this.cbLogins.Size = new System.Drawing.Size(199, 26);
			this.cbLogins.TabIndex = 26;
			this.cbLogins.SelectedIndexChanged += new System.EventHandler(this.cbLogins_SelectedIndexChanged);
			// 
			// btDeleteLogin
			// 
			this.tablePanel1.SetColumn(this.btDeleteLogin, 3);
			this.btDeleteLogin.ImageOptions.SvgImage = global::Furmanov.UI.Properties.Resources.delete;
			this.btDeleteLogin.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
			this.btDeleteLogin.Location = new System.Drawing.Point(498, 10);
			this.btDeleteLogin.Margin = new System.Windows.Forms.Padding(1, 10, 10, 10);
			this.btDeleteLogin.MaximumSize = new System.Drawing.Size(26, 26);
			this.btDeleteLogin.Name = "btDeleteLogin";
			this.tablePanel1.SetRow(this.btDeleteLogin, 0);
			this.btDeleteLogin.Size = new System.Drawing.Size(26, 26);
			toolTipTitleItem1.Text = "Удалить логин";
			toolTipItem1.ImageOptions.Image = global::Furmanov.UI.Properties.Resources.cancel_32x32;
			toolTipItem1.LeftIndent = 6;
			toolTipItem1.Text = "Удалить выбранный логин из выпадающего списка";
			superToolTip1.Items.Add(toolTipTitleItem1);
			superToolTip1.Items.Add(toolTipItem1);
			this.btDeleteLogin.SuperTip = superToolTip1;
			this.btDeleteLogin.TabIndex = 25;
			this.btDeleteLogin.Click += new System.EventHandler(this.btDeleteLogin_Click);
			// 
			// pictureEdit1
			// 
			this.tablePanel1.SetColumn(this.pictureEdit1, 0);
			this.pictureEdit1.EditValue = global::Furmanov.UI.Properties.Resources.login;
			this.pictureEdit1.Location = new System.Drawing.Point(4, 5);
			this.pictureEdit1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.pictureEdit1.Name = "pictureEdit1";
			this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
			this.tablePanel1.SetRow(this.pictureEdit1, 0);
			this.tablePanel1.SetRowSpan(this.pictureEdit1, 4);
			this.pictureEdit1.Size = new System.Drawing.Size(142, 160);
			this.pictureEdit1.TabIndex = 24;
			// 
			// LoginView
			// 
			this.AcceptButton = this.btnOk;
			this.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.Appearance.Options.UseBackColor = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(534, 228);
			this.Controls.Add(this.tablePanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Вход";
			((System.ComponentModel.ISupportInitialize)(this.tbPass.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chbIsRememberPassword.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chbIsRememberLogin.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
			this.tablePanel1.ResumeLayout(false);
			this.tablePanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cbLogins.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit tbPass;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.CheckEdit chbIsRememberPassword;
        private DevExpress.XtraEditors.CheckEdit chbIsRememberLogin;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
		private DevExpress.Utils.Layout.TablePanel tablePanel1;
		private DevExpress.XtraEditors.SimpleButton btDeleteLogin;
		private DevExpress.XtraEditors.ComboBoxEdit cbLogins;
	}
}