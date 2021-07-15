using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenWrap
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var minX = 0;
            var maxX = 0;
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.Bounds.Left < minX)
                {
                    minX = screen.Bounds.Left;
                }
                else if (screen.Bounds.Right > maxX)
                {
                    maxX = screen.Bounds.Right;
                }
            }

            maxX -= 1;
            
            var wrapper = new Wrapper(minX, maxX);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WrapperApplicationContext(wrapper));
        }
    }

    public class WrapperApplicationContext : ApplicationContext
    {
        private readonly Wrapper _wrapper;
        private readonly NotifyIcon _trayIcon;

        public WrapperApplicationContext (Wrapper wrapper)
        {
            _wrapper = wrapper;
            
            _trayIcon = new NotifyIcon()
            {
                Icon = Resources.Resource1.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Exit", Exit)
                }),
                Visible = true
            };
            _wrapper.Start();
        }

        private void Exit(object sender, EventArgs e)
        {
            _trayIcon.Visible = false;
            _wrapper.Stop();

            Application.Exit();
        }
    }
}