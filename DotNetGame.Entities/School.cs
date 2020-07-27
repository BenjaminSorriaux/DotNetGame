using System.Collections.Generic;

namespace DotNetGame.Entities
{
    public class School
    {
        private int _id;
        private string _name;
        private List<Training> _trainings;
        private Training _training;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public Training Training { get => _training; set => _training = value; }
        public List<Training> Trainings { get => _trainings; set => _trainings = value; }

        public School()
        {

        }

        public School(int id, string name)
        {
            _id = id;
            _name = name;
            _trainings = new List<Training>();
        }
    }
}