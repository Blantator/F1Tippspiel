using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using F1Tippspiel.Db.Data;
using F1Tippspiel.Db.Initializer;

namespace F1Tippspiel.Web.App_Start
{
    public class DbConfig
    {
        public static void RegisterDatabase()
        {
            //No DB config required in Web project
        }
    }
}