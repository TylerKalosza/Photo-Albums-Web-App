using PhotoAlbums.Web.Services;
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

            return View(user);
        }
    }
}