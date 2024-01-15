using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beheerder.Model
{
    internal class Song : NotifyPropertyChanged
    {
        private string _title;
        private string _artist;
        private string _genre;
        private string _coverImagePath;
        private DateTime _releaseDate;
        private TimeSpan _length;
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
        public string Genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
                RaisePropertyChange("Genre");
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
        
        public TimeSpan Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
                RaisePropertyChange("Length");
            }

        }
        public Song(string title, string artist, string genre, DateTime releasedate, TimeSpan length, string coverImagePath = "")
        {
            this.Title = title;
            this.Artist = artist;
            this.Genre = genre;
            this.ReleaseDate = releasedate;
            this.Length = length;
            this.CoverImagePath = coverImagePath;
            //Lists.Songs.Add(this);
        }
    }
}
