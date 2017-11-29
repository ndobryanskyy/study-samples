using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IndexedClipboard
{
    public class ClipboardManager
    {
        private readonly Dictionary<int, object> _clipboardStore
            = new Dictionary<int, object>();

        public void StoreData(int key)
        {
            var clipboardObject = Clipboard.GetDataObject();
            var format = clipboardObject?.GetFormats(false).FirstOrDefault();

            if (format != null)
            {
                _clipboardStore[key] = clipboardObject.GetData(format);
            }
        }

        public void SetData(int key)
        {
            if (_clipboardStore.TryGetValue(key, out var data))
            {
                Clipboard.SetDataObject(data, true);
            }
        }
    }
}