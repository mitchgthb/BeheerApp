using Beheerder.View;
using Beheerder.View.Album;
using Beheerder.ViewModel;
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

namespace Beheerder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavigationService _navigationService;

        public MainWindow()
        {
            InitializeComponent();

            //DataContext = new MainViewModel();

            _navigationService = new NavigationService(MainContent);

            _navigationService.RegisterView("AddSong", new AddSong());
            _navigationService.RegisterView("AddAlbumInfo", new AddAlbumInfo());
            _navigationService.RegisterView("AddAlbum", new AddAlbum());
            _navigationService.RegisterView("AlbumList", new AlbumList());
            _navigationService.RegisterView("AlbumView", new AlbumView());
            _navigationService.RegisterView("CreatePlaylist", new CreatePlaylist());
            _navigationService.RegisterView("PlaylistList", new PlaylistList());
            _navigationService.RegisterView("PlaylistView", new PlaylistView());
            _navigationService.RegisterView("SongList", new SongList());
            _navigationService.RegisterView("SongView", new SongView());
            _navigationService.RegisterView("Home", new Home(_navigationService));
            _navigationService.RegisterView("SelectSongs", new SelectSongs());

            _navigationService.NavigateTo("Home");
        }

    }
}
