using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SC.Common.Services
{
	public static class TaskbarProgress
	{
		public enum TaskbarStates
		{
			NoProgress = 0,
			Indeterminate = 0x1,
			Normal = 0x2,
			Error = 0x4,
			Paused = 0x8
		}

		[ComImport()]
		[Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		private interface ITaskbarList3
		{
			// ITaskbarList
			[PreserveSig]
			void HrInit();
			[PreserveSig]
			void AddTab(IntPtr hwnd);
			[PreserveSig]
			void DeleteTab(IntPtr hwnd);
			[PreserveSig]
			void ActivateTab(IntPtr hwnd);
			[PreserveSig]
			void SetActiveAlt(IntPtr hwnd);

			// ITaskbarList2
			[PreserveSig]
			void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

			// ITaskbarList3
			[PreserveSig]
			void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
			[PreserveSig]
			void SetProgressState(IntPtr hwnd, TaskbarStates state);
		}

		[Guid("56FDF344-FD6D-11d0-958A-006097C9A090")]
		[ClassInterface(ClassInterfaceType.None)]
		[ComImport()]
		private class TaskbarInstance
		{
		}

		private static ITaskbarList3 taskbarInstance = (ITaskbarList3)new TaskbarInstance();
		private static bool taskbarSupported = Environment.OSVersion.Version >= new Version(6, 1);

		public static void SetState(TaskbarStates taskbarState, Control form = null)
		{
			if (!taskbarSupported) return;
			var (isError, handle) = TryGetHandle(form);
			if (isError) return; 
			taskbarInstance.SetProgressState(handle, taskbarState);
		}

		public static void Start() => SetValue(0, 1);
		public static void Error(Control form = null)
		{
			SetValue(1, 1, form);
			SetState(TaskbarStates.Error, form);
		}

		public static void SetValue(double progress, double max, Control form = null)
		{
			if (!taskbarSupported) return;
			var (isError, handle) = TryGetHandle(form);
			if (isError) return;
			taskbarInstance.SetProgressValue(handle, (ulong)progress, (ulong)max);
		}
		public static void Finish(Control form = null)
		{
			if (!taskbarSupported) return;
			var (isError, handle) = TryGetHandle(form);
			if(isError) return;
			taskbarInstance.SetProgressState(handle, TaskbarStates.NoProgress);
		}

		private static (bool isError, IntPtr handle) TryGetHandle(Control form)
		{
			if (form == null) return (false, Process.GetCurrentProcess().MainWindowHandle);
			if (form.Disposing || form.IsDisposed || !form.IsHandleCreated)
			{
				return (true, new IntPtr());
			}
			return (false, form.Handle);
		}
	}
}