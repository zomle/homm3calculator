using Homm3.WPF.Model;
using System.Collections.Generic;
using System.Linq;

namespace Homm3.WPF
{
	public static class MonsterFactory
	{
		private static readonly Dictionary<string, Monster> monsters;

		static MonsterFactory()
		{
			monsters = new Dictionary<string, Monster>();

			foreach (var monster in CreateMonsters())
			{
				monsters.Add(monster.Name, monster);
			}
		}

		public static List<Monster> ListMonsters()
		{
			return monsters.Values.ToList();
		}

		public static Monster GetMonster(string name)
		{
			return monsters[name];
		}

		private static IEnumerable<Monster> CreateMonsters()
		{
			yield return new Monster(Town.Conflux, "Air Elemental", 356, 6);
			yield return new Monster(Town.Stronghold, "Ancient Behemoth", 6168, 1, 1);
			yield return new Monster(Town.Castle, "Angel", 5019, 1);
			yield return new Monster(Town.Inferno, "Arch Devil", 7115, 1, 1);
			yield return new Monster(Town.Tower, "Arch Mage", 680, 4, 1);
			yield return new Monster(Town.Castle, "Archangel", 8776, 1, 1);
			yield return new Monster(Town.Castle, "Archer", 126, 9);
			yield return new Monster(Town.Factory, "Armadillo", 198, 6);
			yield return new Monster(Town.Factory, "Automaton", 669, 5);
			yield return new Monster(Town.Cove, "Ayssid", 645, 4, 1);
			yield return new Monster(Town.Neutral, "Azure Dragon", 78845, 1);
			yield return new Monster(Town.Fortress, "Basilisk", 552, 4);
			yield return new Monster(Town.Rampart, "Battle Dwarf", 209, 8, 1);
			yield return new Monster(Town.Stronghold, "Behemoth", 3162, 1);
			yield return new Monster(Town.Dungeon, "Beholder", 336, 7);
			yield return new Monster(Town.Factory, "Bellwether Armadillo", 256, 6,1);
			yield return new Monster(Town.Dungeon, "Black Dragon", 8721, 1, 1);
			yield return new Monster(Town.Necropolis, "Black Knight", 2084, 2);
			yield return new Monster(Town.Neutral, "Boar", 145, 8);
			yield return new Monster(Town.Necropolis, "Bone Dragon", 3388, 1);
			yield return new Monster(Town.Factory, "Bounty Hunter", 1454, 2,1);
			yield return new Monster(Town.Castle, "Cavalier", 1946, 2);
			yield return new Monster(Town.Rampart, "Centaur Captain", 138, 14, 1);
			yield return new Monster(Town.Rampart, "Centaur", 100, 14);
			yield return new Monster(Town.Inferno, "Cerberus", 392, 5, 1);
			yield return new Monster(Town.Castle, "Champion", 2100, 2, 1);
			yield return new Monster(Town.Fortress, "Chaos Hydra", 5931, 1, 1);
			yield return new Monster(Town.Cove, "Corsair", 407, 7, 1);
			yield return new Monster(Town.Factory, "Couatl", 3574, 1);
			yield return new Monster(Town.Cove, "Crew Mate", 155, 9);
			yield return new Monster(Town.Factory, "Crimson Couatl", 5341, 1, 1);
			yield return new Monster(Town.Castle, "Crusader", 558, 4, 1);
			yield return new Monster(Town.Neutral, "Crystal Dragon", 39338, 1);
			yield return new Monster(Town.Stronghold, "Cyclops King", 1443, 2, 1);
			yield return new Monster(Town.Stronghold, "Cyclops", 1266, 2);
			yield return new Monster(Town.Inferno, "Demon", 445, 4);
			yield return new Monster(Town.Rampart, "Dendroid Guard", 517, 3);
			yield return new Monster(Town.Rampart, "Dendroid Soldier", 803, 3, 1);
			yield return new Monster(Town.Inferno, "Devil", 5101, 1);
			yield return new Monster(Town.Neutral, "Diamond Golem", 775, 2);
			yield return new Monster(Town.Fortress, "Dragon Fly", 312, 8, 1);
			yield return new Monster(Town.Necropolis, "Dread Knight", 2382, 2, 1);
			yield return new Monster(Town.Factory, "Dreadnought", 3879, 1);
			yield return new Monster(Town.Rampart, "Dwarf", 138, 8);
			yield return new Monster(Town.Conflux, "Earth Elemental", 330, 4);
			yield return new Monster(Town.Inferno, "Efreet Sultan", 2343, 2, 1);
			yield return new Monster(Town.Inferno, "Efreet", 1670, 2);
			yield return new Monster(Town.Neutral, "Enchanter", 1210, 2);
			yield return new Monster(Town.Conflux, "Energy Elemental", 470, 5, 1);
			yield return new Monster(Town.Factory, "Engineer", 278, 8, 1);
			yield return new Monster(Town.Dungeon, "Evil Eye", 367, 7, 1);
			yield return new Monster(Town.Neutral, "Faerie Dragon", 30501, 1);
			yield return new Monster(Town.Inferno, "Familiar", 60, 15, 1);
			yield return new Monster(Town.Neutral, "Fangarm", 929, 3);
			yield return new Monster(Town.Conflux, "Fire Elemental", 345, 5);
			yield return new Monster(Town.Conflux, "Firebird", 4336, 1);
			yield return new Monster(Town.Tower, "Genie", 884, 3);
			yield return new Monster(Town.Necropolis, "Ghost Dragon", 4696, 1, 1);
			yield return new Monster(Town.Tower, "Giant", 3718, 1);
			yield return new Monster(Town.Fortress, "Gnoll Marauder", 90, 12, 1);
			yield return new Monster(Town.Fortress, "Gnoll", 56, 12);
			yield return new Monster(Town.Stronghold, "Goblin", 60, 15);
			yield return new Monster(Town.Inferno, "Gog", 159, 8);
			yield return new Monster(Town.Rampart, "Gold Dragon", 8613, 1, 1);
			yield return new Monster(Town.Neutral, "Gold Golem", 600, 3);
			yield return new Monster(Town.Fortress, "Gorgon", 890, 3);
			yield return new Monster(Town.Rampart, "Grand Elf", 331, 7, 1);
			yield return new Monster(Town.Fortress, "Greater Basilisk", 714, 4, 1);
			yield return new Monster(Town.Rampart, "Green Dragon", 4872, 1);
			yield return new Monster(Town.Tower, "Gremlin", 44, 16);
			yield return new Monster(Town.Castle, "Griffin", 351, 7);
			yield return new Monster(Town.Factory, "Gunslinger", 1351, 2);
			yield return new Monster(Town.Castle, "Halberdier", 115, 14, 1);
			yield return new Monster(Town.Factory, "Halfling", 75, 15);
			yield return new Monster(Town.Factory, "Halfling Grenadier", 95, 15, 1);
			yield return new Monster(Town.Dungeon, "Harpy Hag", 238, 8, 1);
			yield return new Monster(Town.Dungeon, "Harpy", 154, 8);
			yield return new Monster(Town.Cove, "Haspid", 7220, 1, 1);
			yield return new Monster(Town.Inferno, "Hell Hound", 357, 5);
			yield return new Monster(Town.Stronghold, "Hobgoblin", 78, 15, 1);
			yield return new Monster(Town.Inferno, "Horned Demon", 480, 4, 1);
			yield return new Monster(Town.Fortress, "Hydra", 4120, 1);
			yield return new Monster(Town.Conflux, "Ice Elemental", 380, 6, 1);
			yield return new Monster(Town.Inferno, "Imp", 50, 15);
			yield return new Monster(Town.Dungeon, "Infernal Troglodyte", 84, 14, 1);
			yield return new Monster(Town.Tower, "Iron Golem", 412, 6, 1);
			yield return new Monster(Town.Factory, "Juggernaut", 6433, 1, 1);
			yield return new Monster(Town.Neutral, "Leprechaun", 208, 9);
			yield return new Monster(Town.Necropolis, "Lich", 848, 3);
			yield return new Monster(Town.Fortress, "Lizard Warrior", 209, 9, 1);
			yield return new Monster(Town.Fortress, "Lizardman", 151, 9);
			yield return new Monster(Town.Tower, "Mage", 570, 4);
			yield return new Monster(Town.Conflux, "Magic Elemental", 2012, 2, 1);
			yield return new Monster(Town.Conflux, "Magma Elemental", 490, 4, 1);
			yield return new Monster(Town.Inferno, "Magog", 240, 8, 1);
			yield return new Monster(Town.Dungeon, "Manticore", 1547, 2);
			yield return new Monster(Town.Castle, "Marksman", 184, 9, 1);
			yield return new Monster(Town.Tower, "Master Genie", 942, 3, 1);
			yield return new Monster(Town.Tower, "Master Gremlin", 66, 16, 1);
			yield return new Monster(Town.Factory, "Mechanic", 168, 8);
			yield return new Monster(Town.Dungeon, "Medusa Queen", 577, 4, 1);
			yield return new Monster(Town.Dungeon, "Medusa", 517, 4);
			yield return new Monster(Town.Fortress, "Mighty Gorgon", 1028, 3, 1);
			yield return new Monster(Town.Dungeon, "Minotaur King", 1068, 3, 1);
			yield return new Monster(Town.Dungeon, "Minotaur", 835, 3);
			yield return new Monster(Town.Castle, "Monk", 582, 3);
			yield return new Monster(Town.Neutral, "Mummy", 270, 7);
			yield return new Monster(Town.Tower, "Naga Queen", 2840, 2, 1);
			yield return new Monster(Town.Tower, "Naga", 2016, 2);
			yield return new Monster(Town.Cove, "Nix Warrior", 2116, 2, 1);
			yield return new Monster(Town.Cove, "Nix", 1415, 2);
			yield return new Monster(Town.Neutral, "Nomad", 345, 7);
			yield return new Monster(Town.Cove, "Nymph", 57, 16);
			yield return new Monster(Town.Tower, "Obsidian Gargoyle", 201, 9, 1);
			yield return new Monster(Town.Cove, "Oceanid", 75, 16, 1);
			yield return new Monster(Town.Stronghold, "Ogre Mage", 672, 4, 1);
			yield return new Monster(Town.Factory, "Olgoi-Khorkhoi", 1220, 3,1);
			yield return new Monster(Town.Stronghold, "Ogre", 416, 4);
			yield return new Monster(Town.Stronghold, "Orc Chieftain", 240, 7, 1);
			yield return new Monster(Town.Stronghold, "Orc", 192, 7);
			yield return new Monster(Town.Neutral, "Peasant", 15, 25);
			yield return new Monster(Town.Rampart, "Pegasus", 518, 5);
			yield return new Monster(Town.Conflux, "Phoenix", 6721, 1, 1);
			yield return new Monster(Town.Castle, "Pikeman", 80, 14);
			yield return new Monster(Town.Cove, "Pirate", 312, 7);
			yield return new Monster(Town.Inferno, "Pit Fiend", 765, 3);
			yield return new Monster(Town.Inferno, "Pit Lord", 1224, 3, 1);
			yield return new Monster(Town.Conflux, "Pixie", 55, 20);
			yield return new Monster(Town.Necropolis, "Power Lich", 1079, 3, 1);
			yield return new Monster(Town.Conflux, "Psychic Elemental", 1669, 2);
			yield return new Monster(Town.Dungeon, "Red Dragon", 4702, 1);
			yield return new Monster(Town.Stronghold, "Roc", 1027, 3);
			yield return new Monster(Town.Neutral, "Rogue", 135, 8);
			yield return new Monster(Town.Castle, "Royal Griffin", 448, 7, 1);
			yield return new Monster(Town.Neutral, "Rust Dragon", 26433, 1);
			yield return new Monster(Town.Factory, "Sandworm", 991, 3);
			yield return new Monster(Town.Neutral, "Satyr", 518, 4);
			yield return new Monster(Town.Dungeon, "Scorpicore", 1589, 2, 1);
			yield return new Monster(Town.Cove, "Sea Dog", 602, 7, 2);
			yield return new Monster(Town.Cove, "Sea Serpent", 3953, 1);
			yield return new Monster(Town.Cove, "Sea Witch", 790, 3);
			yield return new Monster(Town.Cove, "Seaman", 174, 9, 1);
			yield return new Monster(Town.Factory, "Sentinel Automaton", 947, 6,1);
			yield return new Monster(Town.Fortress, "Serpent Fly", 268, 8);
			yield return new Monster(Town.Neutral, "Sharpshooter", 585, 4);
			yield return new Monster(Town.Rampart, "Silver Pegasus", 532, 5, 1);
			yield return new Monster(Town.Necropolis, "Skeleton Warrior", 85, 12, 1);
			yield return new Monster(Town.Necropolis, "Skeleton", 60, 12);
			yield return new Monster(Town.Cove, "Sorceress", 852, 3, 1);
			yield return new Monster(Town.Conflux, "Sprite", 95, 20, 1);
			yield return new Monster(Town.Neutral, "Steel Golem", 597, 4);
			yield return new Monster(Town.Tower, "Stone Gargoyle", 165, 9);
			yield return new Monster(Town.Tower, "Stone Golem", 250, 6);
			yield return new Monster(Town.Conflux, "Storm Elemental", 486, 6, 1);
			yield return new Monster(Town.Cove, "Stormbird", 502, 4);
			yield return new Monster(Town.Castle, "Swordsman", 445, 4);
			yield return new Monster(Town.Stronghold, "Thunderbird", 1106, 3, 1);
			yield return new Monster(Town.Tower, "Titan", 7500, 1, 1);
			yield return new Monster(Town.Dungeon, "Troglodyte", 59, 14);
			yield return new Monster(Town.Neutral, "Troll", 1024, 3);
			yield return new Monster(Town.Rampart, "Unicorn", 1806, 2);
			yield return new Monster(Town.Necropolis, "Vampire Lord", 783, 4, 1);
			yield return new Monster(Town.Necropolis, "Vampire", 555, 4);
			yield return new Monster(Town.Necropolis, "Walking Dead", 98, 8);
			yield return new Monster(Town.Rampart, "War Unicorn", 2030, 2, 1);
			yield return new Monster(Town.Conflux, "Water Elemental", 315, 6);
			yield return new Monster(Town.Necropolis, "Wight", 252, 7);
			yield return new Monster(Town.Stronghold, "Wolf Raider", 203, 9, 1);
			yield return new Monster(Town.Stronghold, "Wolf Rider", 130, 9);
			yield return new Monster(Town.Rampart, "Wood Elf", 234, 7);
			yield return new Monster(Town.Necropolis, "Wraith", 315, 7, 1);
			yield return new Monster(Town.Fortress, "Wyvern Monarch", 1518, 2, 1);
			yield return new Monster(Town.Fortress, "Wyvern", 1350, 2);
			yield return new Monster(Town.Castle, "Zealot", 750, 3, 1);
			yield return new Monster(Town.Necropolis, "Zombie", 128, 8, 1);
		}
	}
}
