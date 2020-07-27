using DotNetGame.Entities;
using DotNetGame.Server.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DotNetGame.Server.Core
{
    class TcpServer
    {
        private TcpListener _server;

        Service services = new Service();

        int playerId = 0;
        decimal money = 0;
        bool maxClients = false;

        TcpClient client;
        List<TcpClient> clients = new List<TcpClient>();

        Game game = new Game();

        Player player = new Player();

        /// <summary>
        /// This method launch the server and initialize the game.
        /// </summary>
        /// <param name="port"></param>
        /// <param name="gameId"></param>
        /// <param name="gameName"></param>
        /// <param name="nbMaxPlayers"></param>
        /// <param name="nbTurns"></param>
        /// <param name="firstIncome"></param>
        public TcpServer(int port, int gameId, string gameName, int nbMaxPlayers, int nbTurns, decimal firstIncome)
        {
            File.Delete("logs.json");
            try
            {
                game.GameId = gameId;
                game.GameName = gameName;
                game.NbMaxPlayer = nbMaxPlayers;
                game.NbTurn = nbTurns;
                game.FirstIncome = firstIncome;
                game.ConnectedPlayers = new List<Player>();
                money = firstIncome;

                _server = new TcpListener(IPAddress.Any, port);
                _server.Start();

                LoopClients();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This is a loop which will be executed when the server is on.
        /// While the server is on, it will wait for clients until the number of clients is under the max number of players for the game,
        /// When this number is reached, the server will send data to all clients and go out of the loop.
        /// </summary>
        public void LoopClients()
        {
            try
            {
                if (playerId < game.NbMaxPlayer)
                {
                    // If the number of max player is done, the server will stop accepting clients
                    while (maxClients == false)
                    {
                        // Wait for client connection
                        TcpClient newClient = _server.AcceptTcpClient();

                        // Create a thread to handle communication on client found
                        Thread t = new Thread(HandleClient);
                        t.Start(newClient);

                        // Add the new client to a list
                        clients.Add(newClient);

                        playerId++;

                        Console.WriteLine("Player " + playerId + " connected !");

                        if (playerId == game.NbMaxPlayer)
                        {
                            game.Schools = services.GenerateSchool();
                            game.DevelopersAvailable = services.GenerateDeveloper();
                            game.ProjectsAvailable = services.GenerateProject();
                            game.GameTime = DateTime.Now;
                            SendData();
                            maxClients = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is called when a client is connecting.
        /// This will read the data it is sending to the server to get it's username.
        /// </summary>
        /// <param name="obj"></param>
        public void HandleClient(object obj)
        {
            // Send data to clients
            try
            {
                client = (TcpClient)obj;

                ReadData();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// This is the reading method called when a client is connecting and when the game has started.
        /// It will get data in bytes array format and convert it to StringBuilder.
        /// When this is done, it will be converted in string to be added to logs and to prepare it to send when data is correctly edited.
        /// </summary>
        public void ReadData()
        {
            try
            {
                // Creation of a bytes array
                byte[] receivedBytes = new byte[102400];

                // Create a StringBuilder
                StringBuilder message = new StringBuilder();

                do
                {
                    int bytesToRead = client.GetStream().Read(receivedBytes, 0, receivedBytes.Length);
                    message.AppendFormat("{0}", Encoding.UTF8.GetString(receivedBytes, 0, bytesToRead));
                }
                while (client.GetStream().DataAvailable);

                // Get the stringBuilder value in a string wich will be deserialized
                string json = message.ToString();

                // Write received data in the JSON file
                WriteLogs(json);

                PrepareData(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This is the preparation of the data to send to all clients.
        /// A player object is created to edit the user's informations in the JSON data depending on his actions.
        /// The game object is edited depending on the developers, projects or schools selected by the client.
        /// If the client is joining (before the game started) the Player object will be edited differently as during a game.
        /// </summary>
        /// <param name="receivedData"></param>
        public void PrepareData(string receivedData)
        {
            player = JsonConvert.DeserializeObject<Player>(receivedData);

            // Check if a player has the same Id as the received player
            if (player.Id != 0)
            {
                // If the player Id already exists
                // The server will remove the player and recreate it with his new informations
                game.ConnectedPlayers.Remove(game.ConnectedPlayers.Where(p => p.Id == player.Id).FirstOrDefault());
                game.ConnectedPlayers.Add(player);

                game.DevelopersAvailable
                    .Where(d => player.Developers.Any(d1 => d1.Id == d.Id)).ToList()
                    .ForEach(d => game.DevelopersAvailable.Remove(game.DevelopersAvailable.First(d1 => d1.Id == d.Id)
                   ));

                game.ProjectsAvailable
                    .Where(p => player.Projects.Any(p1 => p1.Id == p.Id)).ToList()
                    .ForEach(p => game.ProjectsAvailable.Remove(game.ProjectsAvailable.First(p1 => p1.Id == p.Id)
                   ));
            }
            else
            {
                // If this is a new player, his Id will be 0
                // So the server will add money to him and give him an Id
                player.Id = playerId;
                player.Money = money;
                game.ConnectedPlayers.Add(player);
            }            
        }

        /// <summary>
        /// This is the send data method.
        /// It waits for a second and serialize data to convert it in bytes array.
        /// It send data to the client.
        /// It writes this data in logs and in the console.
        /// </summary>
        public void SendData()
        {
            try
            {
                Thread.Sleep(1000);

                string jsonData = JsonConvert.SerializeObject(game);

                byte[] bytes = Encoding.ASCII.GetBytes(jsonData);

                clients.ForEach(c => c.GetStream().Write(bytes, 0, bytes.Length));

                // Write data to send in the JSON file
                WriteLogs(jsonData);

                Console.WriteLine(jsonData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This is a loop which is executed during the game.
        /// This read data in a first time and when it got something will edit this data and send it to everybody.
        /// When the data is sent, it will read again.
        /// </summary>
        public void GameComm()
        {
            while(true)
            {
                ReadData();
                SendData();
            }
        }

        /// <summary>
        /// This method get updated logs.json file.
        /// It will get the json data to write it in the file.
        /// </summary>
        /// <param name="comms"></param>
        public void WriteLogs(string comms)
        {
            string jsonFile = "logs.json";
            if (!File.Exists(jsonFile))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(jsonFile))
                {
                    sw.WriteLine(comms);
                }
            }

            using (StreamWriter sw = File.AppendText(jsonFile))
            {
                sw.WriteLine(comms);
            }
        }
    }
}
