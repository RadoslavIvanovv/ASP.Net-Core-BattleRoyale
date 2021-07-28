

namespace BattleRoyale.Models.Pets
{
    public class AddPetFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stats { get; set; }
        public string Type { get; set; }
        public int HeroId { get; set; }
    }
}
