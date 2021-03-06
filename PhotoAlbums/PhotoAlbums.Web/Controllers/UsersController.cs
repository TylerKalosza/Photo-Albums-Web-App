﻿using PhotoAlbums.Web.Services;
using PhotoAlbums.Web.ViewModels;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PhotoAlbums.Web.Controllers
{
    public class UsersController : Controller
    {
        [Route("users/{id}")]
        public async Task<ActionResult> Show(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = await ApiService.GetUserAsync((int)id);
            if (user == null) return RedirectToAction("Index", "Error");

            var posts = await ApiService.GetPostsAsync(user);
            if (posts == null) return RedirectToAction("Index", "Error");

            // Create the UserViewModel that will be used in the view.
            var userVm = new UserViewModel(user, posts.ToArray());

            return View(userVm);
        }
    }
}