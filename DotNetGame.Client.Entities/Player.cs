using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DotNetGame.Client.Entities
{
    public class Player
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Money")]
        public decimal Money { get; set; }
        [JsonProperty("LesDevelopers")]
        public List<Developer> LesDevelopers { get; set; }
        [JsonProperty("LesProjects")]
        public List<Project> LesProjects { get; set; }
        [JsonProperty("LesSchools")]
        public List<School> LesSchools { get; set; }


        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}
