namespace SeekQ.Geo.Api.Models
{
    public class CityModel
    {
        public string CityId { get; set; }
        public string CityName { get; set; }

        public string StateId { get; set; }
        public StateModel State { get; set; }

        public CityModel(string cityId, string cityName, string stateId)
        {
            CityId = cityId;
            CityName = cityName;
            StateId = stateId;
        }
    }
}
