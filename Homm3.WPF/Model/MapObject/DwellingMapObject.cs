using System;

namespace Homm3.WPF.Model
{
	public class DwellingMapObject : MapObject
	{
		public Monster[] Monsters { get; set; }
		public int ExtraMultiplier { get; set; }

		public DwellingMapObject(string name, params Monster[] monster) : base(name, 0)
		{
			Monsters = monster;
			ExtraMultiplier = 1;
		}

		public override int GetAiValue(UserInput userInput)
		{
			float result = 0.0f;

			foreach (var monster in Monsters)
			{
				result += GetAiValue(userInput, monster);
			}

			result = result / Math.Min(Monsters.Length, 2);

			return (int)Math.Floor(ExtraMultiplier * result);
		}

		private float GetAiValue(UserInput userInput, Monster monster)
		{
			float result;
			if (monster.Town == Town.Neutral)
			{
				result = monster.AiValue * monster.Growth;
			}
			else
			{
				float n = userInput.GetZoneCount(monster.Town);
				float N = userInput.TotalTownZoneCount;
				if (N == 0)
				{
					return 0.0f;
				}

				result = monster.AiValue * (monster.Growth * (1 + n / N) + n / 2);
			}

			return result;
		}
	}
}
