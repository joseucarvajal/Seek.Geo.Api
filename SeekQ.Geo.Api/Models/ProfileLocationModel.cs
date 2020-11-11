namespace SeekQ.Geo.Api.Models
{
    using System;
    using App.Common.Repository;

    public class ProfileLocationModel : BaseEntity
    {
        public Guid UserId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string ZipCode { get; set; }

        public string CityId { get; set; }
        public CityModel City { get; set; }

        public ProfileLocationModel() { }

        public ProfileLocationModel(Guid id, Guid userId, double latitud, double longitud, string zipCode, string cityId)
        {
            Id = id;
            UserId = userId;
            Latitud = latitud;
            Longitud = longitud;
            ZipCode = zipCode;
            CityId = cityId;
        }
    }
}
