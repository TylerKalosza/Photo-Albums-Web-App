using PhotoAlbums.Web.Models;
using System.Collections.Generic;

namespace PhotoAlbums.Web.ViewModels
{
    public class UserViewModel
    {
        public User User { get; }
        public IEnumerable<Post> Posts { get; }

        public UserViewModel(User user, IEnumerable<Post> posts)
        {
            User = user;
            Posts = posts;
        }
    }
}