using PhotoAlbums.Web.Services;
using PhotoAlbums.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PhotoAlbums.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var albums = await ApiService.GetAlbumsAsync();

            var photos = await ApiService.GetPhotosAsync(albums);

            var albumVms = albums.Select(album => new AlbumViewModel(
                album,
                photos.FirstOrDefault(photo => photo.Album?.Id == album.Id)?.ThumbnailUrl)
            );

            return View(albumVms);
        }
    }
}
