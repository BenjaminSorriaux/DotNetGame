using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetGame.Entities
{
    public class Game
    {
        private int _gameId;
        private string _gameName;
        private int _nbTurn;
        private int _nbMaxTurn;
        private int _nbMaxPlayer;
        private decimal _firstIncome; // Corresponding to the beginning money
        private List<Player> _connectedPlayers;
        private List<Developer> _developersAvailable;
        private List<Project> _projectsAvailable;
        private List<School> _schools;
        private DateTime _gameTime;
        private bool _hasBegan;

        public int GameId { get => _gameId; set => _gameId = value; }
        public string GameName { get => _gameName; set => _gameName = value; }
        public int NbTurn { get => _nbTurn; set => _nbTurn = value; }
        public decimal FirstIncome { get => _firstIncome; set => _firstIncome = value; }
        public List<Player> ConnectedPlayers { get => _connectedPlayers; set => _connectedPlayers = value; }
        public DateTime GameTime { get => _gameTime; set => _gameTime = value; }
        public int NbMaxTurn { get => _nbMaxTurn; set => _nbMaxTurn = value; }
        public List<Developer> DevelopersAvailable { get => _developersAvailable; set => _developersAvailable = value; }
        public bool HasBegan { get => _hasBegan; set => _hasBegan = value; }
        public int NbMaxPlayer { get => _nbMaxPlayer; set => _nbMaxPlayer = value; }
        public List<Project> ProjectsAvailable { get => _projectsAvailable; set => _projectsAvailable = value; }
        public List<School> Schools { get => _schools; set => _schools = value; }

        public Game()
        {
            DevelopersAvailable = new List<Developer>();
            ProjectsAvailable = new List<Project>();
            ConnectedPlayers = new List<Player>();
            Schools = new List<School>();
        }
    }
}
