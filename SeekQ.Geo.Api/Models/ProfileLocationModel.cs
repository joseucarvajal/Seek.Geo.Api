namespace SeekQ.Geo.Api.Models
{
    using System;
    using App.Common.Repository;
    using NetTopologySuite.Geometries;

    public class ProfileLocationModel : BaseEntity
    {
        public Guid UserId { get; set; }
        public Point Location { get; set; }
        public string ZipCode { get; set; }

        public string CityId { get; set; }
        public CityModel City { get; set; }

        public ProfileLocationModel() { }

        public ProfileLocationModel(Guid id, Guid userId, Point location, string zipCode, string cityId)
        {
            Id = id;
            UserId = userId;
            Location = location;
            ZipCode = zipCode;
            CityId = cityId;
        }
    }
}
