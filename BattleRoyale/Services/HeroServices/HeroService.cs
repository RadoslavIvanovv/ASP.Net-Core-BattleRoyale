

using AutoMapper;
using AutoMapper.QueryableExtensions;
using BattleRoyale.Data;
using BattleRoyale.Data.Models;
using BattleRoyale.Data.Models.HeroTypes;
using BattleRoyale.Models.Heroes;
using BattleRoyale.Models.Players;
using BattleRoyale.Services.ItemServices;
using System;
using System.Collections.Generic;
using System.Linq;

using static BattleRoyale.Data.Constants;
using static BattleRoyale.Data.Constants.HeroConstants;
using static BattleRoyale.Data.Constants.ItemConstants;
using static BattleRoyale.Data.Constants.PlayerConstants;
using static BattleRoyale.Data.Constants.HeroControllerConstants;

namespace BattleRoyale.Services.HeroServices
{
    public class HeroService : IHeroService
    {

        private readonly BattleRoyaleDbContext context;
        private readonly IItemService itemService;
        private readonly IConfigurationProvider mapper;

        public HeroService(
            BattleRoyaleDbContext context,
            IItemService itemService,
            IConfigurationProvider mapper)
        {
            this.context = context;
            this.itemService = itemService;
            this.mapper = mapper;
        }

        public string Add(HeroModel hero, string userId)
        {
            var player = this.context.Players.Where(p => p.UserId == userId).FirstOrDefault();


            var heroData = new Hero
            {
                Id = hero.Id,
                Name = hero.Name,
                ImageUrl = hero.ImageUrl,
                Player = player,
                Pet=hero.Pet,
                HeroType = Enum.Parse<HeroType>(hero.HeroType),
            };

            SetHeroStats(heroData);

            if (player == null)
            {
                player = BecomePlayer(userId);
            }
            var playerHeroes = GetPlayerHeroes(userId);

            var existingHero = playerHeroes.Where(h => h.Name == hero.Name).FirstOrDefault();

            if (playerHeroes.Count == 0)
            {
                heroData.IsMain = true;
            }

            if (player.Heroes.Count > 0)
            {
                var playerLevelRequirement = player.Level % 10 == 0;
                var playerHeroesRequirement = player.Heroes.Count < (player.Level / 10);
                if (!playerLevelRequirement && !playerHeroesRequirement)
                {
                    return RequirementsNotMet;
                }
            }

            player.Heroes.Add(heroData);

            this.context.Heroes.Add(heroData);

            this.context.SaveChanges();

            return null;
        }

        public string Remove(int heroId, string userId)
        {
            var heroes = GetPlayerHeroes(userId);

            var hero = heroes.Where(h => h.Id == heroId).FirstOrDefault();

            if (heroes.Count == 1)
            {
                return HeroCountCannotBeLessThanOne;
            }

            this.context.Heroes.Remove(hero);
            heroes.Remove(hero);

            if (!heroes.Any(h => h.IsMain))
            {
                heroes[0].IsMain = true;
            }

            this.context.SaveChanges();

            return null;
        }

        public IEnumerable<HeroModel> All(int heroId, string userId)
        {
            if (heroId != 0)
            {
                var currentMainHero = this.context.Players
                .Where(p => p.UserId == userId)
                .Select(p => p.Heroes.Where(h => h.IsMain == true).FirstOrDefault()).FirstOrDefault();

                currentMainHero.IsMain = false;

                var newMainHero = this.context.Players
                .Where(p => p.UserId == userId)
                .Select(p => p.Heroes.Where(h => h.Id == heroId).FirstOrDefault()).FirstOrDefault();

                newMainHero.IsMain = true;

                this.context.SaveChanges();
            }

            var heroes = this.context.Players
                .Where(p => p.UserId == userId)
                .SelectMany(p => p.Heroes.Select(h => new HeroModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    ImageUrl = h.ImageUrl,
                    Level = h.Level,
                    Attack = h.Attack,
                    MagicAttack = h.MagicAttack,
                    Health = h.Health,
                    Armor = h.Armor,
                    MagicResistance = h.MagicResistance,
                    Speed = h.Speed,
                    Pet=h.Pet,
                    OverallPower = h.OverallPower,
                    HeroType = h.HeroType.ToString()
                })).ToList();

