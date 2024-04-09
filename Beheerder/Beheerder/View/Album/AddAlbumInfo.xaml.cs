using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Beheerder;

namespace Beheerder.View.Album
{
    /// <summary>
    /// Interaction logic for AddAlbumInfo.xaml
    /// </summary>
    public partial class AddAlbumInfo : UserControl
    {
        private readonly NavigationService _navigationService;
        public AddAlbumInfo()
        {
            InitializeComponent();
            //_navigationService = new NavigationService(); // Initialize your NavigationService

        }
    }
}
