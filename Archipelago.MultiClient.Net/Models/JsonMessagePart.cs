﻿using Archipelago.MultiClient.Net.Enums;

#if NET6_0_OR_GREATER
using JsonProperty = System.Text.Json.Serialization.JsonPropertyNameAttribute;
using System.Text.Json.Serialization;
using Archipelago.MultiClient.Net.Converters;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
#endif

namespace Archipelago.MultiClient.Net.Models
{
	public class JsonMessagePart
    {
        [JsonProperty("type")]
#if NET6_0_OR_GREATER
        [JsonConverter(typeof(JsonSnakeCaseStringEnumConverter))]
#else
		[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
#endif
		public JsonMessagePartType? Type { get; set; }

        [JsonProperty("color")]
#if NET6_0_OR_GREATER
        [JsonConverter(typeof(JsonSnakeCaseStringEnumConverter))]
#else
		[JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
#endif
		public JsonMessagePartColor? Color { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("player")]
        public int? Player { get; set; }

        [JsonProperty("flags")]
        public ItemFlags? Flags { get; set; }
    }
}