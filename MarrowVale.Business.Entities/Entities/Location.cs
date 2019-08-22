
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class Location
    {
        public Location()
        {
            Items = new List<IItem>();
            Npcs = new List<Npc>();
        }

        [JsonConstructor]
        public Location(string Name, string DayDescription, string DayVisitedDescription, string NightDescription, string NightVisitedDescription, IList<EnvironmentalObject> EnvironmentalObjects, IList<EnvironmentalInteraction> EnvironmentalInteractions): this()
        {
            this.Name = Name;
            this.DayDescription = DayDescription;
            this.DayVisitedDescription = DayVisitedDescription;
            this.NightDescription = NightDescription;
            this.NightVisitedDescription = NightVisitedDescription;
            this.EnvironmentalObjects = EnvironmentalObjects;
            this.EnvironmentalInteractions = EnvironmentalInteractions;
            this.PlayersVisited = new List<string>();
        }
        
        public string Name { get; }
        public string DayDescription { get; private set; }
        public string NightDescription { get; private set; }
        public string DayVisitedDescription { get; private set; }
        public string NightVisitedDescription { get; private set; }
        public IList<string> PlayersVisited { get; set; }

        [JsonProperty]
        private IList<IItem> Items { get; }

        [JsonProperty]
        private IList<Npc> Npcs { get; }
        private IList<EnvironmentalObject> EnvironmentalObjects { get; set; }
        private IList<EnvironmentalInteraction> EnvironmentalInteractions { get; set; }
        
        private IList<string> ConnectingLocationNames { get; set; }

        public void AddItem(IItem item)
        {
            Items.Add(item);
        }

        public IItem PickUpItem(string item)
        {
            return Items.FirstOrDefault(x => x.Name.Equals(item, StringComparison.CurrentCultureIgnoreCase));
        }

        public string GetLocationDescription(string description)
        {
            //first attempt at building location description
            var itemList = new List<string>();
            var objectList = new List<string>();

            //lists cannot be null FIX THIS !!!!!!!!!!!!!!!!!!!!!
            if(Items != null)
            {
                Items.Where(x => x.IsVisible).ToList().ForEach(x => itemList.Add(x.Name));
            }
            
            if(EnvironmentalObjects != null)
            {
                EnvironmentalObjects.ToList().ForEach(x => objectList.Add(x.Name));
            }        

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(description);
            //not working....convert lists to lists of strings
            stringBuilder.AppendJoin($"{Environment.NewLine}", itemList);
            stringBuilder.AppendJoin($"{Environment.NewLine}", objectList);
            return stringBuilder.ToString();
        }
    }
}
