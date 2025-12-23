using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TwitchBot
{
    public class AppSettingsLoader
    {
        // Path to the appsettings.json file
        private readonly string _filePath;

        // Constructor with optional file path parameter
        public AppSettingsLoader(string filePath = "appsettings.json")
        {
            _filePath = filePath;
        }

        // Load and deserialize the appsettings.json file
        public AppSettings Load()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException(
                    $"Configuration file not found: {_filePath}"
                );

            var json = File.ReadAllText(_filePath);

            var settings = JsonSerializer.Deserialize<AppSettings>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (settings == null)
                throw new InvalidOperationException("Failed to deserialize appsettings.json");

            return settings;
        }
    }
}
