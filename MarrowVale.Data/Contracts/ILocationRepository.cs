using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Data.Contracts
{
    public interface ILocationRepository
    {
        void SaveLocations(IList<Location> locations);
        Location GetLocation(string locationName);
    }
}
