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
			this.btnOk = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.tbLogin = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.tbPass = new DevExpress.XtraEditors.TextEdit();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.checkAutoLoginOnStart = new DevExpress.XtraEditors.CheckEdit();
			this.checkRemember = new DevExpress.XtraEditors.CheckEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
			((System.ComponentModel.ISupportInitialize)(this.tbLogin.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbPass.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkAutoLoginOnStart.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkRemember.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(141, 134);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(175, 30);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Вход";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(143, 41);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(34, 13);
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "Логин:";
			// 
			// tbLogin
			// 
			this.tbLogin.Location = new System.Drawing.Point(216, 38);
			this.tbLogin.Name = "tbLogin";
			this.tbLogin.Size = new System.Drawing.Size(100, 20);
			this.tbLogin.TabIndex = 0;
			this.tbLogin.EditValueChanged += new System.EventHandler(this.TbLogin_EditValueChanged);
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point(143, 67);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(41, 13);
			this.labelControl2.TabIndex = 1;
			this.labelControl2.Text = "Пароль:";
			// 
			// tbPass
			// 
			this.tbPass.Location = new System.Drawing.Point(216, 64);
			this.tbPass.Name = "tbPass";
			this.tbPass.Properties.PasswordChar = '*';
			this.tbPass.Size = new System.Drawing.Size(100, 20);
			this.tbPass.TabIndex = 1;
			this.tbPass.EditValueChanged += new System.EventHandler(this.TbPass_EditValueChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(36, 141);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(76, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// checkAutoLoginOnStart
			// 
			this.checkAutoLoginOnStart.Enabled = false;
			this.checkAutoLoginOnStart.Location = new System.Drawing.Point(235, 94);
			this.checkAutoLoginOnStart.Name = "checkAutoLoginOnStart";
			this.checkAutoLoginOnStart.Properties.Caption = "Автовход";
			this.checkAutoLoginOnStart.Size = new System.Drawing.Size(88, 19);
			this.checkAutoLoginOnStart.TabIndex = 22;
			this.checkAutoLoginOnStart.CheckedChanged += new System.EventHandler(this.CheckAutoLoginOnStart_CheckedChanged);
			// 
			// checkRemember
			// 
			this.checkRemember.Location = new System.Drawing.Point(141, 94);
			this.checkRemember.Name = "checkRemember";
			this.checkRemember.Properties.Caption = "Запомнить";
			this.checkRemember.Size = new System.Drawing.Size(88, 19);
			this.checkRemember.TabIndex = 23;
			this.checkRemember.CheckedChanged += new System.EventHandler(this.checkRemember_CheckedChanged);
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
			this.labelControl3.Appearance.Options.UseForeColor = true;
			this.labelControl3.Location = new System.Drawing.Point(145, 15);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(149, 13);
			this.labelControl3.TabIndex = 25;
			this.labelControl3.Text = "Неверные данные для входа";
			this.labelControl3.Visible = false;
			// 
			// pictureEdit1
			// 
			this.pictureEdit1.EditValue = global::Furmanov.UI.Properties.Resources.login;
			this.pictureEdit1.Location = new System.Drawing.Point(12, 12);
			this.pictureEdit1.Name = "pictureEdit1";
			this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
			this.pictureEdit1.Size = new System.Drawing.Size(100, 100);
			this.pictureEdit1.TabIndex = 24;
			// 
			// LoginView
			// 
			this.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.Appearance.Options.UseBackColor = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(341, 188);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.pictureEdit1);
			this.Controls.Add(this.checkAutoLoginOnStart);
			this.Controls.Add(this.checkRemember);
			this.Controls.Add(this.tbPass);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.tbLogin);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Вход";
			((System.ComponentModel.ISupportInitialize)(this.tbLogin.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbPass.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkAutoLoginOnStart.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkRemember.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tbLogin;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit tbPass;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.CheckEdit checkAutoLoginOnStart;
        private DevExpress.XtraEditors.CheckEdit checkRemember;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}