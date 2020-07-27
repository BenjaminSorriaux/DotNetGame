using DotNetGame.Entities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DotNetGame.Client.Services
{
    public class ClientClass
    {
        private string _data;
        private TcpClient _client = new TcpClient();

        Player player = new Player();

        //public string Data { get; set; }

        public int _bytesLength = 102400;

        public string Data { get => _data; set => _data = value; }
        public Game Game { get; set; }

        public void SocketClient(String ipAddress, int portNum,object obj)
        {
            _client.Connect(ipAddress, portNum);
            HandleCommunication();
            sendData(obj);
        }

        public void HandleCommunication()
        {
            Thread t = new Thread(ReadThread);
            t.Start();
        }

        public void ReadThread()
        {

            // Creation of a bytes array
            byte[] bytes = new byte[_bytesLength];

            // Create a StringBuilder

            do
            {
                StringBuilder message = new StringBuilder();
                int bytesToRead = _client.GetStream().Read(bytes, 0, bytes.Length);
                message.AppendFormat("{0}", Encoding.UTF8.GetString(bytes, 0, bytesToRead));
                Data = message.ToString();
                Game = JsonConvert.DeserializeObject<Game>(message.ToString());
            }
            while (true);
        }
        public  void sendData(Object obj)
        {
            //_client.Connect(ip, port);
            string json = JsonConvert.SerializeObject(obj);
            NetworkStream network = _client.GetStream();
            if (network.CanWrite)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(json);
                network.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
