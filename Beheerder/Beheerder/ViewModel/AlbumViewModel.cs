using Beheerder.Model;
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
    class AlbumViewModel : NotifyPropertyChanged
    {
        private string _albumTitle;
        private string _albumArtist;
        private string _releaseYear;
        private string _coverImagePath;
        private DateTime _albumReleaseDate;
        private ObservableCollection<Album> _albums;
        //private ObservableCollection<Song> _songs;
        private TimeSpan _length;
        private string _songTitle;
        private string _songArtist;
        private TimeSpan _songLength;
        private string _songGenre;
        private DateTime _songReleaseDate;
        private Song _selectedSong; // Keep track of the selected song for editing
        private Album _selectedAlbum; // Keep track of the selected album for editing

        public ObservableCollection<Song> Songs { get; } = new ObservableCollection<Song>();
        public ObservableCollection<Album> Albums
        {
            get => _albums; 
            set
            {
                _albums = value;
                RaisePropertyChange("Albums");
            }
        }

        public string AlbumTitle
        {
            get => _albumTitle;
            set
            {
                _albumTitle = value;
                RaisePropertyChange("AlbumTitle");
            }
        }

        public string AlbumArtist
        {
            get => _albumArtist; 
            set
            {
                _albumArtist = value;
                RaisePropertyChange("AlbumArtist");
            }
        }

        public string ReleaseYear
        {
            get => _releaseYear;
            set
            {
                _releaseYear = value;
                RaisePropertyChange("ReleaseYear");
            }
        }

        public string CoverImagePath
        {
            get => _coverImagePath;
            set
            {
                _coverImagePath = value;
                RaisePropertyChange("CoverImagePath");
            }
        }

        public DateTime AlbumReleaseDate
        {
            get => _albumReleaseDate;
            set
            {
                _albumReleaseDate = value;
                RaisePropertyChange("AlbumReleaseDate");
            }
        }
        public TimeSpan Length
        {
            get => _length;
            set
            {
                _length = value;
                RaisePropertyChange("Length");
            }
        }
        //public ObservableCollection<Song> Songs
        //{
        //    get { return _songs; }
        //    set
        //    {
        //        _songs = value;
        //        RaisePropertyChange("Songs");
        //    }
        //}
 
        public string SongTitle
        {
            get => _songTitle;
            set
            {
                _songTitle = value;
                RaisePropertyChange("SongTitle");
            }
        }

        public string SongArtist
        {
            get => _songArtist;
            set
            {
                _songArtist = value;
                RaisePropertyChange("SongArtist");
            }
        }

        public TimeSpan SongLength
        {
            get => _songLength;
            set
            {
                _songLength = value;
                RaisePropertyChange("SongLength");
            }
        }

        public string SongGenre
        {
            get => _songGenre;
            set
            {
                _songGenre = value;
                RaisePropertyChange("SongGenre");
            }
        }

        public DateTime SongReleaseDate
        {
            get => _songReleaseDate;
            set
            {
                _songReleaseDate = value;
                RaisePropertyChange("SongReleaseDate");
            }
        }

        public ICommand AddAlbumCommand { get; set; }
        public ICommand AddSongCommand { get; set; }
        public ICommand EditSongCommand { get; }
        public ICommand DeleteSongCommand { get; }

        public AlbumViewModel()
        {
            //Albums = new ObservableCollection<Album>();
            AlbumReleaseDate = DateTime.Now;  // Set default release date for the album
            AddAlbumCommand = new RelayCommand(AddAlbum);
            AddSongCommand = new RelayCommand(AddSong);
            EditSongCommand = new RelayCommand(EditSong);
            DeleteSongCommand = new RelayCommand(DeleteSong);
        }

        private async void AddAlbum(object parameter)
        {
            // Implement logic to add the album using the provided properties
            var album = new Album(AlbumTitle, AlbumArtist, new ObservableCollection<Song>(), AlbumReleaseDate, CoverImagePath);
            Albums.Add(album);

            await JsonHandler.Add(album);

            // Reset input fields
            this.AlbumTitle = string.Empty;
            this.AlbumArtist = string.Empty;
            this.ReleaseYear = string.Empty;
            this.CoverImagePath = string.Empty;
        }
        private async void EditAlbum(object parameter)
        {
            // Ensure an album is selected for editing
            if (_selectedAlbum != null)
            {
                // Update the selected album with the edited values
                _selectedAlbum.Title = AlbumTitle;
                _selectedAlbum.Artist = AlbumArtist;
                _selectedAlbum.ReleaseDate = AlbumReleaseDate;
                _selectedAlbum.CoverImagePath = CoverImagePath;

                // Save the updated list of albums to the JSON file
                await JsonHandler.Add(_selectedAlbum);

                // Reset input fields and clear the selected album
                ClearInputFields();
                _selectedAlbum = null;
            }
        }
        private async void DeleteAlbum(object parameter)
        {
            // Ensure an album is selected for deletion
            if (_selectedAlbum != null)
            {
                // Remove the selected album from the Albums collection
                Albums.Remove(_selectedAlbum);

                // Save the updated list of albums to the JSON file
                await JsonHandler.Add(Albums);

                // Reset input fields and clear the selected album
                ClearInputFields();
                _selectedAlbum = null;
            }
        }

        private void AddSong(object parameter)
        {
            // Check if the Songs collection is initialized
            //if (Songs == null)
            //{
            //    // Initialize the Songs collection if it is null
            //    Songs = new ObservableCollection<Song>();
            //}
            // Implement logic to add a song to the current album
            var song = new Song(SongTitle, SongArtist, SongGenre, SongReleaseDate, SongLength);
            // Assuming the selected album is the first one, you might want to modify this logic based on your requirements
            Songs.Add(song);

            // Reset input fieldss
            ClearInputFields();
        }
        private void EditSong(object parameter)
        {
            // Ensure a song is selected for editing
            if (_selectedSong != null)
            {
                // Update the selected song with the edited values
                _selectedSong.Title = SongTitle;
                _selectedSong.Artist = SongArtist;
                _selectedSong.Genre = SongGenre;
                _selectedSong.ReleaseDate = SongReleaseDate;
                _selectedSong.Length = SongLength;

                // Reset input fields and clear the selected song
                ClearInputFields();
                _selectedSong = null;
            }
        }

        private async void DeleteSong(object parameter)
        {
            // Ensure a song is selected for deletion
            if (_selectedSong != null && _selectedAlbum != null)
            {
                // Remove the selected song from the Songs collection of the selected album
                _selectedAlbum.Songs.Remove(_selectedSong);

                // Save the updated list of albums to the JSON file
                await JsonHandler.Add(Albums);

                // Reset input fields and clear the selected song
                ClearInputFields();
                _selectedSong = null;
            }
        }
        private void ClearInputFields()
        {
            SongTitle = string.Empty;
            SongArtist = string.Empty;
            SongGenre = string.Empty;
            SongReleaseDate = DateTime.Today;
            SongLength = TimeSpan.Zero;
        }

        public int getSongAmount()
        {
            return _selectedAlbum.Songs.Count;
        }

    }
}
