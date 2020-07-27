using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetGame.Entities
{
    public class DeserializeData
    {
        public List<Developer> Developers { get; set; }

        public List<Project> Projects { get; set; }
        public List<School> Schools { get; set; }
        public DeserializeData()
        {
           
        }
    }
}
