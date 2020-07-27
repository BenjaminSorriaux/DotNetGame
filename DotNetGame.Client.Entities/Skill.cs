using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetGame.Client.Entities
{
    public class Skill
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Level")]
        public int Level { get; set; }
    }
}
