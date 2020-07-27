using System.Collections.Generic;

namespace DotNetGame.Entities
{
    // Other way to manage bool variables
    //public enum DevState {  Available, Learning, Working };

    public class Developer
    {
        private int _id;
        private string _name;
        private decimal _purchaseCost;
        private decimal _salary;
        private bool _isLearning;
        private bool _isWorking;
        private List<Skill> _devSkills;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public decimal PurchaseCost { get => _purchaseCost; set => _purchaseCost = value; }
        public decimal Salary { get => _salary; set => _salary = value; }
        public List<Skill> DevSkills { get => _devSkills; set => _devSkills = value; }
        public bool IsLearning { get => _isLearning; set => _isLearning = value; }
        public bool IsWorking { get => _isWorking; set => _isWorking = value; }

        public Developer()
        {

        }

        public Developer(int id, string name, decimal cost, decimal salary)
        {
            _id = id;
            _name = name;
            _purchaseCost = cost;
            _salary = salary;
            List<Skill> _devSkills = new List<Skill>();
        }
    }
}