            return heroes;
        }

        public PlayerHeroViewModel Details(int heroId, string userId)
        {

            var player = this.context.Players.Where(p => p.UserId == userId).FirstOrDefault();

            var inventory = this.context.Players
               .Where(p => p.UserId == userId)
               .ProjectTo<PlayerInventoryViewModel>(this.mapper)
               .FirstOrDefault();

            var hero = this.context.Heroes
                .Where(h => h.Id == heroId).FirstOrDefault();

            var pet = this.context.Pets.Where(p => p.HeroId == hero.Id).FirstOrDefault();

            var heroDetails = new Hero
            {
                Id = hero.Id,
                Name = hero.Name,
                ImageUrl = hero.ImageUrl,
                Level = hero.Level,
                ExperiencePoints = hero.ExperiencePoints,
                RequiredExperiencePoints = hero.RequiredExperiencePoints,
                Attack = hero.Attack,
                MagicAttack = hero.MagicAttack,
                Health = hero.Health,
                Armor = hero.Armor,
                MagicResistance = hero.MagicResistance,
                Speed = hero.Speed,
                Pet = pet,
                OverallPower = hero.OverallPower,
                HeroType = hero.HeroType,
                Items = hero.Items
            };

            if (pet != null)
            {
                heroDetails.HasPet = true;
            }

            var playerData = new PlayerHeroViewModel
            {
                Id = player.Id,
                Hero = heroDetails,
                Items = inventory.BoughtItems
            };

            return playerData;
        }

        public Hero Equip(int heroId, int itemId,string userId)
        {
            var inventory = GetPlayerInventory(userId);

            var hero = this.context.Heroes
                .Where(h => h.Id == heroId).FirstOrDefault();

            var item = inventory.BoughtItems.Where(i => i.Id == itemId).FirstOrDefault();

            if (itemService.HeroHasItem(hero, item))
            {
                return null;
            }

            EquipItem(hero, item);
            hero.Items.Add(item);
            this.context.SaveChanges();

            return hero;
        }

        public Hero Unequip(int heroId, int itemId,string userId)
        {
            var inventory = GetPlayerInventory(userId);

            var hero = this.context.Heroes
                .Where(h => h.Id == heroId).FirstOrDefault();

            var item = inventory.BoughtItems.Where(i => i.Id == itemId).FirstOrDefault();

            UnequipItem(hero, item);
            hero.Items.Remove(item);
            this.context.SaveChanges();

            return hero;
        }

    public string GetHeroType(Hero hero)
        {
            if (hero.HeroType.ToString() == Assassin)
            {
                return Assassin;
            }
            else if (hero.HeroType.ToString() == Tank)
            {
                return Tank;
            }
            else if (hero.HeroType.ToString() == Mage)
            {
                return Mage;
            }
            else
            {
                return new InvalidOperationException(InvalidHero).ToString();
            }
        }


        public void SetHeroStats(Hero hero)
        {
            var heroType = GetHeroType(hero);

            hero.Level = InitialHeroLevel;
            hero.RequiredExperiencePoints = InitialRequiredExperience;

            if (heroType == Assassin)
            {
                hero.Attack = InitialAssassinAttack;
                hero.MagicAttack = InitialAssassinMagicAttack;
                hero.Health = InitialAssassinHealth;
                hero.Armor = InitialAssassinArmor;
                hero.MagicResistance = InitialAssassinMagicResistance;
                hero.Speed = InitialAssassinSpeed;
            }
            else if (heroType == Tank)
            {
                hero.Attack = InitialTankAttack;
                hero.MagicAttack = InitialTankMagicAttack;
                hero.Health = InitialTankHealth;
                hero.Armor = InitialTankArmor;
                hero.MagicResistance = InitialTankMagicResistance;
                hero.Speed = InitialTankSpeed;
            }
            else if (heroType == Mage)
            {
                hero.Attack = InitialMageAttack;
                hero.MagicAttack = InitialMageMagicAttack;
                hero.Health = InitialMageHealth;
                hero.Armor = InitialMageArmor;
                hero.MagicResistance = InitialMageMagicResistance;
                hero.Speed = InitialMageSpeed;
            }

            hero.OverallPower = hero.Attack + hero.MagicAttack + hero.Health + hero.Armor + hero.MagicResistance + hero.Speed;

            SetHeroImage(hero);
        }
        public void SetHeroImage(Hero hero)
        {
            var heroType = GetHeroType(hero);

            if (heroType == Assassin)
            {
                hero.ImageUrl = AssassinImage;
            }
            else if (heroType == Tank)
            {
                hero.ImageUrl = TankImage;
            }
            else if (heroType == Mage)
            {
                hero.ImageUrl = MageImage;
            }
        }

