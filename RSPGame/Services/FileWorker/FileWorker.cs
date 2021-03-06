﻿using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RSPGame.Services.FileWorker
{
    public class FileWorker : IFileWorker
    {
        public async Task SaveToFileAsync<T>(string path, T obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            await File.WriteAllTextAsync(path, json);
        }

        public async Task<T> DeserializeAsync<T>(string path)
        {
            var json = await File.ReadAllTextAsync(path);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}