using PhotoAlbums.Web.Models;
using System.Collections.Generic;

namespace PhotoAlbums.Web.ViewModels
{
    public class UserViewModel
    {
        public User User { get; }
        public Post[] Posts { get; }

        public UserViewModel(User user, Post[] posts)
        {
            User = user;
            Posts = posts;
        }
    }
}