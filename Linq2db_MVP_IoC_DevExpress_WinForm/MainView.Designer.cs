﻿namespace Furmanov
{
	partial class MainView
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
			this.components = new System.ComponentModel.Container();
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
			this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
			this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
			this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
			this.btnRedo = new DevExpress.XtraBars.BarButtonItem();
			this.ribbonPageMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.ribbonPageGenerateDb = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
			this.отрудник = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
			this.undoMenu = new DevExpress.XtraBars.PopupMenu(this.components);
			this.redoMenu = new DevExpress.XtraBars.PopupMenu(this.components);
			((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.undoMenu)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.redoMenu)).BeginInit();
			this.SuspendLayout();
			// 
			// ribbon
			// 
			this.ribbon.ExpandCollapseItem.Id = 0;
			this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.barButtonItem7,
            this.btnRedo});
			this.ribbon.Location = new System.Drawing.Point(0, 0);
			this.ribbon.MaxItemId = 9;
			this.ribbon.Name = "ribbon";
			this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageMain,
            this.ribbonPageGenerateDb});
			this.ribbon.QuickToolbarItemLinks.Add(this.barButtonItem7);
			this.ribbon.QuickToolbarItemLinks.Add(this.btnRedo);
			this.ribbon.Size = new System.Drawing.Size(1100, 170);
			this.ribbon.StatusBar = this.ribbonStatusBar;
			// 
			// barButtonItem1
			// 
			this.barButtonItem1.Caption = "Добавить";
			this.barButtonItem1.Id = 1;
			this.barButtonItem1.ImageOptions.Image = global::Furmanov.Properties.Resources.AddUser;
			this.barButtonItem1.Name = "barButtonItem1";
			this.barButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
			// 
			// barButtonItem2
			// 
			this.barButtonItem2.Caption = "barButtonItem2";
			this.barButtonItem2.Id = 2;
			this.barButtonItem2.ImageOptions.Image = global::Furmanov.Properties.Resources.EditUser;
			this.barButtonItem2.Name = "barButtonItem2";
			this.barButtonItem2.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
			// 
			// barButtonItem3
			// 
			this.barButtonItem3.Caption = "barButtonItem3";
			this.barButtonItem3.Id = 3;
			this.barButtonItem3.ImageOptions.Image = global::Furmanov.Properties.Resources.DeleteUser;
			this.barButtonItem3.Name = "barButtonItem3";
			this.barButtonItem3.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
			// 
			// barButtonItem4
			// 
			this.barButtonItem4.Caption = "Добавить";
			this.barButtonItem4.Id = 4;
			this.barButtonItem4.ImageOptions.Image = global::Furmanov.Properties.Resources.AddUser;
			this.barButtonItem4.Name = "barButtonItem4";
			this.barButtonItem4.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
			this.barButtonItem4.ShortcutKeyDisplayString = "+";
			this.barButtonItem4.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
			toolTipTitleItem1.Text = "Создать сотрудника";
			toolTipItem1.ImageOptions.Image = global::Furmanov.Properties.Resources.AddUser;
			toolTipItem1.LeftIndent = 6;
			toolTipItem1.Text = "Создать сотрудника.\r\nГорячая клавиша: \"+\"";
			superToolTip1.Items.Add(toolTipTitleItem1);
			superToolTip1.Items.Add(toolTipItem1);
			this.barButtonItem4.SuperTip = superToolTip1;
			// 
			// barButtonItem5
			// 
			this.barButtonItem5.Caption = "Изменить";
			this.barButtonItem5.Id = 5;
			this.barButtonItem5.ImageOptions.Image = global::Furmanov.Properties.Resources.EditUser;
			this.barButtonItem5.Name = "barButtonItem5";
			this.barButtonItem5.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
			toolTipTitleItem2.Text = "Изменить сотрудника";
			toolTipItem2.ImageOptions.Image = global::Furmanov.Properties.Resources.EditUser;
			toolTipItem2.LeftIndent = 6;
			toolTipItem2.Text = "Редактировать данные сотрудника.\r\nДоступно по двойному клику на сотруднике.";
			superToolTip2.Items.Add(toolTipTitleItem2);
			superToolTip2.Items.Add(toolTipItem2);
			this.barButtonItem5.SuperTip = superToolTip2;
			// 
			// barButtonItem6
			// 
			this.barButtonItem6.Caption = "  Удалить  ";
			this.barButtonItem6.Id = 6;
			this.barButtonItem6.ImageOptions.Image = global::Furmanov.Properties.Resources.DeleteUser;
			this.barButtonItem6.Name = "barButtonItem6";
			this.barButtonItem6.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
			this.barButtonItem6.ShortcutKeyDisplayString = "Delete";
			this.barButtonItem6.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
			toolTipTitleItem3.Text = "Удалить сотрудника";
			toolTipItem3.ImageOptions.Image = global::Furmanov.Properties.Resources.DeleteUser;
			toolTipItem3.LeftIndent = 6;
			toolTipItem3.Text = "Удалить сотрудника и все данные о нём.\r\nГорячая клавиша: \"Delete\"";
			superToolTip3.Items.Add(toolTipTitleItem3);
			superToolTip3.Items.Add(toolTipItem3);
			this.barButtonItem6.SuperTip = superToolTip3;
			// 
			// barButtonItem7
			// 
			this.barButtonItem7.ActAsDropDown = true;
			this.barButtonItem7.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			this.barButtonItem7.Caption = "Отменить";
			this.barButtonItem7.DropDownControl = this.undoMenu;
			this.barButtonItem7.Id = 7;
			this.barButtonItem7.ImageOptions.Image = global::Furmanov.Properties.Resources.undo;
			this.barButtonItem7.Name = "barButtonItem7";
			// 
			// btnRedo
			// 
			this.btnRedo.ActAsDropDown = true;
			this.btnRedo.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			this.btnRedo.Caption = "Повторить";
			this.btnRedo.DropDownControl = this.redoMenu;
			this.btnRedo.Enabled = false;
			this.btnRedo.Id = 8;
			this.btnRedo.ImageOptions.Image = global::Furmanov.Properties.Resources.RedoNoEnabled;
			this.btnRedo.Name = "btnRedo";
			// 
			// ribbonPageMain
			// 
			this.ribbonPageMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
			this.ribbonPageMain.Name = "ribbonPageMain";
			this.ribbonPageMain.Text = "Главная";
			// 
			// ribbonPageGroup1
			// 
			this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem4);
			this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem5);
			this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem6);
			this.ribbonPageGroup1.Name = "ribbonPageGroup1";
			this.ribbonPageGroup1.ShowCaptionButton = false;
			this.ribbonPageGroup1.Text = "Сотрудники";
			// 
			// ribbonPageGenerateDb
			// 
			this.ribbonPageGenerateDb.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
			this.ribbonPageGenerateDb.Name = "ribbonPageGenerateDb";
			this.ribbonPageGenerateDb.Text = "Генерация БД";
			// 
			// ribbonPageGroup2
			// 
			this.ribbonPageGroup2.Name = "ribbonPageGroup2";
			this.ribbonPageGroup2.Text = "ribbonPageGroup2";
			// 
			// ribbonStatusBar
			// 
			this.ribbonStatusBar.Location = new System.Drawing.Point(0, 510);
			this.ribbonStatusBar.Name = "ribbonStatusBar";
			this.ribbonStatusBar.Ribbon = this.ribbon;
			this.ribbonStatusBar.Size = new System.Drawing.Size(1100, 24);
			// 
			// отрудник
			// 
			this.отрудник.ImageOptions.Image = global::Furmanov.Properties.Resources.AddUser;
			this.отрудник.ItemLinks.Add(this.barButtonItem1);
			this.отрудник.ItemLinks.Add(this.barButtonItem2);
			this.отрудник.ItemLinks.Add(this.barButtonItem3);
			this.отрудник.Name = "отрудник";
			this.отрудник.Text = "Сотрудники";
			// 
			// undoMenu
			// 
			this.undoMenu.Name = "undoMenu";
			this.undoMenu.Ribbon = this.ribbon;
			this.undoMenu.PaintMenuBar += new DevExpress.XtraBars.BarCustomDrawEventHandler(this.MenuUndo_PaintMenuBar);
			// 
			// redoMenu
			// 
			this.redoMenu.Name = "redoMenu";
			this.redoMenu.Ribbon = this.ribbon;
			this.redoMenu.PaintMenuBar += new DevExpress.XtraBars.BarCustomDrawEventHandler(this.MenuUndo_PaintMenuBar);
			// 
			// MainView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1100, 534);
			this.Controls.Add(this.ribbonStatusBar);
			this.Controls.Add(this.ribbon);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainView";
			this.Ribbon = this.ribbon;
			this.StatusBar = this.ribbonStatusBar;
			this.Text = "MainView";
			((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.undoMenu)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.redoMenu)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageMain;
		private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
		private DevExpress.XtraBars.BarButtonItem barButtonItem1;
		private DevExpress.XtraBars.BarButtonItem barButtonItem2;
		private DevExpress.XtraBars.BarButtonItem barButtonItem3;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup отрудник;
		private DevExpress.XtraBars.BarButtonItem barButtonItem4;
		private DevExpress.XtraBars.BarButtonItem barButtonItem5;
		private DevExpress.XtraBars.BarButtonItem barButtonItem6;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageGenerateDb;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
		private DevExpress.XtraBars.BarButtonItem barButtonItem7;
		private DevExpress.XtraBars.BarButtonItem btnRedo;
		private DevExpress.XtraBars.PopupMenu undoMenu;
		private DevExpress.XtraBars.PopupMenu redoMenu;
	}
}