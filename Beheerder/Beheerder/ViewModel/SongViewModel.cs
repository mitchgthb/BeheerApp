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
    internal class SongViewModel : NotifyPropertyChanged
    {
        private Song? _song = new();
        private Song _selectedSong;

        public ObservableCollection<Song> Songs { get; } = new ObservableCollection<Song>();

        public Song SelectedSong
        {
            get => _selectedSong;
            set
            {
                _selectedSong = value;
                RaisePropertyChange(nameof(SelectedSong));

                // Update the editable fields with the values of the selected song
                if (_selectedSong != null)
                {
                    Title = _selectedSong.Title;
                    Artist = _selectedSong.Artist;
                    Genre = _selectedSong.Genre;
                    ReleaseDate = _selectedSong.ReleaseDate;
                    Length = _selectedSong.Length;
                }
            }
        }

        public string Title
        {
            get => _song.Title;
            set
            {
                _song.Title = value;
                RaisePropertyChange(Title);
            }
        }

        public string Artist
        {
            get => _song.Artist;
            set
            {
                _song.Artist = value;
                RaisePropertyChange(Artist);
            }
        }

        public string Genre
        {
            get => _song.Genre;
            set
            {
                _song.Genre = value;
                RaisePropertyChange(Genre);
            }
        }
        public string CoverImagePath
        {
            get { return _song.CoverImagePath; }
            set
            {
                _song.CoverImagePath = value;
                RaisePropertyChange(CoverImagePath);
            }
        }

        public DateTime ReleaseDate
        {
            get => _song.ReleaseDate;
            set
            {
                _song.ReleaseDate = value;
                RaisePropertyChange(nameof(ReleaseDate));
            }
        }

        public TimeSpan Length
        {
            get => _song.Length;
            set
            {
                _song.Length = value;
                RaisePropertyChange(nameof(Length));
            }
        }

        public ICommand AddSongCommand { get; }
        public ICommand EditSongCommand { get; }
        public ICommand DeleteSongCommand { get; }


        public SongViewModel()
        {
            LoadSongs(); // Load songs when the ViewModel is created
            AddSongCommand = new RelayCommand(AddSong);
            EditSongCommand = new RelayCommand(EditSong);
            DeleteSongCommand = new RelayCommand(DeleteSong);
        }
        private async void LoadSongs()
        {
            var songs = await JsonHandler.GetAll<Song>();
            foreach (var song in songs)
            {
                Songs.Add(song);
            }
        }

        private async void SaveSongs()
        {
            await JsonHandler.Add(Songs.ToList());
        }
        private void AddSong(object parameter)
        {
            // Add logic to create and add a new song
            var newSong = new Song(Title, Artist, Genre, ReleaseDate, Length);
            Songs.Add(newSong);

            SaveSongs();            //save songs in json

            // Reset input fields
            ClearInputFields();
        }

        private void EditSong(object parameter)
        {
            // Ensure a song is selected for editing
            if (SelectedSong != null)
            {
                // Update the selected song with the edited values
                SelectedSong.Title = Title;
                SelectedSong.Artist = Artist;
                SelectedSong.Genre = Genre;
                SelectedSong.ReleaseDate = ReleaseDate;
                SelectedSong.Length = Length;

                SaveSongs();            //save songs in json

                // Reset input fields and clear the selected song
                ClearInputFields();
                SelectedSong = null;
            }
        }

        private void DeleteSong(object parameter)
        {
            // Ensure a song is selected for deletion
            if (_song != null)
            {
                // Remove the selected song from the Songs collection
                Songs.Remove(_song);

                SaveSongs();            //save songs in json

                // Reset input fields and clear the selected song
                ClearInputFields();
                _song = null;
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
