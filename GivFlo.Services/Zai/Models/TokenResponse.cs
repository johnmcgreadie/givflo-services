﻿using System.Text.Json.Serialization;

namespace GivFlo.Services.Zai.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
}
