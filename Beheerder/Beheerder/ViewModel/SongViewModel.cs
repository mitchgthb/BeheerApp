using Beheerder.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Beheerder.ViewModel
{
    internal class SongViewModel : NotifyPropertyChanged
    {
        private string _title;
        private string _artist;
        private string _genre;
        private string _coverImagePath;
        private DateTime _releaseDate;
        private TimeSpan _length;
        private Song _selectedSong; // Keep track of the selected song for editing
        public ObservableCollection<Song> Songs { get; } = new ObservableCollection<Song>();

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChange(Title);
            }
        }

        public string Artist
        {
            get => _artist;
            set
            {
                _artist = value;
                RaisePropertyChange(Artist);
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
                RaisePropertyChange(Genre);
            }
        }
        public string CoverImagePath
        {
            get { return _coverImagePath; }
            set
            {
                _coverImagePath = value;
                RaisePropertyChange("CoverImagePath");
            }
        }

        public DateTime ReleaseDate
        {
            get => _releaseDate;
            set
            {
                _releaseDate = value;
                RaisePropertyChange(nameof(ReleaseDate));
            }
        }

        public TimeSpan Length
        {
            get => _length;
            set
            {
                _length = value;
                RaisePropertyChange(nameof(Length));
            }
        }

        public ICommand AddSongCommand { get; }
        public ICommand EditSongCommand { get; }
        public ICommand DeleteSongCommand { get; }


        public SongViewModel()
        {
            AddSongCommand = new RelayCommand(AddSong);
            EditSongCommand = new RelayCommand(EditSong);
            DeleteSongCommand = new RelayCommand(DeleteSong);
        }

        private void AddSong(object parameter)
        {
            // Add logic to create and add a new song
            var newSong = new Song(Title, Artist, Genre, ReleaseDate, Length);
            Songs.Add(newSong);

            // Reset input fields
            ClearInputFields();
        }

        private void EditSong(object parameter)
        {
            // Ensure a song is selected for editing
            if (_selectedSong != null)
            {
                // Update the selected song with the edited values
                _selectedSong.Title = Title;
                _selectedSong.Artist = Artist;
                _selectedSong.Genre = Genre;
                _selectedSong.ReleaseDate = ReleaseDate;
                _selectedSong.Length = Length;

                // Reset input fields and clear the selected song
                ClearInputFields();
                _selectedSong = null;
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
                ClearInputFields();
                _selectedSong = null;
            }
        }
        private void ClearInputFields()
        {
            Title = string.Empty;
            Artist = string.Empty;
            Genre = string.Empty;
            ReleaseDate = DateTime.Today;
            Length = TimeSpan.Zero;
        }
    }
}
