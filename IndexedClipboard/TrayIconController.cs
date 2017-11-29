using System;
using System.Windows.Forms;
using IndexedClipboard.Properties;

namespace IndexedClipboard
{
    public class TrayIconController : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;

        public TrayIconController()
        {
            _notifyIcon = new NotifyIcon
            {
                ContextMenu = new ContextMenu(new[]
                {
                    new MenuItem("Exit", ExitApp),
                }),

                Icon = Resources.Bookmarks,
                Text = Resources.TrayIconController_Display_Indexed_Clipboard
            };
        }

        private void ExitApp(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Display()
        {
            _notifyIcon.Visible = true;
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;
        }
    }
}