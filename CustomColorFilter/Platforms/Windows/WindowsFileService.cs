﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CustomColorFilter.Contracts;

namespace CustomColorFilter.Platforms.Windows
{
    internal class WindowsFileService : IFileService
    {
        private string ConfigPath => Path.Combine(FileSystem.Current.AppDataDirectory, "config.json");

        public Configuration GetConfig()
        {
            if (File.Exists(ConfigPath))
            {
                var content = File.ReadAllText(this.ConfigPath);
                var deserialized = JsonSerializer.Deserialize<Configuration>(content);
                return deserialized!;
            }
            return new Configuration(string.Empty, false, Array.Empty<Filter>());
        }

        public void SaveConfig(Configuration config)
        {
            
            var asString = JsonSerializer.Serialize(config);
            File.WriteAllText(this.ConfigPath, asString);
        }
    }
}
