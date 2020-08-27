namespace Furmanov.UI
{
	partial class MainView
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
			this.treeSalary = new BrightIdeasSoftware.TreeListView();
			this.colName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colPhone = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colPosition = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colSalary = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colRateDays = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colFactDays = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colAdvance = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colPenalty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colPremium = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colSalaryToPay = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.colComment = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.gvWorkedDays = new System.Windows.Forms.DataGridView();
			this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colIsWorked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.topMenu = new System.Windows.Forms.Panel();
			this.btReportForObject = new System.Windows.Forms.Button();
			this.btReportTotal = new System.Windows.Forms.Button();
			this.separator4 = new System.Windows.Forms.Label();
			this.btDeleteAllDays = new System.Windows.Forms.Button();
			this.btAllDays = new System.Windows.Forms.Button();
			this.btWorkDaysOnly = new System.Windows.Forms.Button();
			this.separator3 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.nudYear = new System.Windows.Forms.NumericUpDown();
			this.cbMonth = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.separator2 = new System.Windows.Forms.Label();
			this.btAddUser = new System.Windows.Forms.Button();
			this.btEditUser = new System.Windows.Forms.Button();
			this.btDeleteUser = new System.Windows.Forms.Button();
			this.separator1 = new System.Windows.Forms.Label();
			this.btReportBugTest = new System.Windows.Forms.Button();
			this.btRefillDataBase = new System.Windows.Forms.Button();
			this.mainMenu = new System.Windows.Forms.ToolStrip();
			this.lblVersion = new System.Windows.Forms.ToolStripLabel();
			this.btLogOut = new System.Windows.Forms.ToolStripButton();
			this.btLogin = new System.Windows.Forms.ToolStripButton();
			this.lblUser = new System.Windows.Forms.ToolStripLabel();
			this.btUndo = new System.Windows.Forms.ToolStripSplitButton();
			this.btRedo = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			((System.ComponentModel.ISupportInitialize)(this.treeSalary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvWorkedDays)).BeginInit();
			this.topMenu.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
			this.mainMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeSalary
			// 
			this.treeSalary.AllColumns.Add(this.colName);
			this.treeSalary.AllColumns.Add(this.colPhone);
			this.treeSalary.AllColumns.Add(this.colPosition);
			this.treeSalary.AllColumns.Add(this.colSalary);
			this.treeSalary.AllColumns.Add(this.colRateDays);
			this.treeSalary.AllColumns.Add(this.colFactDays);
			this.treeSalary.AllColumns.Add(this.colAdvance);
			this.treeSalary.AllColumns.Add(this.colPenalty);
			this.treeSalary.AllColumns.Add(this.colPremium);
			this.treeSalary.AllColumns.Add(this.colSalaryToPay);
			this.treeSalary.AllColumns.Add(this.colComment);
			this.treeSalary.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
			this.treeSalary.CellEditEnterChangesRows = true;
			this.treeSalary.CellEditTabChangesRows = true;
			this.treeSalary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colPhone,
            this.colPosition,
            this.colSalary,
            this.colRateDays,
            this.colFactDays,
            this.colAdvance,
            this.colPenalty,
            this.colPremium,
            this.colSalaryToPay,
            this.colComment});
			this.treeSalary.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeSalary.FullRowSelect = true;
			this.treeSalary.GridLines = true;
			this.treeSalary.HideSelection = false;
			this.treeSalary.LargeImageList = this.imageList;
			this.treeSalary.Location = new System.Drawing.Point(0, 115);
			this.treeSalary.MultiSelect = false;
			this.treeSalary.Name = "treeSalary";
			this.treeSalary.OwnerDraw = true;
			this.treeSalary.ShowGroups = false;
			this.treeSalary.Size = new System.Drawing.Size(1084, 446);
			this.treeSalary.SmallImageList = this.imageList;
			this.treeSalary.StateImageList = this.imageList;
			this.treeSalary.TabIndex = 0;
			this.treeSalary.UnfocusedHighlightBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
			this.treeSalary.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
			this.treeSalary.UseCompatibleStateImageBehavior = false;
			this.treeSalary.View = System.Windows.Forms.View.Details;
			this.treeSalary.VirtualMode = true;
			this.treeSalary.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.treeSalary_FormatRow);
			this.treeSalary.SelectedIndexChanged += new System.EventHandler(this.treeSalary_SelectedIndexChanged);
			// 
			// colName
			// 
			this.colName.AspectName = "Name";
			this.colName.FillsFreeSpace = true;
			this.colName.ImageAspectName = "Type";
			this.colName.Text = "ФИО / Наименование";
			this.colName.Width = 202;
			// 
			// colPhone
			// 
			this.colPhone.AspectName = "Phone";
			this.colPhone.Text = "Телефон";
			this.colPhone.Width = 167;
			// 
			// colPosition
			// 
			this.colPosition.AspectName = "Position";
			this.colPosition.Text = "Должность / Адрес";
			this.colPosition.Width = 250;
			// 
			// colSalary
			// 
			this.colSalary.AspectName = "Salary";
			this.colSalary.AspectToStringFormat = "{0:c2}";
			this.colSalary.Text = "Оклад";
			this.colSalary.Width = 120;
			// 
			// colRateDays
			// 
			this.colRateDays.AspectName = "RateDays";
			this.colRateDays.AspectToStringFormat = "{0:N0}";
			this.colRateDays.Text = "Норма";
			// 
			// colFactDays
			// 
			this.colFactDays.AspectName = "FactDays";
			this.colFactDays.AspectToStringFormat = "{0:N0}";
			this.colFactDays.Text = "Факт";
			// 
			// colAdvance
			// 
			this.colAdvance.AspectName = "Advance";
			this.colAdvance.AspectToStringFormat = "{0:c2}";
			this.colAdvance.Text = "Аванс";
			this.colAdvance.Width = 120;
			// 
			// colPenalty
			// 
			this.colPenalty.AspectName = "Penalty";
			this.colPenalty.AspectToStringFormat = "{0:c2}";
			this.colPenalty.Text = "Штрафы";
			this.colPenalty.Width = 120;
			// 
			// colPremium
			// 
			this.colPremium.AspectName = "Premium";
			this.colPremium.AspectToStringFormat = "{0:c2}";
			this.colPremium.Text = "Премии";
			this.colPremium.Width = 120;
			// 
			// colSalaryToPay
			// 
			this.colSalaryToPay.AspectName = "SalaryToPay";
			this.colSalaryToPay.AspectToStringFormat = "{0:c2}";
			this.colSalaryToPay.Text = "К выплате";
			this.colSalaryToPay.Width = 120;
			// 
			// colComment
			// 
			this.colComment.AspectName = "Comment";
			this.colComment.Text = "Комментарий";
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "project-16.png");
			this.imageList.Images.SetKeyName(1, "object-16.png");
			this.imageList.Images.SetKeyName(2, "pay-16.png");
			this.imageList.Images.SetKeyName(3, "summary-16.png");
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.SystemColors.Control;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(1079, 115);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(5, 446);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// gvWorkedDays
			// 
			this.gvWorkedDays.AllowUserToAddRows = false;
			this.gvWorkedDays.AllowUserToDeleteRows = false;
			this.gvWorkedDays.AllowUserToOrderColumns = true;
			this.gvWorkedDays.AllowUserToResizeColumns = false;
			this.gvWorkedDays.AllowUserToResizeRows = false;
			this.gvWorkedDays.BackgroundColor = System.Drawing.Color.White;
			this.gvWorkedDays.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.gvWorkedDays.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gvWorkedDays.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDate,
            this.colIsWorked});
			this.gvWorkedDays.Dock = System.Windows.Forms.DockStyle.Right;
			this.gvWorkedDays.Location = new System.Drawing.Point(1084, 115);
			this.gvWorkedDays.Name = "gvWorkedDays";
			this.gvWorkedDays.RowHeadersVisible = false;
			this.gvWorkedDays.Size = new System.Drawing.Size(200, 446);
			this.gvWorkedDays.TabIndex = 2;
			this.gvWorkedDays.VirtualMode = true;
			this.gvWorkedDays.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvWorkedDays_CellContentClick);
			this.gvWorkedDays.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvWorkedDays_CellFormatting);
			this.gvWorkedDays.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gvWorkedDays_CellValueNeeded);
			// 
			// colDate
			// 
			this.colDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colDate.DataPropertyName = "Date";
			this.colDate.HeaderText = "Дата";
			this.colDate.Name = "colDate";
			this.colDate.ReadOnly = true;
			this.colDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.colDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// colIsWorked
			// 
			this.colIsWorked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colIsWorked.DataPropertyName = "IsWorked";
			this.colIsWorked.HeaderText = "Выход";
			this.colIsWorked.Name = "colIsWorked";
			this.colIsWorked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// topMenu
			// 
			this.topMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.topMenu.BackColor = System.Drawing.Color.WhiteSmoke;
			this.topMenu.Controls.Add(this.btReportForObject);
			this.topMenu.Controls.Add(this.btReportTotal);
			this.topMenu.Controls.Add(this.separator4);
			this.topMenu.Controls.Add(this.btDeleteAllDays);
			this.topMenu.Controls.Add(this.btAllDays);
			this.topMenu.Controls.Add(this.btWorkDaysOnly);
			this.topMenu.Controls.Add(this.separator3);
			this.topMenu.Controls.Add(this.tableLayoutPanel1);
			this.topMenu.Controls.Add(this.separator2);
			this.topMenu.Controls.Add(this.btAddUser);
			this.topMenu.Controls.Add(this.btEditUser);
			this.topMenu.Controls.Add(this.btDeleteUser);
			this.topMenu.Controls.Add(this.separator1);
			this.topMenu.Controls.Add(this.btReportBugTest);
			this.topMenu.Controls.Add(this.btRefillDataBase);
			this.topMenu.Dock = System.Windows.Forms.DockStyle.Top;
			this.topMenu.Location = new System.Drawing.Point(0, 25);
			this.topMenu.Name = "topMenu";
			this.topMenu.Size = new System.Drawing.Size(1284, 90);
			this.topMenu.TabIndex = 3;
			// 
			// btReportForObject
			// 
			this.btReportForObject.AutoSize = true;
			this.btReportForObject.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btReportForObject.Dock = System.Windows.Forms.DockStyle.Left;
			this.btReportForObject.Enabled = false;
			this.btReportForObject.FlatAppearance.BorderSize = 0;
			this.btReportForObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btReportForObject.Image = global::Furmanov.UI.Properties.Resources.Report;
			this.btReportForObject.Location = new System.Drawing.Point(982, 0);
			this.btReportForObject.Name = "btReportForObject";
			this.btReportForObject.Size = new System.Drawing.Size(96, 90);
			this.btReportForObject.TabIndex = 17;
			this.btReportForObject.Text = "Ведомость\r\nпо объекту";
			this.btReportForObject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btReportForObject.UseVisualStyleBackColor = true;
			this.btReportForObject.Click += new System.EventHandler(this.ShowNoImplementedCode);
			// 
			// btReportTotal
			// 
			this.btReportTotal.AutoSize = true;
			this.btReportTotal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btReportTotal.Dock = System.Windows.Forms.DockStyle.Left;
			this.btReportTotal.FlatAppearance.BorderSize = 0;
			this.btReportTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btReportTotal.Image = global::Furmanov.UI.Properties.Resources.ReportAll;
			this.btReportTotal.Location = new System.Drawing.Point(889, 0);
			this.btReportTotal.Name = "btReportTotal";
			this.btReportTotal.Size = new System.Drawing.Size(93, 90);
			this.btReportTotal.TabIndex = 16;
			this.btReportTotal.Text = "Сводная\r\nведомость";
			this.btReportTotal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btReportTotal.UseVisualStyleBackColor = true;
			this.btReportTotal.Click += new System.EventHandler(this.ShowNoImplementedCode);
			// 
			// separator4
			// 
			this.separator4.BackColor = System.Drawing.Color.DarkGray;
			this.separator4.Dock = System.Windows.Forms.DockStyle.Left;
			this.separator4.Location = new System.Drawing.Point(888, 0);
			this.separator4.Name = "separator4";
			this.separator4.Size = new System.Drawing.Size(1, 90);
			this.separator4.TabIndex = 15;
			// 
			// btDeleteAllDays
			// 
			this.btDeleteAllDays.AutoSize = true;
			this.btDeleteAllDays.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btDeleteAllDays.Dock = System.Windows.Forms.DockStyle.Left;
			this.btDeleteAllDays.Enabled = false;
			this.btDeleteAllDays.FlatAppearance.BorderSize = 0;
			this.btDeleteAllDays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btDeleteAllDays.Image = global::Furmanov.UI.Properties.Resources.NoDays;
			this.btDeleteAllDays.Location = new System.Drawing.Point(813, 0);
			this.btDeleteAllDays.Name = "btDeleteAllDays";
			this.btDeleteAllDays.Size = new System.Drawing.Size(75, 90);
			this.btDeleteAllDays.TabIndex = 14;
			this.btDeleteAllDays.Text = "Удалить\r\nдни";
			this.btDeleteAllDays.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btDeleteAllDays.UseVisualStyleBackColor = true;
			this.btDeleteAllDays.Click += new System.EventHandler(this.btDeleteAllDays_Click);
			// 
			// btAllDays
			// 
			this.btAllDays.AutoSize = true;
			this.btAllDays.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btAllDays.Dock = System.Windows.Forms.DockStyle.Left;
			this.btAllDays.Enabled = false;
			this.btAllDays.FlatAppearance.BorderSize = 0;
			this.btAllDays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btAllDays.Image = global::Furmanov.UI.Properties.Resources.AllDays;
			this.btAllDays.Location = new System.Drawing.Point(740, 0);
			this.btAllDays.Name = "btAllDays";
			this.btAllDays.Size = new System.Drawing.Size(73, 90);
			this.btAllDays.TabIndex = 13;
			this.btAllDays.Text = "Все дни";
			this.btAllDays.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btAllDays.UseVisualStyleBackColor = true;
			this.btAllDays.Click += new System.EventHandler(this.btAllDays_Click);
			// 
			// btWorkDaysOnly
			// 
			this.btWorkDaysOnly.AutoSize = true;
			this.btWorkDaysOnly.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btWorkDaysOnly.Dock = System.Windows.Forms.DockStyle.Left;
			this.btWorkDaysOnly.Enabled = false;
			this.btWorkDaysOnly.FlatAppearance.BorderSize = 0;
			this.btWorkDaysOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btWorkDaysOnly.Image = global::Furmanov.UI.Properties.Resources.OnlyWorkDays;
			this.btWorkDaysOnly.Location = new System.Drawing.Point(699, 0);
			this.btWorkDaysOnly.Name = "btWorkDaysOnly";
			this.btWorkDaysOnly.Size = new System.Drawing.Size(41, 90);
			this.btWorkDaysOnly.TabIndex = 12;
			this.btWorkDaysOnly.Text = "5-2";
			this.btWorkDaysOnly.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btWorkDaysOnly.UseVisualStyleBackColor = true;
			this.btWorkDaysOnly.Click += new System.EventHandler(this.btWorkDaysOnly_Click);
			// 
			// separator3
			// 
			this.separator3.BackColor = System.Drawing.Color.DarkGray;
			this.separator3.Dock = System.Windows.Forms.DockStyle.Left;
			this.separator3.Location = new System.Drawing.Point(698, 0);
			this.separator3.Name = "separator3";
			this.separator3.Size = new System.Drawing.Size(1, 90);
			this.separator3.TabIndex = 11;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.nudYear, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbMonth, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(518, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(180, 90);
			this.tableLayoutPanel1.TabIndex = 10;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Right;
			this.label2.Location = new System.Drawing.Point(24, 45);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
			this.label2.Size = new System.Drawing.Size(38, 45);
			this.label2.TabIndex = 10;
			this.label2.Text = "Год";
			// 
			// nudYear
			// 
			this.nudYear.Dock = System.Windows.Forms.DockStyle.Top;
			this.nudYear.Location = new System.Drawing.Point(68, 48);
			this.nudYear.Maximum = new decimal(new int[] {
            2019,
            0,
            0,
            0});
			this.nudYear.Minimum = new decimal(new int[] {
            2019,
            0,
            0,
            0});
			this.nudYear.Name = "nudYear";
			this.nudYear.Size = new System.Drawing.Size(109, 27);
			this.nudYear.TabIndex = 8;
			this.nudYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudYear.Value = new decimal(new int[] {
            2019,
            0,
            0,
            0});
			this.nudYear.ValueChanged += new System.EventHandler(this.MonthChanged);
			// 
			// cbMonth
			// 
			this.cbMonth.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cbMonth.FormattingEnabled = true;
			this.cbMonth.Items.AddRange(new object[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"});
			this.cbMonth.Location = new System.Drawing.Point(68, 10);
			this.cbMonth.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.cbMonth.Name = "cbMonth";
			this.cbMonth.Size = new System.Drawing.Size(109, 28);
			this.cbMonth.TabIndex = 7;
			this.cbMonth.SelectedIndexChanged += new System.EventHandler(this.MonthChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Right;
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(5, 7, 0, 0);
			this.label1.Size = new System.Drawing.Size(59, 38);
			this.label1.TabIndex = 9;
			this.label1.Text = "Месяц";
			// 
			// separator2
			// 
			this.separator2.BackColor = System.Drawing.Color.DarkGray;
			this.separator2.Dock = System.Windows.Forms.DockStyle.Left;
			this.separator2.Location = new System.Drawing.Point(517, 0);
			this.separator2.Name = "separator2";
			this.separator2.Size = new System.Drawing.Size(1, 90);
			this.separator2.TabIndex = 6;
			// 
			// btAddUser
			// 
			this.btAddUser.AutoSize = true;
			this.btAddUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btAddUser.Dock = System.Windows.Forms.DockStyle.Left;
			this.btAddUser.Enabled = false;
			this.btAddUser.FlatAppearance.BorderSize = 0;
			this.btAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btAddUser.Image = global::Furmanov.UI.Properties.Resources.AddUser;
			this.btAddUser.Location = new System.Drawing.Point(419, 0);
			this.btAddUser.Name = "btAddUser";
			this.btAddUser.Size = new System.Drawing.Size(98, 90);
			this.btAddUser.TabIndex = 0;
			this.btAddUser.Text = "Удалить\r\nсотрудника";
			this.btAddUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btAddUser.UseVisualStyleBackColor = true;
			this.btAddUser.Click += new System.EventHandler(this.ShowNoImplementedCode);
			// 
			// btEditUser
			// 
			this.btEditUser.AutoSize = true;
			this.btEditUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btEditUser.Dock = System.Windows.Forms.DockStyle.Left;
			this.btEditUser.Enabled = false;
			this.btEditUser.FlatAppearance.BorderSize = 0;
			this.btEditUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btEditUser.Image = global::Furmanov.UI.Properties.Resources.EditUser;
			this.btEditUser.Location = new System.Drawing.Point(321, 0);
			this.btEditUser.Name = "btEditUser";
			this.btEditUser.Size = new System.Drawing.Size(98, 90);
			this.btEditUser.TabIndex = 1;
			this.btEditUser.Text = "Изменить\r\nсотрудника";
			this.btEditUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btEditUser.UseVisualStyleBackColor = true;
			this.btEditUser.Click += new System.EventHandler(this.ShowNoImplementedCode);
			// 
			// btDeleteUser
			// 
			this.btDeleteUser.AutoSize = true;
			this.btDeleteUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btDeleteUser.Dock = System.Windows.Forms.DockStyle.Left;
			this.btDeleteUser.Enabled = false;
			this.btDeleteUser.FlatAppearance.BorderSize = 0;
			this.btDeleteUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btDeleteUser.Image = global::Furmanov.UI.Properties.Resources.DeleteUser;
			this.btDeleteUser.Location = new System.Drawing.Point(223, 0);
			this.btDeleteUser.Name = "btDeleteUser";
			this.btDeleteUser.Size = new System.Drawing.Size(98, 90);
			this.btDeleteUser.TabIndex = 2;
			this.btDeleteUser.Text = "Создать\r\nсотрудника";
			this.btDeleteUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btDeleteUser.UseVisualStyleBackColor = true;
			this.btDeleteUser.Click += new System.EventHandler(this.ShowNoImplementedCode);
			// 
			// separator1
			// 
			this.separator1.BackColor = System.Drawing.Color.DarkGray;
			this.separator1.Dock = System.Windows.Forms.DockStyle.Left;
			this.separator1.Location = new System.Drawing.Point(222, 0);
			this.separator1.Name = "separator1";
			this.separator1.Size = new System.Drawing.Size(1, 90);
			this.separator1.TabIndex = 5;
			// 
			// btReportBugTest
			// 
			this.btReportBugTest.AutoSize = true;
			this.btReportBugTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btReportBugTest.Dock = System.Windows.Forms.DockStyle.Left;
			this.btReportBugTest.FlatAppearance.BorderSize = 0;
			this.btReportBugTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btReportBugTest.Image = global::Furmanov.UI.Properties.Resources.bug;
			this.btReportBugTest.Location = new System.Drawing.Point(125, 0);
			this.btReportBugTest.Name = "btReportBugTest";
			this.btReportBugTest.Size = new System.Drawing.Size(97, 90);
			this.btReportBugTest.TabIndex = 4;
			this.btReportBugTest.Text = "Тест отчёта\r\nо баге";
			this.btReportBugTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btReportBugTest.UseVisualStyleBackColor = true;
			this.btReportBugTest.Click += new System.EventHandler(this.btReportBugTest_Click);
			// 
			// btRefillDataBase
			// 
			this.btRefillDataBase.AutoSize = true;
			this.btRefillDataBase.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btRefillDataBase.Dock = System.Windows.Forms.DockStyle.Left;
			this.btRefillDataBase.FlatAppearance.BorderSize = 0;
			this.btRefillDataBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btRefillDataBase.Image = global::Furmanov.UI.Properties.Resources.RefillDataBase;
			this.btRefillDataBase.Location = new System.Drawing.Point(0, 0);
			this.btRefillDataBase.Name = "btRefillDataBase";
			this.btRefillDataBase.Size = new System.Drawing.Size(125, 90);
			this.btRefillDataBase.TabIndex = 3;
			this.btRefillDataBase.Text = "Сгенерировать\r\nновые данные";
			this.btRefillDataBase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btRefillDataBase.UseVisualStyleBackColor = true;
			this.btRefillDataBase.Click += new System.EventHandler(this.btRefillDataBase_Click);
			// 
			// mainMenu
			// 
			this.mainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblVersion,
            this.toolStripSeparator1,
            this.btLogOut,
            this.btLogin,
            this.lblUser,
            this.btUndo,
            this.btRedo});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(1284, 25);
			this.mainMenu.TabIndex = 4;
			this.mainMenu.Text = "toolStrip1";
			// 
			// lblVersion
			// 
			this.lblVersion.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(82, 22);
			this.lblVersion.Text = "Версия 1.0.0.0";
			// 
			// btLogOut
			// 
			this.btLogOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btLogOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btLogOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btLogOut.Name = "btLogOut";
			this.btLogOut.Size = new System.Drawing.Size(46, 22);
			this.btLogOut.Text = "Выйти";
			this.btLogOut.Click += new System.EventHandler(this.btLogOut_Click);
			// 
			// btLogin
			// 
			this.btLogin.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btLogin.Name = "btLogin";
			this.btLogin.Size = new System.Drawing.Size(44, 22);
			this.btLogin.Text = "Войти";
			this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
			// 
			// lblUser
			// 
			this.lblUser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(108, 22);
			this.lblUser.Text = "Вход не выполнен";
			// 
			// btUndo
			// 
			this.btUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btUndo.Image = global::Furmanov.UI.Properties.Resources.undo;
			this.btUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btUndo.Name = "btUndo";
			this.btUndo.Size = new System.Drawing.Size(32, 22);
			this.btUndo.Text = "toolStripSplitButton1";
			// 
			// btRedo
			// 
			this.btRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btRedo.Image = global::Furmanov.UI.Properties.Resources.redo;
			this.btRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btRedo.Name = "btRedo";
			this.btRedo.Size = new System.Drawing.Size(32, 22);
			this.btRedo.Text = "toolStripSplitButton2";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// MainView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1284, 561);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.treeSalary);
			this.Controls.Add(this.gvWorkedDays);
			this.Controls.Add(this.topMenu);
			this.Controls.Add(this.mainMenu);
			this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainView";
			this.Text = "Табель учёта рабочего времени";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.treeSalary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvWorkedDays)).EndInit();
			this.topMenu.ResumeLayout(false);
			this.topMenu.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private BrightIdeasSoftware.TreeListView treeSalary;
		private BrightIdeasSoftware.OLVColumn colName;
		private BrightIdeasSoftware.OLVColumn colPhone;
		private BrightIdeasSoftware.OLVColumn colSalary;
		private BrightIdeasSoftware.OLVColumn colPenalty;
		private BrightIdeasSoftware.OLVColumn colPosition;
		private BrightIdeasSoftware.OLVColumn colRateDays;
		private BrightIdeasSoftware.OLVColumn colFactDays;
		private BrightIdeasSoftware.OLVColumn colAdvance;
		private BrightIdeasSoftware.OLVColumn colPremium;
		private BrightIdeasSoftware.OLVColumn colSalaryToPay;
		private BrightIdeasSoftware.OLVColumn colComment;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.DataGridView gvWorkedDays;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colIsWorked;
		private System.Windows.Forms.Panel topMenu;
		private System.Windows.Forms.Button btAddUser;
		private System.Windows.Forms.Button btEditUser;
		private System.Windows.Forms.Button btDeleteUser;
		private System.Windows.Forms.Button btRefillDataBase;
		private System.Windows.Forms.Button btReportBugTest;
		private System.Windows.Forms.Label separator1;
		private System.Windows.Forms.Label separator2;
		private System.Windows.Forms.ComboBox cbMonth;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nudYear;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label separator3;
		private System.Windows.Forms.Button btDeleteAllDays;
		private System.Windows.Forms.Button btAllDays;
		private System.Windows.Forms.Button btWorkDaysOnly;
		private System.Windows.Forms.Button btReportForObject;
		private System.Windows.Forms.Button btReportTotal;
		private System.Windows.Forms.Label separator4;
		private System.Windows.Forms.ToolStrip mainMenu;
		private System.Windows.Forms.ToolStripLabel lblUser;
		private System.Windows.Forms.ToolStripButton btLogin;
		private System.Windows.Forms.ToolStripButton btLogOut;
		private System.Windows.Forms.ToolStripLabel lblVersion;
		private System.Windows.Forms.ToolStripSplitButton btUndo;
		private System.Windows.Forms.ToolStripSplitButton btRedo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}

