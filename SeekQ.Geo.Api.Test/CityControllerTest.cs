using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SeekQ.Geo.Api;
using SeekQ.Geo.Api.Application.City.ViewModel;
using Xunit;

namespace SeekQ.Identity.Test
{
    public class CityControllerTest : BaseIntegrationTest<Startup>
    {
        public CityControllerTest(WebApplicationFactory<Startup> factory)
            : base(factory)
        {

        }

        [Theory]
        [InlineData("/api/v1/geo/city")]
        public async void GetCities_getExpectedCities(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{url}/{SeedData.ID_CALIFORNIA_STATE}");

            // Assert
            response.EnsureSuccessStatusCode();
            var cityList = JsonConvert
                               .DeserializeObject<IEnumerable<CityViewModel>>
                                   (await response.Content.ReadAsStringAsync());

            Assert.True(cityList.ToList().Count > 0, "get city list");
        }
    }
}