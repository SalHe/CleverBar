using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleverBar
{
    internal class WinTaskBarAutoHider : ITaskBarAutoHider
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AppBarData
        {
            public int cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public uint uEdge;
            public RECT rect;
            public int lParam;
        }

        private enum ABM : int
        {
            ABM_NEW = 0,
            ABM_REMOVE,
            ABM_QUERYPOS,
            ABM_SETPOS,
            ABM_GETSTATE,
            ABM_GETTASKBARPOS,
            ABM_ACTIVATE,
            ABM_GETAUTOHIDEBAR,
            ABM_SETAUTOHIDEBAR,
            ABM_WINDOWPOSCHANGED,
            ABM_SETSTATE
        }

        private enum ABS : int
        {
            ABS_AUTOHIDE = 0x0000001,
            ABS_ALWAYSONTOP = 0x0000002
        }

        [DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "SHAppBarMessage")]
        private static extern uint SHAppBarMessage(ABM dwMessage, ref AppBarData pData);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


        private const int ABM_GETAUTOHIDEBAR = 0x00000007;
        private const int ABM_SETAUTOHIDEBAR = 0x00000008;

        private AppBarData PrepareAppBarData()
        {
            AppBarData appBarData = new AppBarData();
            appBarData.hWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
            appBarData.cbSize = Marshal.SizeOf<AppBarData>();
            return appBarData;
        }

        public bool IsAutoHideMode()
        {
            var appBarData = PrepareAppBarData();
            return (SHAppBarMessage(ABM.ABM_GETSTATE, ref appBarData) & ((uint)ABS.ABS_AUTOHIDE)) != 0;
        }

        public bool TurnOffAutoHide()
        {
            var appBarData = PrepareAppBarData();
            var oldState = SHAppBarMessage(ABM.ABM_GETSTATE, ref appBarData);
            appBarData.lParam = (int)(oldState & ~(uint)ABS.ABS_AUTOHIDE);
            return SHAppBarMessage(ABM.ABM_SETSTATE, ref appBarData) != 0;
        }

        public bool TurnOnAutoHide()
        {
            var appBarData = PrepareAppBarData();
            var oldState = SHAppBarMessage(ABM.ABM_GETSTATE, ref appBarData);
            appBarData.lParam = (int)(oldState | (uint)ABS.ABS_AUTOHIDE);
            return SHAppBarMessage(ABM.ABM_SETSTATE, ref appBarData) != 0;
        }

    }
}
