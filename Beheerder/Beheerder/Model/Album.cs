using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beheerder.Model
{
    internal class Album : NotifyPropertyChanged
    {
        private string _title;
        private ObservableCollection<Song> _songs;
        private TimeSpan _length;
        private string _artist;
        private string _coverImagePath;
        private DateTime _releaseDate;

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
        public string Artist
        {
            get 
            { 
                return _artist; 
            }
            set 
            {
                _artist = value;
                RaisePropertyChange("Artist");
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
            get
            {
                return _releaseDate;
            }
            set
            {
                _releaseDate = value;
                RaisePropertyChange("ReleaseDate");
            }
        }
        public Album(string title, string artist, ObservableCollection<Song> songs, DateTime releaseDate, string coverImagePath)
        {
            this.Title = title;
            this.Artist = artist;
            this.Songs = songs;
            this.ReleaseDate = releaseDate;
            //this.Length = length;
            this.CoverImagePath = coverImagePath;
            //Lists.Albums.Add(this);
        }

        public void addSong(Song song)
        {
            Songs.Add(song);
        }
        public void removeSong(string songTitle) 
        {
            foreach (Song song in Songs)
            {
                if(!(song.Title == songTitle))
                {
                    continue;
                }
                else
                {
                    Songs.Remove(song);
                    break;
                }
            }
        }

    }
}
