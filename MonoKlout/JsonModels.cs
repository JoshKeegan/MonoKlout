using System;
using Newtonsoft.Json;

namespace MonoKlout
{
    /*
     * This class is used as a model for deserializing the Json returned by the HttpWebRequest.
     */
    [JsonObject(MemberSerialization.OptIn)]
    public class KloutIdentityResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("network")]
        public string Network { get; set; }
    }
    
    [JsonObject(MemberSerialization.OptIn)]
    public class KloutScoreResponse
    {
        [JsonProperty("score")]
        public double Score { get; set; }
        
        [JsonProperty("scoreDelta")]
        public KloutScoreDeltaResponse ScoreDelta { get; set; }
    }
    
    [JsonObject(MemberSerialization.OptIn)]
    public class KloutScoreDeltaResponse
    {
        [JsonProperty("dayChange")]
        public double DayChange { get; set; }
        
        [JsonProperty("weekChange")]
        public double WeekChange { get; set; }
        
        [JsonProperty("monthChange")]
        public double MonthChange { get; set; }
    }

    public class KloutUserTopicsResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class KloutInfluenceResponse
    {
        [JsonProperty("myInfluencers")]
        public KloutInfluenceEntityResponse[] MyInfluencers { get; set; }

        [JsonProperty("myInfluencees")]
        public KloutInfluenceEntityResponse[] MyInfluencees { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class KloutInfluenceEntityResponse
    {
        [JsonProperty("entity")]
        public KloutInfluenceEntityDetailedResponse Entity { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class KloutInfluenceEntityDetailedResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("payload")]
        public KloutInfluencePayloadResponse Payload { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class KloutInfluencePayloadResponse
    {
        [JsonProperty("kloutId")]
        public string KloutId { get; set; }

        [JsonProperty("nick")]
        public string Nick { get; set; }

        [JsonProperty("score")]
        public KloutInfluenceScoreResponse Score { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class KloutInfluenceScoreResponse
    {
        [JsonProperty("score")]
        public string Score { get; set; }
    }
}  