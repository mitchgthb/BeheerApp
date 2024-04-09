using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beheerder.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private BaseViewModel? _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set 
            { 
                _selectedViewModel = value;
                RaisePropertyChange(nameof(SelectedViewModel));
            }
            
        }
    }
}
