using PagedList;
using PhotoAlbums.Web.Services;
using PhotoAlbums.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PhotoAlbums.Web.Controllers
{
    public class AlbumsController : Controller
    {
        public async Task<ActionResult> Index(string currentFilter, string searchString, int? page)
        {
            var albums = await ApiService.GetAlbumsAsync();

            if (searchString == null)
                searchString = currentFilter;
            else
                page = 1;

            ViewBag.CurrentFilter = searchString;

            // Filter the data based on the search string matching part of the album title or user's name.
            if (!string.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(album => album.Title.Contains(searchString.ToLower())
                    || (album.User != null && album.User.Name.ToLower().Contains(searchString.ToLower())));
            }

            var photos = await ApiService.GetPhotosAsync(albums);

            // Create the AlbumViewModels that will be used in the view.
            var albumVms = albums.Select(album => new AlbumViewModel(
                album,
                photos.FirstOrDefault(photo => photo.Album?.Id == album.Id)?.ThumbnailUrl)
            );

            var pageSize = 12;
            var pageNumber = page ?? 1;

            return View(albumVms.ToPagedList(pageNumber, pageSize));
        }
    }
}
