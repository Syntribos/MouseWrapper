using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseWrap
{
    public static class MouseControls
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }
        
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);
        
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(ushort virtualKeyCode);
        
        public static Point GetCursorPosition()
        {
            GetCursorPos(out var lpPoint);

            return lpPoint;
        }

        public static void SetCursorPosition(int x, int y)
        {
            Cursor.Position = new Point(x, y);
        }
    }
}