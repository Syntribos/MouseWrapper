using System.Drawing;
using System.Runtime.InteropServices;

namespace ScreenWrap
{
    public static class MouseControls
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int X;
            public int Y;

            public static implicit operator System.Drawing.Point(Point point)
            {
                return new System.Drawing.Point(point.X, point.Y);
            }
        }
        
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);
        
        public static System.Drawing.Point GetCursorPosition()
        {
            GetCursorPos(out var lpPoint);

            return lpPoint;
        }
    }
}