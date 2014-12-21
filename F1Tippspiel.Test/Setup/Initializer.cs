using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Test.Setup
{
    public interface IInitializer
    {
        void SeedData(DbContext context);
    }
}
