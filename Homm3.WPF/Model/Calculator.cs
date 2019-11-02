using Homm3.WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homm3.WPF
{
	public class CalculatedValues
	{
		public int ProtectionIndex { get; set; }
		public CalculatedAiValues AiValues { get; set; }
		public CalculatedMonsterValues MonsterValues { get; set; }

		public CalculatedValues()
		{
			AiValues = new CalculatedAiValues();
			MonsterValues = new CalculatedMonsterValues();
		}
	}

	public class CalculatedMonsterValues
	{
		public int AverageMonsterCount { get; set; }
		public int MonsterCountDeviation { get; set; }
		public int MinimalCount { get { return AverageMonsterCount - MonsterCountDeviation; } }
		public int MaximalCount { get { return AverageMonsterCount + MonsterCountDeviation; } }
	}

	public class CalculatedAiValues 
	{
		public int ZoneConnectionValue { get; set; }
		public int Object1Value { get; set; }
		public int Object2Value { get; set; }
		public int Object3Value { get; set; }
		public int Object4Value { get; set; }
		public int Object5Value { get; set; }
		public int Object6Value { get; set; }

		public int TotalAiValue { get; set; }
	}

	public class Calculator
	{
		public static int AdjustForWeeklyGrowth(int number, int week)
		{
			double result = number;
			while (week > 1)
			{
				result = result * 1.1d;
				week--;
			}
			return (int)Math.Floor(result);
		}

		public static int ReverseWeeklyGrowth(int number, int week)
		{
			double result = number;
			while (week > 1)
			{
				result = result / 1.1d;
				week--;
			}
			return (int)Math.Ceiling(result);
		}

		public static bool IsMapObjectPossible(UserInput userInput, MapObject additionalMapObject)
		{
			if (userInput.SelectedMonsterSize == null)
			{
				return false;
			}

			var calculatedValue = CalculateValues(userInput, additionalMapObject);

			var calcMin = calculatedValue.MonsterValues.MinimalCount;
			var calcMax = calculatedValue.MonsterValues.MaximalCount;
			
			var enteredMin = ReverseWeeklyGrowth(userInput.SelectedMonsterSize.MinValue, userInput.CurrentWeek);
			var enteredMax = ReverseWeeklyGrowth(userInput.SelectedMonsterSize.MaxValue, userInput.CurrentWeek);

			if ((calcMin <= enteredMin && enteredMin <= calcMax) || (calcMin <= enteredMax && enteredMax <= calcMax) 
			 || (enteredMin <= calcMin && calcMin <= enteredMax) || (enteredMin <= calcMax && calcMax <= enteredMax))
			{
				return true;
			}
			else
			{
				return false;
			}			
		}

		public static CalculatedValues CalculateValues(UserInput userInput, MapObject additionalMapObject = null)
		{
			var result = new CalculatedValues();
			
			int monsterStrengthMap = userInput.SelectedMonsterStrengthMap?.Value ?? 0;
			int monsterStrengthZone = userInput.SelectedMonsterStrengthZone?.Value ?? 0;
			result.ProtectionIndex = monsterStrengthMap + monsterStrengthZone;

			result.AiValues = CalculateAiValues(userInput, result.ProtectionIndex, additionalMapObject?.AiValue ?? 0);

			if (userInput.SelectedMonster != null)
			{
				result.MonsterValues = CalculatedMonsterValues(result.AiValues.TotalAiValue, userInput.SelectedMonster.AiValue);
			}
			return result;
		}

		private static CalculatedAiValues CalculateAiValues(UserInput userInput, int protectionIndex, int additionalAiValue = 0)
		{
			var result = new CalculatedAiValues();

			bool zoneGuard = userInput.IsZoneGuard;

			if (zoneGuard)
			{
				result.Object1Value = 0;
				result.Object2Value = 0;
				result.Object3Value = 0;
				result.Object4Value = 0;
				result.Object5Value = 0;
				result.Object6Value = 0;
				result.ZoneConnectionValue = userInput.ZoneConnectionValue;
			}
			else
			{
				result.Object1Value = userInput.GetMapObjectAiValue(1);
				result.Object2Value = userInput.GetMapObjectAiValue(2);
				result.Object3Value = userInput.GetMapObjectAiValue(3);
				result.Object4Value = userInput.GetMapObjectAiValue(4);
				result.Object5Value = userInput.GetMapObjectAiValue(5);
				result.Object6Value = userInput.GetMapObjectAiValue(6);
				result.ZoneConnectionValue = 0;
			}

			int totalObjectValue = result.ZoneConnectionValue
				+ Math.Max(0, result.Object1Value)
				+ Math.Max(0, result.Object2Value)
				+ Math.Max(0, result.Object3Value)
				+ Math.Max(0, result.Object4Value)
				+ Math.Max(0, result.Object5Value)
				+ Math.Max(0, result.Object6Value)
				+ additionalAiValue;

			int minimalValue1;
			float coefficient1;
			int minimalValue2;
			float coefficient2;
			switch (protectionIndex)
			{
				case 1:
					minimalValue1 = 2500;
					coefficient1 = 0.5f;
					minimalValue2 = 7500;
					coefficient2 = 0.5f;
					break;

				case 2:
					minimalValue1 = 1500;
					coefficient1 = 0.75f;
					minimalValue2 = 7500;
					coefficient2 = 0.75f;
					break;

				case 3:
					minimalValue1 = 1000;
					coefficient1 = 1f;
					minimalValue2 = 7500;
					coefficient2 = 1f;
					break;

				case 4:
					minimalValue1 = 500;
					coefficient1 = 1.5f;
					minimalValue2 = 5000;
					coefficient2 = 1f;
					break;

				case 5:
					minimalValue1 = 0;
					coefficient1 = 1.5f;
					minimalValue2 = 5000;
					coefficient2 = 1.5f;
					break;

				default:
					return result;
			}

			result.TotalAiValue = (int)(Math.Max(totalObjectValue - minimalValue1, 0) * coefficient1 + Math.Max(totalObjectValue - minimalValue2, 0) * coefficient2);

			return result;
		}

		private static CalculatedMonsterValues CalculatedMonsterValues(int totalAiValue, int monsterAiValue)
		{
			var result = new CalculatedMonsterValues();

			result.AverageMonsterCount = (int)Math.Round((double)totalAiValue / monsterAiValue);
			result.MonsterCountDeviation = result.AverageMonsterCount >= 4 ? result.AverageMonsterCount / 4 : 0;

			return result;
		}

		internal static Dictionary<string, List<string>> GuessPossibleObjects(UserInput userInput)
		{
			var objs = userInput.GetUnknownMapObjects();
			if (objs.Count != 1)
			{
				return new Dictionary<string, List<string>>();
			}

			var obj = objs.Single();
			var possibleMapObjects = new List<GuessableMapObject>();
			foreach (var item in obj.Candidates)
			{
				if (Calculator.IsMapObjectPossible(userInput, item))
				{
					possibleMapObjects.Add(item);
				}
			}

			return possibleMapObjects.GroupBy(po => po.Category, po => po.SummarizedName).ToDictionary(kv => kv.Key, kv => kv.ToList());
		}
	}
}
