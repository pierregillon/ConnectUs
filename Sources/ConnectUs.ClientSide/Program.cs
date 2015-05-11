﻿using System;

namespace ConnectUs.ClientSide
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            const string hostName = "localhost";
            const int port = 9000;

            var client = new Client();
            client.ClientConnected+=ClientOnClientConnected;
            client.ClientDisconnected += ClientOnClientDisconnected;

            while (true) {
                Console.WriteLine("Trying to connect to the host '{0}' on port '{1}'", hostName, port);
                client.ConnectToServer(hostName, port);
            }
        }
        private static void ClientOnClientConnected(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Client connected.");
        }

        private static void ClientOnClientDisconnected(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Client disconnected.");
        }
    }
}