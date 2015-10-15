using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F1Tippspiel.Db.Tools;

namespace F1Tippspiel.Test.Tools
{
	[TestClass]
	public class GeneratorTest
	{
		[TestMethod]
		public void Generator_generates_correct_values()
		{
			string test = Generator.GenerateNumberString(5);
			Assert.AreEqual(5, test.Length);
		}

		[TestMethod]
		public void Generator_doesnt_crash_on_to_long_values()
		{
			string test = Generator.GenerateNumberString(10);
			Assert.AreEqual(9, test.Length);
		}
	}
}
