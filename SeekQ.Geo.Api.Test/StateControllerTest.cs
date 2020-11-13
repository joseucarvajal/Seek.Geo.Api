using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SeekQ.Geo.Api;
using SeekQ.Geo.Api.Application.State.ViewModel;
using Xunit;

namespace SeekQ.Identity.Test
{
    public class ProfileControllerTest : BaseIntegrationTest<Startup>
    {
        public ProfileControllerTest(WebApplicationFactory<Startup> factory)
            : base(factory)
        {

        }

        [Theory]
        [InlineData("/api/v1/geo/state")]
        public async void GetStates_getExpectedStates(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var stateList = JsonConvert
                               .DeserializeObject<IEnumerable<StateViewModel>>
                                   (await response.Content.ReadAsStringAsync());

            Assert.True(stateList.ToList().Count > 0, "get state list");
        }
    }
}