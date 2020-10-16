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
                        return null;

                    // Deserialize response into User objects.
                    return await response.Content.ReadAsAsync<IEnumerable<User>>();
                }
            }
        }

        /// <summary>
        /// Gets a single user from the API and deserializes it into an object.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A single User model.</returns>
        public static async Task<User> GetUserAsync(int userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                using (var response = await client.GetAsync("/users/" + userId))
                {
                    if (response.IsSuccessStatusCode == false)
                        return null;

                    // Deserialize response into a User object.
                    return await response.Content.ReadAsAsync<User>();
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
                        return null;

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
        /// Gets a single album from the API and deserializes it into an object.
        /// </summary>
        /// <param name="albumId">The ID of the album.</param>
        /// <returns>A single Album model.</returns>
        public static async Task<Album> GetAlbumAsync(int albumId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                using (var response = await client.GetAsync("/albums/" + albumId))
                {
                    if (response.IsSuccessStatusCode == false)
                        return null;

                    // Deserialize response into AlbumDto objects.
                    var albumDto = await response.Content.ReadAsAsync<AlbumDto>();

                    // Get the user for the album.
                    var user = await GetUserAsync(albumDto.UserId);

                    return new Album { Id = albumDto.Id, Title = albumDto.Title, User = user };
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

                using (var response = await client.GetAsync("/photos"))
                {
                    if (response.IsSuccessStatusCode == false)
                        return null;

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

        /// <summary>
        /// Gets all the photos for a single album from the API and deserializes them into objects.
        /// </summary>
        /// <param name="album">The album used to filter the request and associate the photos to.</param>
        /// <returns>An enumerable of the Photo model.</returns>
        public static async Task<IEnumerable<Photo>> GetPhotosAsync(Album album)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                using (var response = await client.GetAsync($"/albums/{album.Id}/photos"))
                {
                    if (response.IsSuccessStatusCode == false)
                        return null;

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
                                Album = album
                            });
                    }

                    return photos;
                }
            }
        }

        /// <summary>
        /// Gets all the posts for a single user from the API and deserializes them into objects.
        /// </summary>
        /// <param name="user">The user used to filter the request and associate the posts to.</param>
        /// <returns>An enumerable of the Post model.</returns>
        public static async Task<IEnumerable<Post>> GetPostsAsync(User user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;

                using (var response = await client.GetAsync($"/users/{user.Id}/posts"))
                {
                    if (response.IsSuccessStatusCode == false)
                        return null;

                    // Deserialize response into PhotoDto objects.
                    var postDtos = await response.Content.ReadAsAsync<IEnumerable<PostDto>>();

                    var posts = new List<Post>();

                    // Loop through DTOs and create Photo objects.
                    foreach (var postDto in postDtos)
                    {
                        posts.Add(
                            new Post
                            {
                                Id = postDto.Id,
                                User = user,
                                Title = postDto.Title,
                                Body = postDto.Body
                            });
                    }

                    return posts;
                }
            }
        }
    }
}
