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
    }
}
