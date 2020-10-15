using PhotoAlbums.Web.Models;

namespace PhotoAlbums.Web.ViewModels
{
    public class AlbumViewModel
    {
        public Album Album { get; }
        public string ThumbnailUrl { get; }

        public AlbumViewModel(Album album, string thumbnailUrl)
        {
            Album = album;
            ThumbnailUrl = thumbnailUrl;
        }
    }
}