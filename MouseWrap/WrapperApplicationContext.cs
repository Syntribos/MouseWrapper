using System;
using System.Windows.Forms;

namespace MouseWrap
{
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
                    new MenuItem("Pause", Pause),
                    new MenuItem("Start", Start),
                    new MenuItem("-"),
                    new MenuItem("Exit", Exit),
                }),
                Visible = true,
                Text = "Mouse Wrapper"
            };
            _wrapper.Start();
        }

        private void Pause(object sender, EventArgs e)
        {
            _wrapper.Stop();
        }

        private void Start(object sender, EventArgs e)
        {
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