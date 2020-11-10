namespace SeekQ.Geo.Api.Models
{
    public class StateModel
    {
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }

        public StateModel(string stateId, string stateName, string zipCode)
        {
            StateId = stateId;
            StateName = stateName;
            ZipCode = zipCode;
        }
    }
}
