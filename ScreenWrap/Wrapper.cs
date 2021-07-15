using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace ScreenWrap
{
    public class Wrapper
    {
        private readonly Timer _timer;
        private int _minX;
        private int _maxX;

        public Wrapper(int minX, int maxX)
        {
            _timer = new Timer(new TimerCallback(OnElapsed));
            _minX = minX;
            _maxX = maxX;
        }

        private void OnElapsed(object state)
        {
            var point = MouseControls.GetCursorPosition();

            if (point.X <= _minX)
            {
                Cursor.Position = new Point(_maxX - 1, point.Y);
                return;
            }

            if (point.X >= _maxX)
            {
                Cursor.Position = new Point(_minX + 1, point.Y);
            }
        }

        public void Stop()
        {
            _timer.Change(-1, -1);
        }

        public void Start()
        {
            _timer.Change(10, 50);
        }
    }
}