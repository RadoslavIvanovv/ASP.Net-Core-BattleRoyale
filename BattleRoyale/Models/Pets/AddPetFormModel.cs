

using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Models.Pets
{
    public class AddPetFormModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        public int Stats { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int HeroId { get; set; }
    }
}
