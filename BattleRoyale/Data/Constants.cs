

namespace BattleRoyale.Data
{
    public class Constants
    {
        public class DatabaseConstants
        {
            public const int DefaultMaxLengthForName = 20;
            public const int DefaultMinLengthForName = 5;
            public const int MaxPriceForItem = 5000;
            public const int MinPriceForItem = 100;
            public const int MaxRequiredLevelForItem = 100;
            public const int MinRequiredLevelForItem = 1;
            public const int ItemPerPage = 20;
            public const int PlayerPerPage = 20;
        }

        public class PlayerConstants
        {
            public const int InitialPlayerLevel = 1;
            public const int InitialPlayerGold = 1000;
            public const int GoldOnLevelUp = 1000;
            public const int RequiredExperiencePoints = 1000;
            public const double AdditionalRequiredExperiencePointsOnLevelUp = 0.5;
            public const int PlayerExperiencePointsGainOnDefeat = 50;
            public const int PlayerExperiencePointsGainOnVictory = 100;
            public const int PlayerGoldGainOnDefeat = 50;
            public const int PlayerGoldGainOnVictory = 100;
        }

        public class HeroConstants
        {
            public const string Assassin = "Assassin";
            public const string Tank = "Tank";
            public const string Mage = "Mage";
            public const string InvalidHero = "Invalid hero type.";

            public const int InitialHeroLevel = 1;
            public const int InitialRequiredExperience = 2000;

            public const int InitialAssassinAttack = 10;
            public const int InitialAssassinMagicAttack = 0;
            public const int InitialAssassinHealth = 500;
            public const int InitialAssassinArmor = 30;
            public const int InitialAssassinMagicResistance = 20;
            public const int InitialAssassinSpeed = 100;

            public const int InitialTankAttack = 30;
            public const int InitialTankMagicAttack = 10;
            public const int InitialTankHealth = 700;
            public const int InitialTankArmor = 50;
            public const int InitialTankMagicResistance = 30;
            public const int InitialTankSpeed = 50;

            public const int InitialMageAttack = 20;
            public const int InitialMageMagicAttack = 50;
            public const int InitialMageHealth = 400;
            public const int InitialMageArmor = 30;
            public const int InitialMageMagicResistance = 20;
            public const int InitialMageSpeed = 90;

            public const string AssassinImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTfggACYYPQYNnJB-ZzveDW4b1sildTHxHcxg&usqp=CAU";
            public const string TankImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShVcj2klYHeZ1sxhtFjAiUVfliDcpwpxNlr2vE_CkrwQPPuG2Z-1xR_LqtEsrxH4VfDcw&usqp=CAU";
            public const string MageImage = "https://www.watchmojo.com/uploads/thumbs720/VG-RP-Top10-Wizards-In-VideoGames-720p30.jpg";

            public const int AssassinAttackOnLevelUp = 50;
            public const int AssassinMagicAttackOnLevelUp = 0;
            public const int AssassinHealthOnLevelUp = 500;
            public const int AssassinArmorOnLevelUp = 30;
            public const int AssassinMagicResistanceOnLevelUp = 20;
            public const int AssassinSpeedOnLevelUp = 100;

            public const int TankAttackOnLevelUp = 30;
            public const int TankMagicAttackOnLevelUp = 10;
            public const int TankHealthOnLevelUp = 700;
            public const int TankArmorOnLevelUp = 50;
            public const int TankMagicResistanceOnLevelUp = 30;
            public const int TankSpeedOnLevelUp = 50;

            public const int MageAttackOnLevelUp = 20;
            public const int MageMagicAttackOnLevelUp = 50;
            public const int MageHealthOnLevelUp = 400;
            public const int MageArmorOnLevelUp = 30;
            public const int MageMagicResistanceOnLevelUp = 20;
            public const int MageSpeedOnLevelUp = 90;


            public const double AdditionalRequiredExperienceAfterLevelUp = 0.33;
            public const int HeroExperiencePointsGainOnDefeat = 50;
            public const int HeroExperiencePointsGainOnVictory = 100;

