using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetGame.Entities
{
    public class Training
    {
        private int _id;
        private string _description;
        private List<Skill> _improvedSkills;
        private int _timeTraining;

        public int Id { get => _id; set => _id = value; }
        public string Description { get => _description; set => _description = value; }
        public List<Skill> ImprovedSkills { get => _improvedSkills; set => _improvedSkills = value; }
        public int TimeTraining { get => _timeTraining; set => _timeTraining = value; }

        public Training()
        {

        }

        public Training(int id, string description)
        {
            _id = id;
            _description = description;
            List<Skill> _improvedSkills = new List<Skill>();
        }
    }
}
