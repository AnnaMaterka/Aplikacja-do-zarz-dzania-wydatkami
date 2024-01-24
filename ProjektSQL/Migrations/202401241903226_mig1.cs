namespace ProjektSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WydatkiStale", "Uzytkownik_IdUzytkownika", c => c.Int());
            CreateIndex("dbo.WydatkiStale", "Uzytkownik_IdUzytkownika");
            AddForeignKey("dbo.WydatkiStale", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks", "IdUzytkownika");
            DropColumn("dbo.WydatkiRaz", "IdKategorii");
            DropColumn("dbo.WydatkiStale", "IdKategorii");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WydatkiStale", "IdKategorii", c => c.Int(nullable: false));
            AddColumn("dbo.WydatkiRaz", "IdKategorii", c => c.Int(nullable: false));
            DropForeignKey("dbo.WydatkiStale", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropIndex("dbo.WydatkiStale", new[] { "Uzytkownik_IdUzytkownika" });
            DropColumn("dbo.WydatkiStale", "Uzytkownik_IdUzytkownika");
        }
    }
}