        public void EquipItem(Hero hero, Item item)
        {
            if (item.ItemType.ToString() == Weapon)
            {
                hero.Attack += item.Stats;
                hero.HasWeapon = true;
            }
            else if (item.ItemType.ToString() == Necklace)
            {
                hero.MagicAttack += item.Stats;
                hero.HasNecklace = true;
            }
            else if (item.ItemType.ToString() == Armor)
            {
                hero.Armor += item.Stats;
                hero.HasArmorItem = true;
            }
            else if (item.ItemType.ToString() == MagicResistance)
            {
                hero.MagicResistance += item.Stats;
                hero.HasMagicResistItem = true;
            }
            else if (item.ItemType.ToString() == Boots)
            {
                hero.Speed += item.Stats;
                hero.HasBoots = true;
            }
            SetAdditionalEffectFromItem(hero, item);
            item.IsEquipped = true;
            hero.OverallPower += item.Stats;
        }

        public void UnequipItem(Hero hero, Item item)
        {
            if (item.ItemType.ToString() == Weapon)
            {
                hero.Attack -= item.Stats;
                hero.HasWeapon = false;
            }
            else if (item.ItemType.ToString() == Armor)
            {
                hero.Armor -= item.Stats;
                hero.HasArmorItem = false;
            }
            else if (item.ItemType.ToString() == MagicResistance)
            {
                hero.MagicResistance -= item.Stats;
                hero.HasMagicResistItem = false;
            }
            else if (item.ItemType.ToString() == Necklace)
            {
                hero.MagicAttack -= item.Stats;
                hero.HasNecklace = false;
            }
            else if (item.ItemType.ToString() == Boots)
            {
                hero.Speed -= item.Stats;
                hero.HasBoots = false;
            }
            RemoveAdditionalEffectFromItem(hero, item);
            item.IsEquipped = false;
            hero.OverallPower -= item.Stats;
        }

        public void Attack(HeroFightViewModel attacker, HeroFightViewModel defender)
        {

            ReturnRemainingArmor(attacker, defender);
            ReturnRemainingMagicResistance(attacker, defender);

        }

        public void LevelUp(Hero hero)
        {
            var heroType = GetHeroType(hero);

            hero.Level++;

            if (heroType == Assassin)
            {
                hero.Attack += AssassinAttackOnLevelUp;
                hero.MagicAttack += AssassinMagicAttackOnLevelUp;
                hero.Health += AssassinHealthOnLevelUp;
                hero.Armor += AssassinArmorOnLevelUp;
                hero.MagicResistance += AssassinMagicResistanceOnLevelUp;
                hero.Speed += AssassinSpeedOnLevelUp;
            }
            else if (heroType == Tank)
            {
                hero.Attack += TankAttackOnLevelUp;
                hero.MagicAttack += TankMagicAttackOnLevelUp;
                hero.Health += TankHealthOnLevelUp;
                hero.Armor += TankArmorOnLevelUp;
                hero.MagicResistance += TankMagicResistanceOnLevelUp;
                hero.Speed += TankSpeedOnLevelUp;
            }
            else if (heroType == Mage)
            {
                hero.Attack += MageAttackOnLevelUp;
                hero.MagicAttack += MageMagicAttackOnLevelUp;
                hero.Health += MageHealthOnLevelUp;
                hero.Armor += MageArmorOnLevelUp;
                hero.MagicResistance += MageMagicResistanceOnLevelUp;
                hero.Speed += MageSpeedOnLevelUp;
            }

            hero.OverallPower = hero.Attack + hero.MagicAttack + hero.Health + hero.Armor + hero.MagicResistance + hero.Speed;
            hero.RequiredExperiencePoints = hero.RequiredExperiencePoints + (int)(hero.RequiredExperiencePoints * AdditionalRequiredExperienceAfterLevelUp);

        }

