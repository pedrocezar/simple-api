using Newtonsoft.Json;

namespace Simple.DDD.Domain.Entities
{
    public class Nacionalidade
    {
        [JsonProperty("country")]
        public List<Country> Country { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("country_id")]
        public string CountryId { get; set; }

        [JsonProperty("probability")]
        public double Probability { get; set; }
    }
}
