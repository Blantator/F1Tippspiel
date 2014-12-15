using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Data
{
    class AppDb : DbContext
    {
        public AppDb() : base("F1Db")
        {
            
        }

        public AppDb(string connection)
        {
            //todo: add overide
            String a = "";
        }
    }
}
