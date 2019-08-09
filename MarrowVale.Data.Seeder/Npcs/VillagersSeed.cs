using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Data.Seeder.DialogueSeeds;
using System.Collections.Generic;

namespace MarrowVale.Data.Seeder.Npcs
{
    public static class VillagersSeed
    {
        public static Npc GetVillager()
        {
            var villagers = new List<Npc>();

            //get list of dialogues for this location
            var dialogues = GeneralDialogueSeed.GetDialogues();

            //get dialogues for this character
            var villager = new Npc(dialogues, NpcRaceEnum.Human, ClassEnum.Warrior, "Bob", "A simple villager. Looks to somewhat poor and dirty.", 10, 10, 2);
            villagers.Add(villager);
            //add mother data

           
            return villager;
        }
    }
}
