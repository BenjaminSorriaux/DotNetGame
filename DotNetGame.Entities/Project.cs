using System.Collections.Generic;

namespace DotNetGame.Entities
{
    public class Project
    {
        private int _id;
        private string _name;
        private int _reward;
        private int _remainingDays;
        private bool _isDone;
        private List<Skill> _requiredSkills;
        private List<Developer> _developers;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int Reward { get => _reward; set => _reward = value; }
        public int RemainingDays { get => _remainingDays; set => _remainingDays = value; }
        public List<Skill> RequiredSkills { get => _requiredSkills; set => _requiredSkills = value; }
        public List<Developer> Developers { get => _developers; set => _developers = value; }
        public bool IsDone { get => _isDone; set => _isDone = value; }

        public Project()
        {
            _developers = new List<Developer>();

        }

        public Project(int id, string name, int reward)
        {
            _id = id;
            _name = name;
            _reward = reward;
            _requiredSkills = new List<Skill>();
            _developers = new List<Developer>();
            _isDone = false;
        }
    }
}