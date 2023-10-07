using Newtonsoft.Json;

namespace TestApplication.Other
{
    public class Student
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("studentID")]
        public int StudentID { get; set; }
    }
}