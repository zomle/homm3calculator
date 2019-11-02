namespace Homm3.WPF.Model
{
	public class MonsterStrengthZone
	{
		public ZoneStrength Name { get; set; }
		public int Value { get; set; }

		public MonsterStrengthZone(ZoneStrength name, int value)
		{
			Name = name;
			Value = value;
		}
	}
}
