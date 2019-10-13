namespace Furmanov
{
	partial class XtraForm1
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
			this.tabFormControl1 = new DevExpress.XtraBars.TabFormControl();
			this.tabFormPage1 = new DevExpress.XtraBars.TabFormPage();
			this.tabFormContentContainer1 = new DevExpress.XtraBars.TabFormContentContainer();
			this.button1 = new System.Windows.Forms.Button();
			this.tabFormPage2 = new DevExpress.XtraBars.TabFormPage();
			this.tabFormContentContainer2 = new DevExpress.XtraBars.TabFormContentContainer();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.tabFormPage3 = new DevExpress.XtraBars.TabFormPage();
			this.tabFormContentContainer3 = new DevExpress.XtraBars.TabFormContentContainer();
			this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
			this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).BeginInit();
			this.tabFormContentContainer1.SuspendLayout();
			this.tabFormContentContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
			this.SuspendLayout();
			// 
			// tabFormControl1
			// 
			this.tabFormControl1.Location = new System.Drawing.Point(0, 0);
			this.tabFormControl1.Name = "tabFormControl1";
			this.tabFormControl1.Pages.Add(this.tabFormPage1);
			this.tabFormControl1.Pages.Add(this.tabFormPage2);
			this.tabFormControl1.Pages.Add(this.tabFormPage3);
			this.tabFormControl1.SelectedPage = this.tabFormPage1;
			this.tabFormControl1.ShowAddPageButton = false;
			this.tabFormControl1.ShowTabCloseButtons = false;
			this.tabFormControl1.Size = new System.Drawing.Size(1121, 75);
			this.tabFormControl1.TabForm = this;
			this.tabFormControl1.TabIndex = 0;
			this.tabFormControl1.TabStop = false;
			// 
			// tabFormPage1
			// 
			this.tabFormPage1.ContentContainer = this.tabFormContentContainer1;
			this.tabFormPage1.Name = "tabFormPage1";
			this.tabFormPage1.Text = "  Поступления  ";
			// 
			// tabFormContentContainer1
			// 
			this.tabFormContentContainer1.Controls.Add(this.ribbonControl1);
			this.tabFormContentContainer1.Controls.Add(this.button1);
			this.tabFormContentContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabFormContentContainer1.Location = new System.Drawing.Point(0, 75);
			this.tabFormContentContainer1.Name = "tabFormContentContainer1";
			this.tabFormContentContainer1.Size = new System.Drawing.Size(1121, 480);
			this.tabFormContentContainer1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(501, 180);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// tabFormPage2
			// 
			this.tabFormPage2.ContentContainer = this.tabFormContentContainer2;
			this.tabFormPage2.Name = "tabFormPage2";
			this.tabFormPage2.Text = "  Накладные  ";
			// 
			// tabFormContentContainer2
			// 
			this.tabFormContentContainer2.Controls.Add(this.checkBox1);
			this.tabFormContentContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabFormContentContainer2.Location = new System.Drawing.Point(0, 75);
			this.tabFormContentContainer2.Name = "tabFormContentContainer2";
			this.tabFormContentContainer2.Size = new System.Drawing.Size(1121, 480);
			this.tabFormContentContainer2.TabIndex = 2;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(635, 213);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(80, 17);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "checkBox1";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// tabFormPage3
			// 
			this.tabFormPage3.ContentContainer = this.tabFormContentContainer3;
			this.tabFormPage3.Name = "tabFormPage3";
			this.tabFormPage3.Text = "Page 2";
			// 
			// tabFormContentContainer3
			// 
			this.tabFormContentContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabFormContentContainer3.Location = new System.Drawing.Point(0, 75);
			this.tabFormContentContainer3.Name = "tabFormContentContainer3";
			this.tabFormContentContainer3.Size = new System.Drawing.Size(1121, 480);
			this.tabFormContentContainer3.TabIndex = 3;
			// 
			// ribbonControl1
			// 
			this.ribbonControl1.ExpandCollapseItem.Id = 0;
			this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.SearchEditItem,
            this.ribbonControl1.ExpandCollapseItem});
			this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
			this.ribbonControl1.MaxItemId = 1;
			this.ribbonControl1.Name = "ribbonControl1";
			this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
			this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
			this.ribbonControl1.ShowCategoryInCaption = false;
			this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
			this.ribbonControl1.ShowMoreCommandsButton = DevExpress.Utils.DefaultBoolean.False;
			this.ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
			this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
			this.ribbonControl1.ShowQatLocationSelector = false;
			this.ribbonControl1.ShowToolbarCustomizeItem = false;
			this.ribbonControl1.Size = new System.Drawing.Size(1121, 108);
			this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
			this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
			// 
			// ribbonPage1
			// 
			this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
			this.ribbonPage1.Name = "ribbonPage1";
			this.ribbonPage1.Text = "ribbonPage1";
			// 
			// ribbonPageGroup1
			// 
			this.ribbonPageGroup1.Name = "ribbonPageGroup1";
			this.ribbonPageGroup1.Text = "ribbonPageGroup1";
			// 
			// XtraForm1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1121, 555);
			this.Controls.Add(this.tabFormContentContainer1);
			this.Controls.Add(this.tabFormControl1);
			this.Name = "XtraForm1";
			this.TabFormControl = this.tabFormControl1;
			this.Text = "XtraForm1";
			((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).EndInit();
			this.tabFormContentContainer1.ResumeLayout(false);
			this.tabFormContentContainer1.PerformLayout();
			this.tabFormContentContainer2.ResumeLayout(false);
			this.tabFormContentContainer2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraBars.TabFormControl tabFormControl1;
		private DevExpress.XtraBars.TabFormPage tabFormPage1;
		private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer1;
		private System.Windows.Forms.Button button1;
		private DevExpress.XtraBars.TabFormPage tabFormPage2;
		private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer2;
		private System.Windows.Forms.CheckBox checkBox1;
		private DevExpress.XtraBars.TabFormPage tabFormPage3;
		private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer3;
		private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
	}
}