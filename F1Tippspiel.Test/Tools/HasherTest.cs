using F1Tippspiel.Db.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Test.Tools
{
    [TestClass]
    public class HasherTest
    {
        [TestMethod]
        public void Inmemory_db_is_created_successful()
        {
            string text = "123@abc";
            string hashed = Hasher.GetHash(text);

            Console.WriteLine(hashed);

            Assert.AreEqual(hashed, "lCXDroz4HhR1EIx8qaz3C13z/quTXBkQ3Q5hj7Qx3aA=");
        }
    }
}
