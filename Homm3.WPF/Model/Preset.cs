using Homm3.WPF.Model;
using System.Collections.Generic;

namespace Homm3.WPF
{
	public class Preset
	{
		public string Name { get; set; }
		public int? ZoneConnectionValue { get; set; }
		public ZoneStrength? ZoneMonsterStrength { get; set; }
		public int? NumberOfZonesWithTowns { get; set; }
	}

	public class Settings
	{
		public List<Preset> Presets { get; set; }
	}
}
