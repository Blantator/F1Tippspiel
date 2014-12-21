using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using F1Tippspiel.Db.Data;
using F1Tippspiel.Db.Game;
using F1Tippspiel.Test.Setup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace F1Tippspiel.Test.Web
{
    /// <summary>
    /// Basic input-output tests
    /// </summary>
    [TestClass]
    public class HomeControllerTest : DbSetup
    {
        public HomeControllerTest()
        {
        }

        [TestMethod]
        public void Inmemory_db_is_created_successful()
        {
            AppDb db = CreateTransientDb(new WebDbInitializer());

            Season s = db.Seasons.ToList()[0];

            Assert.IsNotNull(s, "the season could not be read from db");
            Assert.AreEqual(2014, s.Year, "returned year was not correct");
        }
    }
}
