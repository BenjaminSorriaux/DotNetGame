using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetGame.Client.Entities
{
    public class Game
    {
        [JsonProperty("GameId")]
        public int GameId { get; set; }
        [JsonProperty("GameName")]
        public string GameName { get; set; }
        [JsonProperty("NbTurns")]
        public int NbTurns { get; set; }
        [JsonProperty("NbMaxPlayer")]
        public int NbMaxPlayer { get; set; }
        [JsonProperty("FirstIncome")]
        public decimal FirstIncome { get; set; }
        [JsonProperty("ConnectedPlayers")]
        public List<Player> ConnectedPlayers { get; set; }
    }
}
