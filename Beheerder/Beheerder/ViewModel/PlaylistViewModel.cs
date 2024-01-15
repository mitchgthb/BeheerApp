using Beheerder.Model;
using Beheerder.View;
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

        public ICommand AddPlaylistCommand { get; set; }
        public ICommand AddSongCommand { get; set; }
        public ICommand EditSongCommand { get; }
        public ICommand DeleteSongCommand { get; }
        public ICommand EditPlaylistCommand { get; }
        public ICommand DeletePlaylistCommand { get; }

        public PlaylistViewModel()
        {
            Songs = new ObservableCollection<Song>();
            AddPlaylistCommand = new RelayCommand(AddPlaylist);
            AddSongCommand = new RelayCommand(AddSong);
            DeleteSongCommand = new RelayCommand(DeleteSong);
            EditPlaylistCommand = new RelayCommand(EditPlaylist);
            DeletePlaylistCommand = new RelayCommand(DeletePlaylist);
        }

        private void AddPlaylist(object param)
        {
            // Implement logic to add the album using the provided properties
            var playlist = new Playlist(Title, new ObservableCollection<Song>(), CreatedOn);
            Playlists.Add(playlist);
        }

        private void AddSong(ObservableCollection<Song> param)
        {
            // Assuming the selected album is the first one, you might want to modify this logic based on your requirements
            foreach (Song song in param)
            {
                Songs.Add(song);
            }
        }

        private void DeleteSong(object parameter)
        {
            // Ensure a song is selected for deletion
            if (_selectedSong != null)
            {
                // Remove the selected song from the Songs collection
                Songs.Remove(_selectedSong);

                // Reset input fields and clear the selected song
                _selectedSong = null;
            }
        }
        private void EditPlaylist(object param)
        {
            // param should be the selected playlist
            if (_selectedPlaylist != null)
            {
                // Implement logic to open the edit view with selectedPlaylist
                // You might navigate to another view or show a dialog for editing
                // For now, just updating properties for demonstration purposes
                Title = _selectedPlaylist.Title;
                CreatedOn = _selectedPlaylist.CreatedOn;

                // Update the _selectedPlaylist
                _selectedPlaylist = null;
            }
        }

        private void DeletePlaylist(object param)
        {
            // param should be the selected playlist
            if (_selectedPlaylist != null)
            {
                // Remove the selected playlist from the Playlists collection
                Playlists.Remove(_selectedPlaylist);

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