        private Player BecomePlayer(string userId)
        {
            var existingUser = this.context.Users.Where(u => u.Id == userId).FirstOrDefault();

            if (existingUser != null)
            {
                var player = new Player
                {
                    Name = existingUser.FullName,
                    Level = PlayerLevel,
                    ExperiencePoints = 0,
                    RequiredExperiencePoints = RequiredExperiencePoints,
                    Gold = InitialPlayerGold,
                    UserId = userId
                };

                this.context.Players.Add(player);
                this.context.SaveChanges();

                return player;
            }
            return null;
        }
        private void ReturnRemainingArmor(HeroFightViewModel attacker, HeroFightViewModel defender)
        {
            var remainingArmor = defender.RemainingArmor - attacker.Attack;
            if (remainingArmor > 0)
            {
                defender.RemainingArmor = remainingArmor;
            }
            else
            {
                defender.RemainingArmor = 0;
                defender.RemainingHealth -= Math.Abs(remainingArmor);
            }

        }

        private void ReturnRemainingMagicResistance(HeroFightViewModel attacker, HeroFightViewModel defender)
        {
            var remainingMagicResistance = defender.RemainingMagicResistance - attacker.MagicAttack;

            if (remainingMagicResistance > 0)
            {
                defender.RemainingMagicResistance = remainingMagicResistance;
            }
            else
            {
                defender.RemainingMagicResistance = 0;
                defender.RemainingHealth -= Math.Abs(remainingMagicResistance);
            }
        }

        private void SetAdditionalEffectFromItem(Hero hero, Item item)
        {
            if (item.AdditionalEffect.ToString() == ItemConstants.Attack)
            {
                hero.Attack += AdditionalAttackFromItem;
            }
            else if (item.AdditionalEffect.ToString() == MagicAttack)
            {
                hero.MagicAttack += AdditionalMagicAttackFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Health)
            {
                hero.Health += AdditionalHealthFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Armor)
            {
                hero.Armor += AdditionalArmorFromItem;
            }
            else if (item.AdditionalEffect.ToString() == MagicResistance)
            {
                hero.MagicResistance = AdditionalMagicResistanceFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Speed)
            {
                hero.Speed += AdditionalSpeedFromItem;
            }
        }

        private void RemoveAdditionalEffectFromItem(Hero hero, Item item)
        {
            if (item.AdditionalEffect.ToString() == ItemConstants.Attack)
            {
                hero.Attack -= AdditionalAttackFromItem;
            }
            else if (item.AdditionalEffect.ToString() == MagicAttack)
            {
                hero.MagicAttack -= AdditionalMagicAttackFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Health)
            {
                hero.Health -= AdditionalHealthFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Armor)
            {
                hero.Armor -= AdditionalArmorFromItem;
            }
            else if (item.AdditionalEffect.ToString() == MagicResistance)
            {
                hero.MagicResistance -= AdditionalMagicResistanceFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Speed)
            {
                hero.Speed -= AdditionalSpeedFromItem;
            }
        }

        private List<Hero> GetPlayerHeroes(string userId)
            => this.context.Players.Where(p => p.UserId == userId)
                  .Select(p => p.Heroes).FirstOrDefault().ToList();

        private PlayerInventoryViewModel GetPlayerInventory(string userId)
            => this.context.Players
              .Where(p => p.UserId == userId)
              .Select(pi => new PlayerInventoryViewModel
              {
                  Id = pi.Id,
                  BoughtItems = pi.Inventory
              }).FirstOrDefault();
    }
}
