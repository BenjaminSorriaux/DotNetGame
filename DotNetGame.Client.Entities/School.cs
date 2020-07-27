using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetGame.Client.Entities
{
    public class School
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Trainings")]
        public List<Training> Trainings { get; set; }
    }
}
