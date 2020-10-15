namespace PhotoAlbums.Web.Models
{
    public class Album
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
    }
}