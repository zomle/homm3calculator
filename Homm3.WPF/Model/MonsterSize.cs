namespace Homm3.WPF.Model
{
	public class MonsterSize
	{
		public bool IsSelectable { get; set; }

		public string Name { get; set; }
		public int MinValue { get; set; }
		public int MaxValue { get; set; }

		public string DisplayName
		{
			get
			{
				if (!IsSelectable)
				{
					return Name;
				}

				if (MinValue == MaxValue)
				{
					return $"Exactly {MinValue}";
				}
				else
				{
					return $"{MinValue}-{MaxValue}";
				}
			}
		}

		public string DetailedName
		{
			get
			{
				if (!IsSelectable)
				{
					return Name;
				}

				if (MinValue == MaxValue)
				{
					return $"Exactly {MinValue}";
				}
				else
				{
					return $"{Name} ({MinValue}-{MaxValue})";
				}
			}
		}

		public MonsterSize(string name) : this(name, 0, 0)
		{
			IsSelectable = false;
		}

		public MonsterSize(int exactNumber) : this(null, exactNumber, exactNumber)
		{
		}

		public MonsterSize(string name, int min, int max)
		{
			IsSelectable = true;
			Name = name;
			MinValue = min;
			MaxValue = max;
		}
	}
}
