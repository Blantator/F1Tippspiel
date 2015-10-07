using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1Tippspiel.Db.Data;

namespace F1Tippspiel.Test.Setup
{
    public class DbSetup
    {
        /// <summary>
        /// Create a new inmemory db instance and fill it with test data
        /// </summary>
        /// <param name="initializer">initializer to be used for filling initial test data</param>
        /// <returns>In-memory db instance</returns>
        public AppContext CreateTransientDb(IInitializer initializer)
        {
            DbConnection con = Effort.DbConnectionFactory.CreateTransient();
            AppContext db = new AppContext(con);
            initializer.SeedData(db);
            return db;
        }
    }
}
