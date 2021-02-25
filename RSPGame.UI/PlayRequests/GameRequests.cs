﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RSPGame.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RSPGame.UI.PlayRequests
{
    public static class GameRequests
    {
        public static IEnumerable<string> GetGame(HttpClient client, int roomId)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            var counter = 0;
            HttpResponseMessage response;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            while (true)
            {
                if (stopwatch.ElapsedMilliseconds < 2500) continue;

                try
                {
                    response = client.GetAsync($"api/game/{roomId}").Result;
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("\nERROR:\tCheck your internet connection\n\n");
                    return null;
                }
                if (response.StatusCode == HttpStatusCode.OK) break;

                counter++;
                stopwatch.Restart();
                if (counter == 12) break;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("\nThe game could not be found. Please try again.\n\n");
                return null;
            }

            Console.WriteLine("\nDone!\n\n");
            
            var json = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<string[]>(json);
        }

        public static Task PostAction(HttpClient client, GameActionsUi action, int roomId, int roundId)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            var json = JsonSerializer.Serialize(action);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var message = client.PostAsync($"/api/round/{roomId}/{roundId}", content).Result;
            }
            catch (AggregateException)
            {
                Console.WriteLine("\nERROR:\tCheck your internet connection\n\n");
                return null;
            }

            Console.WriteLine("\nRequest has been sent!\n\n");
            return Task.CompletedTask;
        }

    }
}
