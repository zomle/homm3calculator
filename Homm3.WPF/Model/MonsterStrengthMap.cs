namespace Homm3.WPF.Model
{
	public class MonsterStrengthMap
	{
		public string Name { get; set; }
		public int Value { get; set; }

		public MonsterStrengthMap(string name, int value)
		{
			Name = name;
			Value = value;
		}
	}
}
