using System;
using NetTopologySuite.Geometries;

namespace SeekQ.Geo.Api.Application.Location.ViewModel
{
    public class ProfileLocationViewModel
    {
        public Guid UserId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string ZipCode { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
    }
}
