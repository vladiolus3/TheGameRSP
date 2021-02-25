﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using RSPGame.Models;
using RSPGame.UI.PlayRequests;

namespace RSPGame.UI.Menus
{
    public static class PrivateRoomMenu
    {
        public static void Start(HttpClient client, GamerInfo gamer)
        {
            while (true)
            {
                int num;
                Console.WriteLine("1.\tCreate room");
                Console.WriteLine("2.\tJoin room");
                Console.WriteLine("3.\tBack");

                while (true)
                {
                    Console.Write("Enter the number: ");
                    if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                    else if (num < 1 || num > 3) Console.WriteLine("Incorrect number. Try again");
                    else break;
                }
                Console.WriteLine();
                switch (num)
                {
                    case 1:
                        var json = RoomRequests.CreateRoom(client, gamer)?.Result;
                        if (json == null) break;
                        var id1 = JsonConvert.DeserializeObject<int>(json);

                        Console.WriteLine($"\nRoom with id {id1} has been created!");
                        Console.WriteLine("\nWaiting for opponent\n\n");

                        var result1 = GameRequests.GetGame(client, id1)?.ToArray();

                        break;

                    case 2:
                        Console.Write("Enter the id of the desired room: ");

                        if (!int.TryParse(Console.ReadLine(), out var id2))
                        {
                            Console.WriteLine("\nERROR:\tThe only numbers can be entered. Try again\n\n");
                            break;
                        }
                        else if (id2 < 1 || id2 > 1000)
                        {
                            Console.WriteLine("\nERROR:\tIncorrect number. Try again\n\n");
                            break;
                        }


                        if (RoomRequests.JoinRoom(client, gamer, id2) == null) break;

                        var result2 = GameRequests.GetGame(client, id2)?.ToArray();

                        new GameLogic().StartGame(client, result2, id2);

                        break;

                    case 3:
                        return;
                }
            }
        }
    }
}
