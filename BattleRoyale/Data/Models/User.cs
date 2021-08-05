using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static BattleRoyale.Data.Constants.DatabaseConstants;

namespace BattleRoyale.Data.Models
{
    public class User:IdentityUser
    {
        [Required]
        [MaxLength(DefaultMaxLengthForName)]
        public string FullName { get; set; }
    }
}
