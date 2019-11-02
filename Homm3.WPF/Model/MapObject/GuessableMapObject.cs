namespace Homm3.WPF.Model
{
	public class GuessableMapObject : MapObject
	{
		public string Category { get; set; }
		public string SummarizedName { get; set; }

		public GuessableMapObject(string name, int aiValue, string orderingDescription, string category, string summarizedName) 
			: base(name, aiValue, orderingDescription)
		{
			Category = category;
			SummarizedName = summarizedName;
		}
	}
}
