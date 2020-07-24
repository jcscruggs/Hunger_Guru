using Newtonsoft.Json;
using System.Collections.Generic;


namespace Hunger_Guru.Models
{
    public partial class Cuisine
    {
        [JsonProperty("cuisines")]
        public List<CuisineElement> Cuisines { get; set; }
    }

    public partial class CuisineElement
    {
        [JsonProperty("cuisine")]
        public CuisineCuisine Cuisine { get; set; }
    }

    public partial class CuisineCuisine
    {
        [JsonProperty("cuisine_id")]
        public int CuisineId { get; set; }

        [JsonProperty("cuisine_name")]
        public string CuisineName { get; set; }
    }

    public partial class Cuisine
    {
        public static Cuisine FromJson(string json) => JsonConvert.DeserializeObject<Cuisine>(json, Converter.Settings);
    }

}

