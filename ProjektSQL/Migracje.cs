using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikacja_do_zarzadzania_wydatkami;

namespace ProjektSQL
{
    internal sealed class Migracje : DbMigrationsConfiguration<UzytkownikDbContext>
    {
        public Migracje()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
