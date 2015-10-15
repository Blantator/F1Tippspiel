using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Tools
{
	/// <summary>
	/// Used for generating all kind of stuff
	/// </summary>
	public class Generator
	{
		public static string GenerateNumberString(int length)
		{
			if(Math.Pow(10, length) > int.MaxValue){
				length = 9;
			}
			string genNumber = "";
			while (genNumber.Length != length)
			{
				genNumber = new Random().Next(Convert.ToInt32(Math.Pow(10, length-1)), Convert.ToInt32(Math.Pow(10, length))).ToString();
			}
			return genNumber;
		}
	}
}
