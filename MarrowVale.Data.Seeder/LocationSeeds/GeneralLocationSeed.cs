using MarrowVale.Business.Entities.Entities;
using MarrowVale.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarrowVale.Data.Seeder.LocationSeeds
{
    public static class GeneralLocationSeed
    {
        public static IList<Location> GetLocations()
        {           
            var locations = new List<Location>();

            var greyWillowsLocationsSeeder = new GreyWillowsLocationsSeed();
            var greyWillowsLocations = greyWillowsLocationsSeeder.GetLocations();

            locations.AddRange(greyWillowsLocations);

            return locations;
        }
    }
}
