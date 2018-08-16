using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Backgammon.DAL
{
    class BackgammonContextInitializer : DropCreateDatabaseIfModelChanges<BackgammonContext>
    {
        protected override void Seed(BackgammonContext context)
        {
            base.Seed(context);
        }
    }
}
