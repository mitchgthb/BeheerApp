using Beheerder.Model;
using Beheerder.View;
using Beheerder.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Beheerder.ViewModel
{
    class PlaylistViewModel : NotifyPropertyChanged
    {
        private string _title;
        private ObservableCollection<Song> _songs;
        private TimeSpan _length;
        private DateTime _createdOn;
        private ObservableCollection<Playlist> _playlists;
        private Song _selectedSong; // Keep track of the selected song for editing
        private Playlist _selectedPlaylist; // Keep track of the selected song for editing
        private ObservableCollection<Song> _selectedSongs;
        private readonly NavigationService _navigationService;


        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChange("Title");
            }

        }
        public ObservableCollection<Song> Songs
        {
            get
            {
                return _songs;
            }
            set
            {
                _songs = value;
                RaisePropertyChange("Songs");
            }

        }
        public int getSongAmount()
        {
            return Songs.Count;
        }
        public TimeSpan Length
        {
            get
            {
                foreach (Song song in Songs)
                {
                    _length += song.Length;
                }
                return _length;
            }
            set
            {
                _length = value;
                RaisePropertyChange("Length");
            }

        }
        public DateTime CreatedOn
        {
            get
            {
                return _createdOn;
            }
            set
            {
                _createdOn = value;
                RaisePropertyChange("CreatedOn");
            }
        }

        public ObservableCollection<Playlist> Playlists
        {
            get
            {
                return _playlists;
            }
            set
            {
                _playlists = value;
                RaisePropertyChange("Playlists");
            }

        }
        public ObservableCollection<Song> SelectedSongs
        {
            get => _selectedSongs;
            set
            {
                _selectedSongs = value;
                RaisePropertyChange(nameof(SelectedSongs));

                // Additional logic if needed
            }
        }

        public ICommand AddPlaylistCommand { get; set; }
        public ICommand AddSongCommand { get; set; }
        public ICommand EditSongCommand { get; }
        public ICommand DeleteSongCommand { get; }
        public ICommand EditPlaylistCommand { get; }
        public ICommand DeletePlaylistCommand { get; }
        public ICommand AddSelectedSongsToPlaylistCommand { get; }

        public PlaylistViewModel(NavigationService navigationService)
        {
            Songs = new ObservableCollection<Song>();
            AddPlaylistCommand = new RelayCommand(AddPlaylist);
            AddSongCommand = new RelayCommand(AddSongToPlaylist);
            DeleteSongCommand = new RelayCommand(DeleteSong);
            EditPlaylistCommand = new RelayCommand(EditPlaylist);
            DeletePlaylistCommand = new RelayCommand(DeletePlaylist);
            _navigationService = navigationService;
        }

        private async void AddPlaylist(object param)
        {
            // Implement logic to add the album using the provided properties
            var playlist = new Playlist(Title, new ObservableCollection<Song>(), CreatedOn);
            Playlists.Add(playlist);

            // Save the updated list of playlists to the JSON file
            await JsonHandler.Add(playlist);
        }

        //private void AddSong(ObservableCollection<Song> param)
        //{
        //    // Assuming the selected album is the first one, you might want to modify this logic based on your requirements
        //    foreach (Song song in param)
        //    {
        //        Songs.Add(song);
        //    }
        //}
        private async void AddSongToPlaylist(object parameter)
        {
            // Ensure a playlist is selected for addition
            if (_selectedPlaylist != null && parameter is ObservableCollection<Song> selectedSongs)
            {
                // Add each selected song to the Songs collection of the selected playlist
                foreach (var selectedSong in selectedSongs)
                {
                    _selectedPlaylist.Songs.Add(selectedSong);
                }

                // Save the updated list of playlists to the JSON file
                await JsonHandler.Add(Playlists);

                // Reset input fields and clear the selected songs
                //ClearInputFields();
            }
        }

        private async void DeleteSong(object parameter)
        {
            // Ensure a song is selected for deletion
            if (_selectedSong != null && _selectedPlaylist != null)
            {
                // Remove the selected song from the Songs collection of the selected playlist
                _selectedPlaylist.Songs.Remove(_selectedSong);

                // Save the updated list of playlists to the JSON file
                await JsonHandler.Add(Playlists);

                // Reset input fields and clear the selected song
                ClearInputFields();
                _selectedSong = null;
            }
        }
        private async void EditPlaylist(object parameter)
        {
            // Ensure a playlist is selected for editing
            if (_selectedPlaylist != null)
            {
                // Update the selected playlist with the edited values
                _selectedPlaylist.Title = Title;

                // Save the updated list of playlists to the JSON file
                await JsonHandler.Add(Playlists);

                // Reset input fields and clear the selected playlist
                ClearInputFields();
                _selectedPlaylist = null;
            }
        }

        private async void DeletePlaylist(object parameter)
        {
            // Ensure a playlist is selected for deletion
            if (_selectedPlaylist != null)
            {
                // Remove the selected playlist from the Playlists collection
                Playlists.Remove(_selectedPlaylist);

                // Save the updated list of playlists to the JSON file
                await JsonHandler.Add(Playlists);

                // Reset input fields and clear the selected playlist
                ClearInputFields();
                _selectedPlaylist = null;
            }
        }

        private void ClearInputFields()
        {
            Title = string.Empty;
            CreatedOn = DateTime.Today;
        }

    }
}
