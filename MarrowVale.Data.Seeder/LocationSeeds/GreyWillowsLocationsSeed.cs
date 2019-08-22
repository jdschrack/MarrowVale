using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Data.Seeder.LocationSeeds
{
    public class GreyWillowsLocationsSeed
    {
        public IList<Location> GetLocations()
        {
            //var listOfDialogues = new List<Dialogue>();

            //var greetingDialogue = new Dialogue("Greetings!", DialogueTypeEnum.Friendly, "Greeting", LanguageEnum.Common, new List<Dialogue>());

            //listOfDialogues.Add(greetingDialogue);

            //return listOfDialogues;

            var listOfLocations = new List<Location>();

            var startingVillage = new Location("Starting Village", $"You step out of your small hut and look up at the sky.{Environment.NewLine}Through the old gnarled trees, you spot a huge black storm cloud rolling in.{Environment.NewLine}" +
                                               $"Everyone you see is rushing to get inside to avoid the coming storm.{Environment.NewLine}You have been in this village since as long as you can remember, and you have never seen anything like this before.{Environment.NewLine}{Environment.NewLine}" +
                                               $"You see everyone run inside their houses except for the Crazy Old Man standing in front of his house to the west.{Environment.NewLine}He is staring at the sky with a look of dread on his face.{Environment.NewLine}{Environment.NewLine}" +
                                               $"Mother: You better hurry. A storm is coming.{Environment.NewLine}You need to get going if you want to make it to High Village before it starts.{Environment.NewLine}You don’t want to be soaking wet on your last day of training.{Environment.NewLine}",
                                               "This is a day visited test description", "this is a night description", "this is a night visited description",new List<EnvironmentalObject>(), new List<EnvironmentalInteraction>());

            listOfLocations.Add(startingVillage);

            return listOfLocations;
        }
    }
}
