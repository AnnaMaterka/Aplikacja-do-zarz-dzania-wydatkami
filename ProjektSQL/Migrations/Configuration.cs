namespace ProjektSQL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Aplikacja_do_zarzadzania_wydatkami.UzytkownikDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Aplikacja_do_zarzadzania_wydatkami.UzytkownikDbContext";
        }

        protected override void Seed(Aplikacja_do_zarzadzania_wydatkami.UzytkownikDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
