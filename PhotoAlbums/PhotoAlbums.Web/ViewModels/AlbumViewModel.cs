using PhotoAlbums.Web.Models;
using System.Collections.Generic;

namespace PhotoAlbums.Web.ViewModels
{
    public class AlbumViewModel
    {
        public Album Album { get; }
        public IEnumerable<Photo> Photos { get; }

        public AlbumViewModel(Album album, IEnumerable<Photo> photos)
        {
            Album = album;
            Photos = photos;
        }
    }
}