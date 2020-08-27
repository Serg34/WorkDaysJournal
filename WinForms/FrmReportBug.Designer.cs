using System.Windows.Forms;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportBug));
			this.lbTitle = new System.Windows.Forms.Label();
			this.btOk = new System.Windows.Forms.Button();
			this.lbDescr = new System.Windows.Forms.Label();
			this.picLogo = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
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
			this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOk.Location = new System.Drawing.Point(178, 3);
			this.btOk.Name = "btOk";
			this.btOk.Size = new System.Drawing.Size(344, 30);
			this.btOk.TabIndex = 21;
			this.btOk.Text = "Ok";
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
			this.lbDescr.Size = new System.Drawing.Size(700, 65);
			this.lbDescr.TabIndex = 17;
			this.lbDescr.Text = "К сожалению, не удалось отправить данные об ошибке разработчикам. Передайте, пожа" +
    "луйста, данные вручную";
			this.lbDescr.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// picLogo
			// 
			this.picLogo.Dock = System.Windows.Forms.DockStyle.Top;
			this.picLogo.Image = global::Furmanov.UI.Properties.Resources.Fsi;
			this.picLogo.Location = new System.Drawing.Point(0, 30);
			this.picLogo.Name = "picLogo";
			this.picLogo.Size = new System.Drawing.Size(700, 96);
			this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picLogo.TabIndex = 20;
			this.picLogo.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.btOk, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 359);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(700, 36);
			this.tableLayoutPanel1.TabIndex = 21;
			// 
			// FrmReportBug
			// 
			this.AcceptButton = this.btOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(700, 410);
			this.Controls.Add(this.lbDescr);
			this.Controls.Add(this.lbTitle);
			this.Controls.Add(this.picLogo);
			this.Controls.Add(this.tableLayoutPanel1);
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
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lbTitle;
		private PictureBox picLogo;
		private Button btOk;
		private System.Windows.Forms.Label lbDescr;
		private TableLayoutPanel tableLayoutPanel1;
	}
}