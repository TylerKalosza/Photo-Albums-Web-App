using PhotoAlbums.Web.Models;

namespace PhotoAlbums.Web.ViewModels
{
    public class AlbumThumbnailViewModel
    {
        public Album Album { get; }
        public string ThumbnailUrl { get; }

        public AlbumThumbnailViewModel(Album album, string thumbnailUrl)
        {
            Album = album;
            ThumbnailUrl = thumbnailUrl;
        }
    }
}