using System.Web.Mvc;

namespace PhotoAlbums.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}