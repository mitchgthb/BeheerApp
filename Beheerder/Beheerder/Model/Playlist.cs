using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beheerder.Model
{
    internal class Playlist : NotifyPropertyChanged
    {
        private string _title;
        private ObservableCollection<Song> _songs;
        private TimeSpan _length;
        private DateTime _createdOn;
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

        public Playlist(string title, ObservableCollection<Song> songs, DateTime createdOn)
        {
            this.Title = title;
            this.Songs = songs;
            this.CreatedOn = createdOn;
            Lists.Playlists.Add(this);
        }
        public void addSong(Song song)
        {
            Songs.Append(song);
        }
        public void removeSong(string songTitle)
        {
            foreach (Song song in Songs)
            {
                if (!(song.Title == songTitle))
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
