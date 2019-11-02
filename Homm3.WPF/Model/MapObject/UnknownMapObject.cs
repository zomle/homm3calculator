using System.Collections.Generic;
using System.Linq;

namespace Homm3.WPF.Model
{
	public class UnknownMapObject : MapObject
	{
		public List<GuessableMapObject> Candidates { get; }
		public string CommonObjectPrefix { get; set; }
		public string Warning { get; set; }

		public UnknownMapObject(string name, string commonObjectPrefix, string warning, IEnumerable<GuessableMapObject> possibleMapObjects) 
			: base(name, -1)
		{
			CommonObjectPrefix = commonObjectPrefix;
			Candidates = possibleMapObjects.ToList();
			Warning = warning;
		}
	}
}
