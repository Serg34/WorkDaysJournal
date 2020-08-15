namespace Furmanov.UI
{
	partial class FrmReportBug
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
			DevExpress.Utils.Layout.TablePanelColumn tablePanelColumn1 = new DevExpress.Utils.Layout.TablePanelColumn();
			DevExpress.Utils.Layout.TablePanelColumn tablePanelColumn2 = new DevExpress.Utils.Layout.TablePanelColumn();
			DevExpress.Utils.Layout.TablePanelColumn tablePanelColumn3 = new DevExpress.Utils.Layout.TablePanelColumn();
			DevExpress.Utils.Layout.TablePanelRow tablePanelRow1 = new DevExpress.Utils.Layout.TablePanelRow();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportBug));
			this.lbTitle = new System.Windows.Forms.Label();
			this.btOk = new DevExpress.XtraEditors.SimpleButton();
			this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
			this.lbDescr = new System.Windows.Forms.Label();
			this.picLogo = new DevExpress.XtraEditors.PictureEdit();
			((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// lbTitle
			// 
			this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
			this.lbTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
			this.lbTitle.Location = new System.Drawing.Point(0, 126);
			this.lbTitle.Margin = new System.Windows.Forms.Padding(0);
			this.lbTitle.Name = "lbTitle";
			this.lbTitle.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);
			this.lbTitle.Size = new System.Drawing.Size(700, 60);
			this.lbTitle.TabIndex = 18;
			this.lbTitle.Text = "Возникла проблема в работе программы";
			this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btOk
			// 
			this.tablePanel1.SetColumn(this.btOk, 1);
			this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOk.Location = new System.Drawing.Point(300, 3);
			this.btOk.Name = "btOk";
			this.tablePanel1.SetRow(this.btOk, 0);
			this.btOk.Size = new System.Drawing.Size(100, 30);
			this.btOk.TabIndex = 21;
			this.btOk.Text = "Ok";
			// 
			// tablePanel1
			// 
			tablePanelColumn1.Style = DevExpress.Utils.Layout.TablePanelEntityStyle.Relative;
			tablePanelColumn1.Width = 50F;
			tablePanelColumn2.Width = 100F;
			tablePanelColumn3.Style = DevExpress.Utils.Layout.TablePanelEntityStyle.Relative;
			tablePanelColumn3.Width = 50F;
			this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            tablePanelColumn1,
            tablePanelColumn2,
            tablePanelColumn3});
			this.tablePanel1.Controls.Add(this.btOk);
			this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tablePanel1.Location = new System.Drawing.Point(0, 359);
			this.tablePanel1.Name = "tablePanel1";
			tablePanelRow1.Height = 30F;
			tablePanelRow1.Style = DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute;
			this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            tablePanelRow1});
			this.tablePanel1.Size = new System.Drawing.Size(700, 36);
			this.tablePanel1.TabIndex = 22;
			// 
			// lbDescr
			// 
			this.lbDescr.AutoSize = true;
			this.lbDescr.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbDescr.ForeColor = System.Drawing.Color.DimGray;
			this.lbDescr.Location = new System.Drawing.Point(0, 186);
			this.lbDescr.Margin = new System.Windows.Forms.Padding(0);
			this.lbDescr.MaximumSize = new System.Drawing.Size(700, 0);
			this.lbDescr.MinimumSize = new System.Drawing.Size(700, 0);
			this.lbDescr.Name = "lbDescr";
			this.lbDescr.Padding = new System.Windows.Forms.Padding(25, 0, 25, 25);
			this.lbDescr.Size = new System.Drawing.Size(700, 75);
			this.lbDescr.TabIndex = 17;
			this.lbDescr.Text = "К сожалению, не удалось отправить данные об ошибке разработчикам. Передайте, пожа" +
    "луйста, данные вручную";
			this.lbDescr.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// picLogo
			// 
			this.picLogo.Dock = System.Windows.Forms.DockStyle.Top;
			this.picLogo.EditValue = global::Furmanov.UI.Properties.Resources.Fsi;
			this.picLogo.Location = new System.Drawing.Point(0, 30);
			this.picLogo.Name = "picLogo";
			// 
			// 
			// 
			this.picLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.picLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.picLogo.Size = new System.Drawing.Size(700, 96);
			this.picLogo.TabIndex = 20;
			// 
			// FrmReportBug
			// 
			this.AcceptButton = this.btOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(700, 410);
			this.Controls.Add(this.lbDescr);
			this.Controls.Add(this.tablePanel1);
			this.Controls.Add(this.lbTitle);
			this.Controls.Add(this.picLogo);
			this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(700, 0);
			this.Name = "FrmReportBug";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 15);
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Отчёт об ошибке";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmReportBug_FormClosed);
			this.Shown += new System.EventHandler(this.FrmReportBug_Shown);
			((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lbTitle;
		private DevExpress.XtraEditors.PictureEdit picLogo;
		private DevExpress.XtraEditors.SimpleButton btOk;
		private DevExpress.Utils.Layout.TablePanel tablePanel1;
		private System.Windows.Forms.Label lbDescr;
	}
}