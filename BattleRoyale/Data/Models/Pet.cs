﻿

namespace BattleRoyale.Data.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stats { get; init; }
        public string Type { get; set; }
        public int HeroId { get; set; }
    }
}
