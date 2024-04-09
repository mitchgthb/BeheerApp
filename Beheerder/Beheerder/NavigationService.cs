using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Beheerder
{
    public class NavigationService
    {
        private readonly Dictionary<string, UserControl> _views = new Dictionary<string, UserControl>();
        private ContentControl _contentControl;

        public NavigationService(ContentControl contentControl)
        {
            _contentControl = contentControl;
        }

        public void RegisterView(string key, UserControl view)
        {
            if (!_views.ContainsKey(key))
                _views.Add(key, view);
        }

        public void NavigateTo(string key)
        {
            if (_views.ContainsKey(key))
            {
                _contentControl.Content = _views[key];
            }
            else
            {
                throw new ArgumentException($"View with key '{key}' not registered.");
            }
        }
    }
}
