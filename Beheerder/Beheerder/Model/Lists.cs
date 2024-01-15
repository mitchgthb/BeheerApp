using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beheerder.Model
{
    static class Lists
    {
        static private List<Song> _songs;
        static private List<Album> _albums;
        static private List<Playlist> _playlists;
        static public List<Song> Songs
        {
            get
            {
                return _songs;
            }
            set
            {
                _songs = value;
            }

        }
        static public List<Album> Albums
        {
            get
            {
                return _albums;
            }
            set
            {
                _albums = value;
            }
        }
        static public List<Playlist> Playlists
        {
            get
            {
                return _playlists;
            }
            set
            {
                _playlists = value;
            }
        }
        static public void deleteSong(string songTitle)
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
                    removeSong(song.Title);
                    break;
                }
            }
        }
        static public void deleteAlbum(string albumTitle)
        {
            foreach (Album album in Albums)
            {
                if (!(album.Title == albumTitle))
                {
                    continue;
                }
                else
                {
                    Albums.Remove(album);
                    break;
                }
            }
        }
        static public void deletePlaylist(string playlistTitle)
        {
            foreach (Playlist playlist in Playlists)
            {
                if (!(playlist.Title == playlistTitle))
                {
                    continue;
                }
                else
                {
                    Playlists.Remove(playlist);
                    break;
                }
            }
        }
        static public void removeSong(string songTitle)
        {
            foreach(Album album in Albums)
            {
                album.removeSong(songTitle);
            }
            foreach(Playlist playlist in Playlists)
            {
                playlist.removeSong(songTitle);
            }
        }
    }
}
