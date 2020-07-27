using System;

namespace DotNetGame.Server.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int gameId = 1;
                Console.WriteLine("Enter a game name");
                string gameName = Console.ReadLine();
                Console.WriteLine("Enter a max number of players");
                int nbMaxPlayer = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a number of turns");
                int nbTurns = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a first income");
                decimal money = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Game initialized !");
                Console.WriteLine("Waiting for players...");

                TcpServer server = new TcpServer(5555, gameId, gameName, nbMaxPlayer, nbTurns, money);

                Console.WriteLine("Data sent to players !");

                server.GameComm();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
