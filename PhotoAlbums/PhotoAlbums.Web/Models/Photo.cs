namespace PhotoAlbums.Web.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public Album Album { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}