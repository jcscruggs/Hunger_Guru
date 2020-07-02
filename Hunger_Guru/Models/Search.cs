using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hunger_Guru.Models
{
    public partial class Search
    {
        [JsonProperty("results_found")]
        public long ResultsFound { get; set; }

        [JsonProperty("results_start")]
        public long ResultsStart { get; set; }

        [JsonProperty("results_shown")]
        public long ResultsShown { get; set; }

        [JsonProperty("restaurants")]
        public List<RestaurantElement> Restaurants { get; set; }
    }

    public partial class RestaurantElement
    {
        [JsonProperty("restaurant")]
        public RestaurantRestaurant Restaurant { get; set; }
    }

    public partial class RestaurantRestaurant
    {
        [JsonProperty("R")]
        public R R { get; set; }

        [JsonProperty("apikey")]
        public string Apikey { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("switch_to_order_menu")]
        public long SwitchToOrderMenu { get; set; }

        [JsonProperty("cuisines")]
        public string Cuisines { get; set; }

        [JsonProperty("timings")]
        public string Timings { get; set; }

        [JsonProperty("average_cost_for_two")]
        public long AverageCostForTwo { get; set; }

        [JsonProperty("price_range")]
        public long PriceRange { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("highlights")]
        public List<string> Highlights { get; set; }

        [JsonProperty("offers")]
        public List<object> Offers { get; set; }

        [JsonProperty("opentable_support")]
        public long OpentableSupport { get; set; }

        [JsonProperty("is_zomato_book_res")]
        public long IsZomatoBookRes { get; set; }

        [JsonProperty("mezzo_provider")]
        public string MezzoProvider { get; set; }

        [JsonProperty("is_book_form_web_view")]
        public long IsBookFormWebView { get; set; }

        [JsonProperty("book_form_web_view_url")]
        public string BookFormWebViewUrl { get; set; }

        [JsonProperty("book_again_url")]
        public string BookAgainUrl { get; set; }

        [JsonProperty("thumb")]
        public string Thumb { get; set; }

        [JsonProperty("user_rating")]
        public UserRating UserRating { get; set; }

        [JsonProperty("all_reviews_count")]
        public long AllReviewsCount { get; set; }

        [JsonProperty("photos_url")]
        public Uri PhotosUrl { get; set; }

        [JsonProperty("photo_count")]
        public long PhotoCount { get; set; }

        [JsonProperty("menu_url")]
        public Uri MenuUrl { get; set; }

        [JsonProperty("featured_image")]
        public string FeaturedImage { get; set; }

        [JsonProperty("has_online_delivery")]
        public long HasOnlineDelivery { get; set; }

        [JsonProperty("is_delivering_now")]
        public long IsDeliveringNow { get; set; }

        [JsonProperty("store_type")]
        public string StoreType { get; set; }

        [JsonProperty("include_bogo_offers")]
        public bool IncludeBogoOffers { get; set; }

        [JsonProperty("deeplink")]
        public string Deeplink { get; set; }

        [JsonProperty("is_table_reservation_supported")]
        public long IsTableReservationSupported { get; set; }

        [JsonProperty("has_table_booking")]
        public long HasTableBooking { get; set; }

        [JsonProperty("events_url")]
        public Uri EventsUrl { get; set; }

        [JsonProperty("phone_numbers")]
        public string PhoneNumbers { get; set; }

        [JsonProperty("all_reviews")]
        public AllReviews AllReviews { get; set; }

        [JsonProperty("establishment")]
        public List<string> Establishment { get; set; }

        [JsonProperty("establishment_types")]
        public List<object> EstablishmentTypes { get; set; }
    }

    public partial class AllReviews
    {
        [JsonProperty("reviews")]
        public List<Review> Reviews { get; set; }
    }

    public partial class Review
    {
        [JsonProperty("review")]
        public List<object> ReviewReview { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("city_id")]
        public long CityId { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("zipcode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zipcode { get; set; }

        [JsonProperty("country_id")]
        public long CountryId { get; set; }

        [JsonProperty("locality_verbose")]
        public string LocalityVerbose { get; set; }
    }

    public partial class R
    {
        [JsonProperty("has_menu_status")]
        public HasMenuStatus HasMenuStatus { get; set; }

        [JsonProperty("res_id")]
        public long ResId { get; set; }

        [JsonProperty("is_grocery_store")]
        public bool IsGroceryStore { get; set; }
    }

    public partial class HasMenuStatus
    {
        [JsonProperty("delivery")]
        public long Delivery { get; set; }

        [JsonProperty("takeaway")]
        public long Takeaway { get; set; }
    }

    public partial class UserRating
    {
        [JsonProperty("aggregate_rating")]
        public string AggregateRating { get; set; }

        [JsonProperty("rating_text")]
        public string RatingText { get; set; }

        [JsonProperty("rating_color")]
        public string RatingColor { get; set; }

        [JsonProperty("rating_obj")]
        public RatingObj RatingObj { get; set; }

        [JsonProperty("votes")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Votes { get; set; }
    }

    public partial class RatingObj
    {
        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("bg_color")]
        public BgColor BgColor { get; set; }
    }

    public partial class BgColor
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tint")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Tint { get; set; }
    }

    public partial class Title
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class Search
    {
        public static Search FromJson(string json) => JsonConvert.DeserializeObject<Search>(json, Converter.Settings);
    }


    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
