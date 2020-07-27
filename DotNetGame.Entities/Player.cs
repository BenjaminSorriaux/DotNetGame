using System;
using System.Linq;
using System.Collections.Generic;

namespace DotNetGame.Entities
{
    public class Player
    {
        #region Champs / accesseurs
        private int _id;
        private string _name;
        private decimal _money;
        private List<Developer> _Developers;
        private List<Project> _Projects;
        private List<School> _Schools;
        private bool _played;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public decimal Money { get => _money; set => _money = value; }
        public List<Developer> Developers { get => _Developers; set => _Developers = value; }
        public List<Project> Projects { get => _Projects; set => _Projects = value; }
        public List<School> Schools { get => _Schools; set => _Schools = value; }
        public bool Played { get => _played; set => _played = value; }
        public int Turn { get; set; }
        #endregion
        #region Les constructeurs
        public Player()
        {
            _Developers = new List<Developer>();
            _Projects = new List<Project>();
        }

        public Player(int id, string name, int money, List<School> schools)
        {
            _Developers = new List<Developer>();
            _Projects = new List<Project>();
            _Schools = schools;
        }
        #endregion
      

    }
}
