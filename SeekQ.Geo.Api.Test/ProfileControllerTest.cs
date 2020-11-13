using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SeekQ.Geo.Api;
using SeekQ.Geo.Api.Application.City.ViewModel;
using SeekQ.Geo.Api.Application.Location.ViewModel;
using SeekQ.Geo.Api.Models;
using Xunit;

namespace SeekQ.Identity.Test
{
    public class LanguagesControllerTest : BaseIntegrationTest<Startup>
    {
        public LanguagesControllerTest(WebApplicationFactory<Startup> factory)
            : base(factory)
        {

        }

        [Theory]
        [InlineData("/api/v1/geo/profilelocation")]
        public async void GetUserLocation_getExpectedUserLocation(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var profileLocationList = JsonConvert
                               .DeserializeObject<IEnumerable<ProfileLocationViewModel>>
                                   (await response.Content.ReadAsStringAsync());

            Assert.True(profileLocationList.ToList().Count() >= 0, "get user location list");
        }

        [Theory]
        [InlineData("/api/v1/geo/profilelocation")]
        public async void CreateUserLocation_createExpectedUserLocation(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            ProfileLocationModel updateUser = new ProfileLocationModel
            {
                Id = SeedData.ID_ROW_3,
                UserId = SeedData.ID_USER_MOCK3,
                Latitud = 34.050232,
                Longitud = -118.249741,
                ZipCode = "050026",
                CityId = SeedData.ID_SAN_FRANCISCO_CITY
            };

            // Content
            var httpContent = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, httpContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var profileLocation = JsonConvert
                               .DeserializeObject<ProfileLocationModel>
                                   (await response.Content.ReadAsStringAsync());

            Assert.True(profileLocation != null, "User Location was created");
        }

        [Theory]
        [InlineData("/api/v1/geo/profilelocation")]
        public async void UpdateUserLocation_updateExpectedUserLocation(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            ProfileLocationModel updateUser = new ProfileLocationModel
            {
                Id = SeedData.ID_ROW_3,
                UserId = SeedData.ID_USER_MOCK3,
                Latitud = 34.050232,
                Longitud = -118.249741,
                ZipCode = "050027",
                CityId = SeedData.ID_SAN_FRANCISCO_CITY
            };

            // Content
            var httpContent = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, httpContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var profileLocation = JsonConvert
                               .DeserializeObject<ProfileLocationModel>
                                   (await response.Content.ReadAsStringAsync());

            Assert.True(profileLocation != null, "User Location was updated");
        }

        [Theory]
        [InlineData("/api/v1/geo/profilelocation")]
        public async void DeleteUserLocation_deleteExpectedUserLocation(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.DeleteAsync($"{url}/{SeedData.ID_ROW_3}");

            // Assert
            response.EnsureSuccessStatusCode();
            var profileLocation = JsonConvert
                               .DeserializeObject<bool>
                                   (await response.Content.ReadAsStringAsync());

            Assert.True(profileLocation == true, "User Location was deleted");
        }
    }
}