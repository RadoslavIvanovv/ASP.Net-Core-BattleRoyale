using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BattleRoyale.Data.Models
{
    public class User:IdentityUser
    {
        [MaxLength(20)]
        public string FullName { get; set; }
    }
}
