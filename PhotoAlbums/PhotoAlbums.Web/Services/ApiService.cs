using PhotoAlbums.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhotoAlbums.Web.Services
{
    public class ApiService
    {
        private static readonly Uri _baseAddress = new Uri("https://jsonplaceholder.typicode.com");

        /// <summary>
        /// Gets all the users from the API and deserializes them into User objects.
        /// </summary>
        /// <returns>An enumerable of the model User.</returns>
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
                    return await response.Content.ReadAsAsync<User[]>();
                }
            }
        }
    }
}