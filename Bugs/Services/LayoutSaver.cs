using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Furmanov.Dal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Furmanov.Services
{
	public static class LayoutSaver
	{
		private static readonly string AppFolder
			= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);

		private static bool _isReset;

		static LayoutSaver()
		{
			var dir = new DirectoryInfo(AppFolder);
			if (!dir.Exists) dir.Create();
		}


		public static void Restore(XtraForm form)
		{
			if (form is RibbonForm rf) Restore(rf.Ribbon);
			GetGridViews(form.Controls).ForEach(Restore);
			GetSplits(form.Controls).ForEach(Restore);

			var formLayout = new XmlRepository<FormLayout>(GetFileName(form)).Load();
			formLayout.Restore(form);
		}
		public static void Save(XtraForm form)
		{
			if (_isReset) return;

			if (form is RibbonForm rf) Save(rf.Ribbon);
			GetGridViews(form.Controls).ForEach(Save);
			GetSplits(form.Controls).ForEach(Save);

			try
			{
				new XmlRepository<FormLayout>(GetFileName(form))
				.Save(new FormLayout(form));
			}
			catch (IOException) { }
		}
		public static void Reset()
		{
			_isReset = true;
			if (_isReset)
			{
				var dir = new DirectoryInfo(AppFolder);
				var assembly = Assembly.GetEntryAssembly()?.GetName().Name;
				if (dir.Exists)
				{
					dir.GetFiles()
						.Where(f => f.FullName.Contains(assembly ?? throw new InvalidOperationException()))
						.ForEach(f => File.Delete(f.FullName));
				}
			}
		}

		private static string GetFileName(XtraForm form) => GetFileName(form.Name);

		#region RibbonControl
		private static void Restore(RibbonControl ribbon)
		{
			var file = GetFileName(ribbon);
			if (File.Exists(file)) ribbon.RestoreLayoutFromXml(file);
		}
		private static void Save(RibbonControl ribbon)
		{
			var file = GetFileName(ribbon);
			ribbon.SaveLayoutToXml(file);
		}
		private static string GetFileName(RibbonControl r)
		{
			return GetFileName($"{r.FindForm()?.Name}_ribbon_{r.Tag}");
		}
		#endregion

		#region GridView
		private static List<GridView> GetGridViews(IEnumerable controls)
		{
			var grids = new List<GridView>();
			foreach (Control control in controls)
			{
				if (control is GridControl gc && gc.MainView is GridView gv)
				{
					grids.Add(gv);
				}

				grids.AddRange(GetGridViews(control.Controls));
			}
			return grids;
		}
		private static void Restore(GridView gridView)
		{
			var file = GetFileName(gridView);
			if (File.Exists(file)) gridView.RestoreLayoutFromXml(file);
		}
		private static void Save(GridView gridView)
		{
			var file = GetFileName(gridView);
			gridView.SaveLayoutToXml(file);
		}
		private static string GetFileName(BaseView gv)
		{
			return GetFileName($"{gv.GridControl.FindForm()?.Name}_{gv.Name}_{gv.Tag}");
		}
		#endregion

		#region SplitContainerControl
		private static List<SplitContainerControl> GetSplits(IEnumerable controls)
		{
			var treeLists = new List<SplitContainerControl>();
			foreach (Control control in controls)
			{
				if (control is SplitContainerControl split)
				{
					treeLists.Add(split);
				}

				treeLists.AddRange(GetSplits(control.Controls));
			}
			return treeLists;
		}
		private static void Restore(SplitContainerControl split)
		{
			var file = GetFileName(split);
			if (File.Exists(file)) split.RestoreLayoutFromXml(file);
		}
		private static void Save(SplitContainerControl split)
		{
			var file = GetFileName(split);
			split.SaveLayoutToXml(file);
		}
		private static string GetFileName(SplitContainerControl sc)
		{
			return GetFileName($"{sc.FindForm()?.Name}_{sc.Name}_{sc.Tag}");
		}
		#endregion
		private static string GetFileName(string name)
		{
			var assembly = Assembly.GetEntryAssembly()?.GetName().Name;
			return Path.Combine(AppFolder, $"{assembly}_{name}.xml");
		}

		public class FormLayout
		{
			public FormLayout() { }
			public FormLayout(XtraForm form)
			{
				WindowState = form.WindowState;
				Location = form.Location;
				Size = form.Size;
			}

			public FormWindowState WindowState { get; set; }
			public Point Location { get; set; }
			public Size Size { get; set; }

			public void Restore(XtraForm form)
			{
				form.WindowState = WindowState;
				if (Size.Width > 100 && Size.Height > 100)
				{
					if (Location.X > 0 && Location.Y > 0)
						form.Location = Location;

					form.Size = Size;
				}
			}
		}
	}
}
