﻿using MarrowVale.Business.Entities.Enums;

namespace MarrowVale.Business.Entities.Entities
{
    public class Weapon : IItem
    {
        public int Range { get; protected set; }
        public int Damage { get; protected set; }
        public string Name { get; set; }
        public WeaponTypeEnum Type { get; private set; }
    }
}
