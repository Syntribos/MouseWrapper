using System.Linq;
using System.Windows.Forms;

namespace MouseWrap
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var minX = Screen.AllScreens.Select(x => x.Bounds.Left).Min();
            var maxX = Screen.AllScreens.Select(x => x.Bounds.Right).Max() - 1;

            var wrapper = new Wrapper(minX, maxX);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WrapperApplicationContext(wrapper));
        }
    }
}