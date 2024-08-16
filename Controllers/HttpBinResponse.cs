 using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace cookie_forwarding.Controllers;

public partial class HttpBinResponse
    {
        [JsonProperty("args")]
        public Args? Args { get; set; }

        [JsonProperty("headers")]
        public Headers? Headers { get; set; }

        [JsonProperty("origin")]
        public string? Origin { get; set; }

        [JsonProperty("url")]
        public Uri? Url { get; set; }
    }

    public partial class Args
    {
    }

    public partial class Headers
    {
        [JsonProperty("Accept")]
        public string? Accept { get; set; }

        [JsonProperty("Accept-Encoding")]
        public string? AcceptEncoding { get; set; }

        [JsonProperty("Accept-Language")]
        public string? AcceptLanguage { get; set; }

        [JsonProperty("Cache-Control")]
        public string? CacheControl { get; set; }

        [JsonProperty("Cookie")]
        public string? Cookie { get; set; }

        [JsonProperty("Host")]
        public string? Host { get; set; }

        [JsonProperty("Priority")]
        public string? Priority { get; set; }

        [JsonProperty("Sec-Ch-Ua")]
        public string? SecChUa { get; set; }

        [JsonProperty("Sec-Ch-Ua-Mobile")]
        public string? SecChUaMobile { get; set; }

        [JsonProperty("Sec-Ch-Ua-Platform")]
        public string? SecChUaPlatform { get; set; }

        [JsonProperty("Sec-Fetch-Dest")]
        public string? SecFetchDest { get; set; }

        [JsonProperty("Sec-Fetch-Mode")]
        public string? SecFetchMode { get; set; }

        [JsonProperty("Sec-Fetch-Site")]
        public string? SecFetchSite { get; set; }

        [JsonProperty("Sec-Fetch-User")]
        public string? SecFetchUser { get; set; }

        [JsonProperty("Upgrade-Insecure-Requests")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? UpgradeInsecureRequests { get; set; }

        [JsonProperty("User-Agent")]
        public string? UserAgent { get; set; }

        [JsonProperty("X-Amzn-Trace-Id")]
        public string? XAmznTraceId { get; set; }
    }

    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, Converter.Settings);
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

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
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

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long?)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }