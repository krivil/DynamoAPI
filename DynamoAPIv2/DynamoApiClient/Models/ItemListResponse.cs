﻿using Newtonsoft.Json;

namespace DynamoApiClient.Models
{
    public class ItemListResponse : TypedResponse<Item[]>
    {
        [JsonProperty("total")]
        public long? Total { get; set; }
    }
}