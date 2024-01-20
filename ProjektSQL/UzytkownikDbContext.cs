// using Microsoft.Analytics.Interfaces;
// using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Entity;
using ProjektSQL;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class UzytkownikDbContext : DbContext
    {
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Konto> Konta { get; set; }
        public DbSet<Wplyw> Wplywy { get; set; }
        public DbSet<WydatekRaz> Wydatki { get; set; }
        public DbSet<Sesja> Sesje { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WydatekRaz>().ToTable("WydatkiRaz");
            modelBuilder.Entity<WydatekStaly>().ToTable("WydatkiStale");
        }
    }
}