using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hunger_Guru.Models
{
    public partial class Location
    {
        [JsonProperty("location_suggestions")]
        public List<LocationSuggestion> LocationSuggestions { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("has_more")]
        public long HasMore { get; set; }

        [JsonProperty("has_total")]
        public long HasTotal { get; set; }

        [JsonProperty("user_has_addresses")]
        public bool UserHasAddresses { get; set; }
    }

    public partial class LocationSuggestion
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country_id")]
        public long CountryId { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_flag_url")]
        public Uri CountryFlagUrl { get; set; }

        [JsonProperty("should_experiment_with")]
        public long ShouldExperimentWith { get; set; }

        [JsonProperty("has_go_out_tab")]
        public long HasGoOutTab { get; set; }

        [JsonProperty("discovery_enabled")]
        public long DiscoveryEnabled { get; set; }

        [JsonProperty("has_new_ad_format")]
        public long HasNewAdFormat { get; set; }

        [JsonProperty("is_state")]
        public long IsState { get; set; }

        [JsonProperty("state_id")]
        public long StateId { get; set; }

        [JsonProperty("state_name")]
        public string StateName { get; set; }

        [JsonProperty("state_code")]
        public string StateCode { get; set; }
    }

    public partial class Locations
    {
        public static Locations FromJson(string json) => JsonConvert.DeserializeObject<Locations>(json, Converter.Settings);
    }



    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
