using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MarrowVale.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ILogger _logger;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private string LocationFilePath { get; set; }

        private JsonSerializerSettings Settings { get; set; }
        private IList<Location> Locations { get; set; }

        public LocationRepository(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider)
        {
            _logger = logger.CreateLogger<SoundRepository>();
            _appSettingsProvider = appSettingsProvider;

            var file = Path.Combine(_appSettingsProvider.DataFilesLocation, "LocationList.json");
            //PlayerFilePath = $"{_appSettingsProvider.DataFilesLocation}\\PlayerList.json";
            LocationFilePath = file;

            Locations = loadLocations();

            Settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
        }

        public void SaveLocations(IList<Location> locations)
        {
            var newJson = JsonConvert.SerializeObject(locations, Formatting.Indented, Settings);

            File.WriteAllText(LocationFilePath, newJson);
        }

        public Location GetLocation(string locationName)
        {
            var location = Locations.FirstOrDefault(x => x.Name == locationName);

            if (location == null)
            {
                return null;
            }

            return location;
        }

        private IList<Location> loadLocations()
        {
            var fileExists = File.Exists(LocationFilePath);

            if (fileExists)
            {
                var locationFile = File.ReadAllText(LocationFilePath);
                return JsonConvert.DeserializeObject<List<Location>>(locationFile, Settings);
            }

            return null;
        }

    }
}
