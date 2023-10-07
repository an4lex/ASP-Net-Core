using System;
using Newtonsoft.Json;
namespace TestApplication.Other {
	public class Book
	{
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("author")]
        public string? Author { get; set; }
    }
}

