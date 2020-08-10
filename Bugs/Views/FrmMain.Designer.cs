namespace Furmanov.Views
{
	partial class FrmMain
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
			_model.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.gcBugs = new DevExpress.XtraGrid.GridControl();
			this.bugBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.gvBugs = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTotalMessage = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riMessage = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.colUser = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colPrintScreen = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colSolvedDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colInfoToUser = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riMessageToUser = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
			this.colCreatedDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colIncidentCount = new DevExpress.XtraGrid.Columns.GridColumn();
			this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
			this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
			this.gcIncidents = new DevExpress.XtraGrid.GridControl();
			this.bugIncidentBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.gvIncidents = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colUser1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDateTime = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.gcBugs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bugBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvBugs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riMessage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riMessageToUser)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
			this.splitContainerControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gcIncidents)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bugIncidentBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvIncidents)).BeginInit();
			this.SuspendLayout();
			// 
			// gcBugs
			// 
			this.gcBugs.DataSource = this.bugBindingSource;
			this.gcBugs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gcBugs.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.gcBugs.Location = new System.Drawing.Point(0, 0);
			this.gcBugs.MainView = this.gvBugs;
			this.gcBugs.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.gcBugs.Name = "gcBugs";
			this.gcBugs.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riMessage,
            this.riMessageToUser});
			this.gcBugs.Size = new System.Drawing.Size(660, 553);
			this.gcBugs.TabIndex = 0;
			this.gcBugs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBugs});
			// 
			// bugBindingSource
			// 
			this.bugBindingSource.DataSource = typeof(Furmanov.Data.Data.Bug);
			// 
			// gvBugs
			// 
			this.gvBugs.Appearance.Empty.BorderColor = System.Drawing.Color.Black;
			this.gvBugs.Appearance.Empty.Options.UseBorderColor = true;
			this.gvBugs.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.gvBugs.Appearance.FocusedRow.Options.UseBackColor = true;
			this.gvBugs.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.gvBugs.Appearance.HideSelectionRow.Options.UseBackColor = true;
			this.gvBugs.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.gvBugs.Appearance.SelectedRow.Options.UseBackColor = true;
			this.gvBugs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colProject,
            this.colTotalMessage,
            this.colUser,
            this.colPrintScreen,
            this.colSolvedDate,
            this.colInfoToUser,
            this.colCreatedDate,
            this.colIncidentCount});
			this.gvBugs.DetailHeight = 280;
			this.gvBugs.FixedLineWidth = 3;
			this.gvBugs.GridControl = this.gcBugs;
			this.gvBugs.Name = "gvBugs";
			this.gvBugs.OptionsEditForm.EditFormColumnCount = 1;
			this.gvBugs.OptionsView.RowAutoHeight = true;
			this.gvBugs.OptionsView.ShowGroupPanel = false;
			this.gvBugs.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvBugs_RowCellClick);
			this.gvBugs.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvBugs_RowStyle);
			this.gvBugs.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(this.gvBugs_CalcRowHeight);
			this.gvBugs.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvBugs_FocusedRowChanged);
			this.gvBugs.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvBugs_CellValueChanged);
			this.gvBugs.DoubleClick += new System.EventHandler(this.gvBugs_DoubleClick);
			this.gvBugs.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gvBugs_ValidatingEditor);
			// 
			// colId
			// 
			this.colId.FieldName = "Id";
			this.colId.Name = "colId";
			this.colId.OptionsColumn.AllowEdit = false;
			this.colId.Visible = true;
			this.colId.VisibleIndex = 0;
			// 
			// colProject
			// 
			this.colProject.FieldName = "Project";
			this.colProject.Name = "colProject";
			this.colProject.OptionsColumn.AllowEdit = false;
			// 
			// colTotalMessage
			// 
			this.colTotalMessage.ColumnEdit = this.riMessage;
			this.colTotalMessage.FieldName = "TotalMessage";
			this.colTotalMessage.Name = "colTotalMessage";
			this.colTotalMessage.Visible = true;
			this.colTotalMessage.VisibleIndex = 1;
			this.colTotalMessage.Width = 93;
			// 
			// riMessage
			// 
			this.riMessage.Name = "riMessage";
			// 
			// colUser
			// 
			this.colUser.FieldName = "User";
			this.colUser.Name = "colUser";
			this.colUser.OptionsColumn.AllowEdit = false;
			this.colUser.Visible = true;
			this.colUser.VisibleIndex = 3;
			// 
			// colPrintScreen
			// 
			this.colPrintScreen.FieldName = "PrintScreen";
			this.colPrintScreen.Name = "colPrintScreen";
			this.colPrintScreen.OptionsColumn.AllowEdit = false;
			this.colPrintScreen.OptionsEditForm.RowSpan = 20;
			this.colPrintScreen.OptionsEditForm.UseEditorColRowSpan = false;
			this.colPrintScreen.Visible = true;
			this.colPrintScreen.VisibleIndex = 2;
			// 
			// colSolvedDate
			// 
			this.colSolvedDate.FieldName = "SolvedDate";
			this.colSolvedDate.Name = "colSolvedDate";
			this.colSolvedDate.Visible = true;
			this.colSolvedDate.VisibleIndex = 4;
			// 
			// colInfoToUser
			// 
			this.colInfoToUser.ColumnEdit = this.riMessageToUser;
			this.colInfoToUser.FieldName = "InfoToUser";
			this.colInfoToUser.Name = "colInfoToUser";
			this.colInfoToUser.Visible = true;
			this.colInfoToUser.VisibleIndex = 5;
			// 
			// riMessageToUser
			// 
			this.riMessageToUser.Name = "riMessageToUser";
			// 
			// colCreatedDate
			// 
			this.colCreatedDate.FieldName = "CreatedDate";
			this.colCreatedDate.Name = "colCreatedDate";
			this.colCreatedDate.OptionsColumn.AllowEdit = false;
			this.colCreatedDate.Visible = true;
			this.colCreatedDate.VisibleIndex = 6;
			// 
			// colIncidentCount
			// 
			this.colIncidentCount.FieldName = "IncidentCount";
			this.colIncidentCount.Name = "colIncidentCount";
			this.colIncidentCount.OptionsColumn.AllowEdit = false;
			this.colIncidentCount.Visible = true;
			this.colIncidentCount.VisibleIndex = 7;
			// 
			// ribbonPage2
			// 
			this.ribbonPage2.Name = "ribbonPage2";
			this.ribbonPage2.Text = "ribbonPage2";
			// 
			// splitContainerControl1
			// 
			this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
			this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
			this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.splitContainerControl1.Name = "splitContainerControl1";
			this.splitContainerControl1.Panel1.Controls.Add(this.gcBugs);
			this.splitContainerControl1.Panel1.Text = "Panel1";
			this.splitContainerControl1.Panel2.Controls.Add(this.gcIncidents);
			this.splitContainerControl1.Panel2.Text = "Panel2";
			this.splitContainerControl1.Size = new System.Drawing.Size(884, 553);
			this.splitContainerControl1.SplitterPosition = 212;
			this.splitContainerControl1.TabIndex = 1;
			// 
			// gcIncidents
			// 
			this.gcIncidents.DataSource = this.bugIncidentBindingSource;
			this.gcIncidents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gcIncidents.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.gcIncidents.Location = new System.Drawing.Point(0, 0);
			this.gcIncidents.MainView = this.gvIncidents;
			this.gcIncidents.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.gcIncidents.Name = "gcIncidents";
			this.gcIncidents.Size = new System.Drawing.Size(212, 553);
			this.gcIncidents.TabIndex = 0;
			this.gcIncidents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvIncidents});
			// 
			// bugIncidentBindingSource
			// 
			this.bugIncidentBindingSource.DataSource = typeof(Furmanov.Data.Data.BugIncidentDto);
			// 
			// gvIncidents
			// 
			this.gvIncidents.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUser1,
            this.colDateTime});
			this.gvIncidents.DetailHeight = 280;
			this.gvIncidents.FixedLineWidth = 3;
			this.gvIncidents.GridControl = this.gcIncidents;
			this.gvIncidents.Name = "gvIncidents";
			this.gvIncidents.OptionsBehavior.Editable = false;
			this.gvIncidents.OptionsView.ShowGroupPanel = false;
			// 
			// colUser1
			// 
			this.colUser1.FieldName = "User";
			this.colUser1.Name = "colUser1";
			this.colUser1.Visible = true;
			this.colUser1.VisibleIndex = 0;
			// 
			// colDateTime
			// 
			this.colDateTime.FieldName = "DateTime";
			this.colDateTime.Name = "colDateTime";
			this.colDateTime.Visible = true;
			this.colDateTime.VisibleIndex = 1;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 553);
			this.Controls.Add(this.splitContainerControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.Name = "FrmMain";
			this.Text = "Баги";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.gcBugs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bugBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvBugs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riMessage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riMessageToUser)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
			this.splitContainerControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gcIncidents)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bugIncidentBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvIncidents)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gcBugs;
		private DevExpress.XtraGrid.Views.Grid.GridView gvBugs;
		private System.Windows.Forms.BindingSource bugBindingSource;
		private DevExpress.XtraGrid.Columns.GridColumn colId;
		private DevExpress.XtraGrid.Columns.GridColumn colUser;
		private DevExpress.XtraGrid.Columns.GridColumn colPrintScreen;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
		private DevExpress.XtraGrid.Columns.GridColumn colIncidentCount;
		private DevExpress.XtraGrid.Columns.GridColumn colProject;
		private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit riMessage;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
		private DevExpress.XtraGrid.GridControl gcIncidents;
		private DevExpress.XtraGrid.Views.Grid.GridView gvIncidents;
		private System.Windows.Forms.BindingSource bugIncidentBindingSource;
		private DevExpress.XtraGrid.Columns.GridColumn colDateTime;
		private DevExpress.XtraGrid.Columns.GridColumn colInfoToUser;
		private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit riMessageToUser;
		private DevExpress.XtraGrid.Columns.GridColumn colUser1;
		private DevExpress.XtraGrid.Columns.GridColumn colTotalMessage;
		private DevExpress.XtraGrid.Columns.GridColumn colSolvedDate;
		private DevExpress.XtraGrid.Columns.GridColumn colCreatedDate;
	}
}

