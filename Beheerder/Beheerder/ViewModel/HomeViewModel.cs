using Beheerder.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Beheerder.ViewModel
{
    public class HomeViewModel : NotifyPropertyChanged
    {
        private readonly NavigationService _navigationService;

        public RelayCommand CreateSongCommand { get; }
        public RelayCommand CreateAlbumCommand { get; }
        public RelayCommand CreatePlaylistCommand { get; }
        public RelayCommand ShowSongListCommand { get; }
        public RelayCommand ShowPlaylistListCommand { get; }
        public RelayCommand ShowAlbumCommand { get; }

        public HomeViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            CreateSongCommand = new RelayCommand(CreateSong);
            CreateAlbumCommand = new RelayCommand(CreateAlbum);
            CreatePlaylistCommand = new RelayCommand(CreatePlaylist);
            ShowSongListCommand = new RelayCommand(ShowSongList);
            ShowPlaylistListCommand = new RelayCommand(ShowPlaylist);
            ShowAlbumCommand = new RelayCommand(ShowAlbum);
    }

        private void CreateSong()
        {
            _navigationService.NavigateTo("AddSong");
        }

        private void CreateAlbum()
        {
            _navigationService.NavigateTo("AddAlbum");
        }

        private void CreatePlaylist()
        {
            _navigationService.NavigateTo("CreatePlaylist");
        }

        private void ShowSongList()
        {
            _navigationService.NavigateTo("SongList");
        }

        private void ShowPlaylist()
        {
            _navigationService.NavigateTo("PlaylistList");
        }

        private void ShowAlbum()
        {
            _navigationService.NavigateTo("AlbumList");
        }
    }
}
