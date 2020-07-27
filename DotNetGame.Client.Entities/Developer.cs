using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DotNetGame.Client.Entities
{
    public class Developer
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Skill")]
        public List<Skill> Skills { get; set; }
        [JsonProperty("Salary")]
        public decimal Salary { get; set; }
        [JsonProperty("PurchaseCost")]
        public double PurchaseCost { get; set; }
        [JsonProperty("IsLearning")]
        public bool IsLearning { get; set; }
        [JsonProperty("IsWorking")]
        public bool IsWorking { get; set; }

       

    }
}
