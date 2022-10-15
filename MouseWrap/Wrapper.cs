using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace MouseWrap
{
    public class Wrapper
    {
        private readonly Timer _wrapTimer;
        private readonly Timer _reevalTimer;
        private int _minX;
        private int _maxX;

        public Wrapper(int minX, int maxX)
        {
            _wrapTimer = new Timer(OnWrapElapsed);
            _reevalTimer = new Timer(OnReevalElapsed);
            _minX = minX;
            _maxX = maxX;
        }

        private void OnReevalElapsed(object state)
        {
            _minX = Screen.AllScreens.Select(x => x.Bounds.Left).Min();
            _maxX = Screen.AllScreens.Select(x => x.Bounds.Right).Max() - 1;
        }

        private void OnWrapElapsed(object state)
        {
            if ((MouseControls.GetAsyncKeyState(0x01) & 0x8000) != 0)
            {
                return;
            }

            var point = MouseControls.GetCursorPosition();

            if (point.X <= _minX)
            {
                MouseControls.SetCursorPosition(_maxX - 1, point.Y);
                return;
            }

            if (point.X >= _maxX)
            {
                MouseControls.SetCursorPosition(_minX + 1, point.Y);
            }
        }

        public void Stop()
        {
            _wrapTimer.Change(-1, -1);
            _reevalTimer.Change(-1, -1);
        }

        public void Start()
        {
            _wrapTimer.Change(10, 50);
            _reevalTimer.Change(5000, 5000);
        }
    }
}