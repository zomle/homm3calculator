namespace Homm3.WPF.Model
{
	public class Monster : IFilterable
	{
		public Town Town { get; set; }
		public string Name { get; set; }
		public int AiValue { get; set; }
		public int Growth { get; set; }
		public int UpgradeLevel { get; set; } 

		public string DisplayName
		{
			get
			{
				if (UpgradeLevel == 0)
				{
					return Name;
				}
				else if (UpgradeLevel == 1)
				{
					return $"{Name} (upg.)";
				}
				else
				{
					return $"{Name} ({UpgradeLevel}x upg.)";
				}
			}
		}

		public Monster(Town town, string name, int aiValue, int growth, int upgradeLevel = 0)
		{
			Town = town;
			Name = name;
			AiValue = aiValue;
			Growth = growth;
			UpgradeLevel = upgradeLevel;
		}
	}
}
