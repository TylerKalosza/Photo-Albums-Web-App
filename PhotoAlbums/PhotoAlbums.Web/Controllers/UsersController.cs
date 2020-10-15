using PhotoAlbums.Web.Services;
using PhotoAlbums.Web.ViewModels;
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
            var posts = await ApiService.GetPostsAsync(user);

            // Create the UserViewModel that will be used in the view.
            var userVm = new UserViewModel(user, posts);

            return View(userVm);
        }
    }
}