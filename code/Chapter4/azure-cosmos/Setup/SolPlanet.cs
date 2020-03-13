using Newtonsoft.Json;

namespace Setup
{
    public class SolPlanet
    {
        [JsonProperty(PropertyName = "id")]
        public string Name { get; set; }
        public double Distance { get; set; }
        public bool IsExplored { get; set; }
        public string Orbits { get; set; } = "Sol";
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public SolPlanet(string name, double dist, bool exp = false) => (Name, Distance, IsExplored) = (name, dist, exp);
    }
}
