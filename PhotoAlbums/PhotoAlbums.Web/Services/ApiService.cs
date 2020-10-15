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

                    var albums = new List<Album>();

                    // Loop through DTOs and create Album objects.
                    foreach (var albumDto in albumDtos)
                    {
                        albums.Add(
                            new Album
                            {
                                Id = albumDto.Id,
                                Title = albumDto.Title,
                                User = users.SingleOrDefault(x => x.Id == albumDto.UserId)
                            });
                    }

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

                    var photos = new List<Photo>();

                    // Loop through DTOs and create Photo objects.
                    foreach (var photoDto in photoDtos)
                    {
                        photos.Add(
                            new Photo
                            {
                                Id = photoDto.Id,
                                Title = photoDto.Title,
                                Url = photoDto.Url,
                                ThumbnailUrl = photoDto.ThumbnailUrl,
                                Album = albums.SingleOrDefault(x => x.Id == photoDto.AlbumId)
                            });
                    }

                    return photos;
                }
            }
        }
    }
}
