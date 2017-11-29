using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace IndexedClipboard
{
    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var trayIcon = new TrayIconController())
            using (var hotkeysManager = new ShortcutsManager(new ClipboardManager()))
            {
                trayIcon.Display();
                hotkeysManager.Initialize();
                Application.Run();
            }
        }
    }
}
