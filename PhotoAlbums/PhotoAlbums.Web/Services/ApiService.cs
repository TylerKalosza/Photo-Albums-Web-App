using PhotoAlbums.Web.Models;
using PhotoAlbums.Web.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhotoAlbums.Web.Services
{
    public class ApiService
    {
        private static readonly Uri _baseAddress = new Uri("https://jsonplaceholder.typicode.com");

        /// <summary>
        /// Gets all the users from the API and deserializes them into objects.
        /// </summary>
        /// <returns>An enumerable of the User model.</returns>
        public static async Task<IEnumerable<User>> GetUsersAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                using (var response = await client.GetAsync("/users"))
                {
                    if (response.IsSuccessStatusCode == false)
                        throw new Exception((int)response.StatusCode + " - " + response.StatusCode.ToString()); // Todo: This is temporary code, replace this with proper error handling.

                    // Deserialize response into User objects.
                    return await response.Content.ReadAsAsync<IEnumerable<User>>();
                }
            }
        }

        /// <summary>
        /// Gets all the albums from the API and deserializes them into objects.
        /// </summary>
        /// <returns>An enumerable of the Album model.</returns>
        public static async Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            var users = await GetUsersAsync();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                using (var response = await client.GetAsync("/albums"))
                {
                    if (response.IsSuccessStatusCode == false)
                        throw new Exception((int)response.StatusCode + " - " + response.StatusCode.ToString()); // Todo: This is temporary code, replace this with proper error handling.

                    // Deserialize response into AlbumDto objects.
                    var albumDtos = await response.Content.ReadAsAsync<IEnumerable<AlbumDto>>();

                    // Create Album objects from the DTOs.
                    var albums = albumDtos.Select(dto => new Album
                    {
                        Id = dto.Id,
                        Title = dto.Title,
                        User = users.SingleOrDefault(u => u.Id == dto.UserId)
                    });

                    return albums;
                }
            }
        }

        /// <summary>
        /// Gets all the photos from the API and deserializes them into objects.
        /// </summary>
        /// <param name="albums">A collection of albums used to associate the photos to the albums.</param>
        /// <returns>An enumerable of the Photo model.</returns>
        public static async Task<IEnumerable<Photo>> GetPhotosAsync(IEnumerable<Album> albums)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                using (var response = await client.GetAsync($"/photos"))
                {
                    if (response.IsSuccessStatusCode == false)
                        throw new Exception((int)response.StatusCode + " - " + response.StatusCode.ToString()); // Todo: This is temporary code, replace this with proper error handling.

                    // Deserialize response into PhotoDto objects.
                    var photoDtos = await response.Content.ReadAsAsync<IEnumerable<PhotoDto>>();

                    // Create Photo objects from the DTOs.
                    var photos = photoDtos.Select(dto => new Photo
                    {
                        Id = dto.Id,
                        Title = dto.Title,
                        Url = dto.Url,
                        ThumbnailUrl = dto.ThumbnailUrl,
                        Album = albums.SingleOrDefault(a => a.Id == dto.AlbumId)
                    });

                    return photos;
                }
            }
        }
    }
}
