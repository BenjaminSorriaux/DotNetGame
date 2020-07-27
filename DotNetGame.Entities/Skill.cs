using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetGame.Entities
{
    public class Skill
    {
        private int _id;
        private string _description;
        private int _level;

        public int Id { get => _id; set => _id = value; }
        public string Description { get => _description; set => _description = value; }
        public int Level { get => _level; set => _level = value; }

        public Skill()
        {

        }

        public Skill(int id, string description, int level)
        {
            _id = id;
            _description = description;
            _level = level;
        }
    }
}
