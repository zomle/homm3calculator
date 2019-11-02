using System;
using System.Diagnostics.CodeAnalysis;

namespace Homm3.WPF.Model
{
	public class MapObject : IComparable<MapObject>, IFilterable
	{
		public string Name { get; set; }
		public int AiValue { get; set; }

		public string OrderingDescription { get; private set; }

		public MapObject(string name, int aiValue, string orderingDescription = null)
		{
			Name = name;
			AiValue = aiValue;
			OrderingDescription = orderingDescription ?? name;
		}

		public virtual int GetAiValue(UserInput userInput)
		{
			return AiValue;
		}

		public int CompareTo([AllowNull] MapObject other)
		{
			return OrderingDescription.CompareTo(other.OrderingDescription);
		}
	}
}
