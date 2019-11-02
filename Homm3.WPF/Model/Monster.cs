namespace Homm3.WPF.Model
{
	public class Monster : IFilterable
	{
		public Town Town { get; set; }
		public string Name { get; set; }
		public int AiValue { get; set; }
		public int Growth { get; set; }

		public Monster(Town town, string name, int aiValue, int growth)
		{
			Town = town;
			Name = name;
			AiValue = aiValue;
			Growth = growth;
		}
	}
}
