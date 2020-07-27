using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetGame.Client.Entities
{
    public class Project
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Reward")]
        public int Reward { get; set; }
        [JsonProperty("RemainingDays")]
        public int RemainingDays { get; set; }
        [JsonProperty("RequiredSkills")]
        public List<Skill> RequiredSkills { get; set; }
    }
}