            public const int AdditionalAttackFromItem = 30;
            public const int AdditionalMagicAttackFromItem = 20;
            public const int AdditionalHealthFromItem = 50;
            public const int AdditionalArmorFromItem = 30;
            public const int AdditionalMagicResistanceFromItem = 40;
            public const int AdditionalSpeedFromItem = 30;


        }

        public class ItemConstants
        {
            public const string Weapon = "Weapon";
            public const string Necklace = "Necklace";
            public const string Armor = "Armor";
            public const string MagicResistance = "MagicResistance";
            public const string Boots = "Boots";
            public const string InvalidItem = "Invalid item type.";

            public const string Attack = "Attack";
            public const string MagicAttack = "MagicAttack";
            public const string Health = "Health";
            public const string Speed = "Speed";

            public const int AssassinWeapon = 50;
            public const int AssassinNecklace = 30;
            public const int AssassinArmor = 40;
            public const int AssassinMagicResistance = 60;
            public const int AssassinBoots = 70;

            public const int TankWeapon = 40;
            public const int TankNecklace = 60;
            public const int TankArmor = 40;
            public const int TankMagicResistance = 60;
            public const int TankBoots = 60;

            public const int MageWeapon = 30;
            public const int MageNecklace = 70;
            public const int MageArmor = 50;
            public const int MageMagicResistance = 70;
            public const int MageBoots = 60;
        }

        public class PetConstants
        {
            public const int PetStats = 100;

            public const string Tiger = "Tiger";
            public const string Frog = "Frog";
            public const string Elephant = "Elephant";
            public const string Armadillo = "Armadillo";
            public const string Turtle = "Turtle";
            public const string Cheetah = "Cheetah";

            public const string TigerImage = "https://static01.nyt.com/images/2019/05/07/science/06SCI-TIGER1/03SCI-TIGER1-superJumbo.jpg";
            public const string FrogImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ949-_Y3Cz4VGphE78mAUN7uLk9-eW1YxOaQ&usqp=CAU";
            public const string ElephantImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTlQMc1DLUbPJ__PX97TJjsKryR7hgSfFxy5g&usqp=CAU";
            public const string ArmadilloImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTXRuE920L4iiXwVU848w5-JxAHEuQqWGuJrg&usqp=CAU";
            public const string TurtleImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT1b4VG2NvOc0KsHJgp7b2mePuCSC9q7LWS6A&usqp=CAU";
            public const string CheetahImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSZdpSgJSam4wvFgwZ44fa33J4xCSs9wzZWNQ&usqp=CAU";
        }

        public class ShopControllerConstants
        {
            public const string ExistingItem = "Item with this name already exists.";
            public const string OwnedItem = "You already have this item.";
            public const string NotEnoughGold = "You don't have enough gold to buy this item.";
        }

        public class HeroControllerConstants
        {
            public const string HeroHasItem = "You cannot do that because of one of the following reasons:" +
                "\n1.The hero already has an item of that type." +
                "\n2.The hero level is too low." +
                "\n3.The item is up fo auction." +
                "\n4.The item is already equipped to another hero.";
            public const string RequirementsNotMet = "You don't have the requirements to add a hero.";
            public const string HeroCountCannotBeLessThanOne = "Your heroes can't be less than 1.";
            public const int MaxHeroLevel = 50;
        }

        public class PetControllerConstants
        {
            public const string HeroAlreadyHasPet = "This hero already has a pet.";
        }

        public class PlayerControllerConstants
        {
            public const int MaxPlayerLevel = 100;
            public const string PlayerNotRegistered = "The player is not registered yet!";
        }

        public class AuctionItemControllerConstants
        {
            public const string NotEnoughGold = "You don't have that much gold.";
            public const string NotLessThanZero = "The bid amount cannot be less than 0.";
            public const string BidAlreadyExists = "You have already placed a bid for this item.";
            public const string ItemIsEquipped = "An equipped item cannot be put up for auction.";
        }
    }
}
