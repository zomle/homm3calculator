using Homm3.WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homm3.WPF
{
	public class UserInput
	{
		public MonsterStrengthMap SelectedMonsterStrengthMap { get; set; }
		public MonsterStrengthZone SelectedMonsterStrengthZone { get; set; }
		
		public Dictionary<int, MapObject> SelectedMapObjects { get; set; }
		public Dictionary<Town, int> TownZoneCounts { get; set; }
		public int TotalTownZoneCount { get; set; }

		public Monster SelectedMonster { get; set; }
		public MonsterSize SelectedMonsterSize { get; set; }

		public int ZoneConnectionValue { get; set; }
		public int CurrentWeek { get; set; }
		
		public bool IsZoneGuard
		{
			get
			{
				return SelectedMonsterStrengthZone == null ? false : SelectedMonsterStrengthZone.Name == ZoneStrength.ZoneGuard;
			}
		}

		public UserInput()
		{
			SelectedMapObjects = new Dictionary<int, MapObject>();
			TownZoneCounts = new Dictionary<Town, int>();
		}

		public bool HasUnknownMapObject()
		{
			return SelectedMapObjects.Values.AsEnumerable().OfType<UnknownMapObject>().Any();
		}

		internal bool HasDwellingMapObject()
		{
			return SelectedMapObjects.Values.AsEnumerable().OfType<DwellingMapObject>().Any();
		}

		public IReadOnlyList<UnknownMapObject> GetUnknownMapObjects()
		{
			return SelectedMapObjects.Values.AsEnumerable().OfType<UnknownMapObject>().ToList();
		}

		public int GetMapObjectAiValue(int index)
		{
			if (!SelectedMapObjects.TryGetValue(index, out var mapObject) || mapObject == null)
			{
				return 0;
			}

			return mapObject.GetAiValue(this);
		}

		internal int GetZoneCount(Town town)
		{
			if (!TownZoneCounts.TryGetValue(town, out var zoneCount))
			{
				return 0;
			}
			return zoneCount;
		}
	}
}
