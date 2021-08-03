using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class User:IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string FullName { get; set; }
    }
}
