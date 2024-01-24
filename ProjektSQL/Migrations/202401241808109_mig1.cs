namespace ProjektSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kontoes",
                c => new
                    {
                        IdKonta = c.Int(nullable: false, identity: true),
                        NazwaBanku = c.String(),
                        StanKonta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Nazwa = c.String(),
                        Uzytkownik_IdUzytkownika = c.Int(),
                    })
                .PrimaryKey(t => t.IdKonta)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.Uzytkownik_IdUzytkownika);
            
            CreateTable(
                "dbo.Oszczednoscs",
                c => new
                    {
                        IdOszczednosci = c.Int(nullable: false, identity: true),
                        IdKonta = c.Int(nullable: false),
                        Cel = c.String(),
                        Kwota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Kategoria = c.String(),
                        Uzytkownik_IdUzytkownika = c.Int(),
                    })
                .PrimaryKey(t => t.IdOszczednosci)
                .ForeignKey("dbo.Kontoes", t => t.IdKonta, cascadeDelete: true)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.IdKonta)
                .Index(t => t.Uzytkownik_IdUzytkownika);
            
            CreateTable(
                "dbo.Uzytkowniks",
                c => new
                    {
                        IdUzytkownika = c.Int(nullable: false, identity: true),
                        Imie = c.String(),
                        StanGotowki = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdUzytkownika);
            
            CreateTable(
                "dbo.Sesjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUzytkownika = c.Int(nullable: false),
                        Zalogowany = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Uzytkowniks", t => t.IdUzytkownika, cascadeDelete: true)
                .Index(t => t.IdUzytkownika);
            
            CreateTable(
                "dbo.WplywRazs",
                c => new
                    {
                        IdWplywu = c.Int(nullable: false, identity: true),
                        IdKonta = c.Int(nullable: false),
                        Kwota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Kategoria = c.String(),
                        Uzytkownik_IdUzytkownika = c.Int(),
                    })
                .PrimaryKey(t => t.IdWplywu)
                .ForeignKey("dbo.Kontoes", t => t.IdKonta, cascadeDelete: true)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.IdKonta)
                .Index(t => t.Uzytkownik_IdUzytkownika);
            
            CreateTable(
                "dbo.WydatkiRaz",
                c => new
                    {
                        IdWydatek = c.Int(nullable: false, identity: true),
                        IdKonta = c.Int(nullable: false),
                        Kategoria = c.String(),
                        Kwota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Uzytkownik_IdUzytkownika = c.Int(),
                    })
                .PrimaryKey(t => t.IdWydatek)
                .ForeignKey("dbo.Kontoes", t => t.IdKonta, cascadeDelete: true)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.IdKonta)
                .Index(t => t.Uzytkownik_IdUzytkownika);
            
            CreateTable(
                "dbo.WplywStalies",
                c => new
                    {
                        IdWydatekStaly = c.Int(nullable: false, identity: true),
                        IdKonta = c.Int(nullable: false),
                        CyklWplywu = c.Int(nullable: false),
                        Kwota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Kategoria = c.String(),
                        Uzytkownik_IdUzytkownika = c.Int(),
                    })
                .PrimaryKey(t => t.IdWydatekStaly)
                .ForeignKey("dbo.Kontoes", t => t.IdKonta, cascadeDelete: true)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.IdKonta)
                .Index(t => t.Uzytkownik_IdUzytkownika);
            
            CreateTable(
                "dbo.WydatkiStale",
                c => new
                    {
                        IdWydatkuStalego = c.Int(nullable: false, identity: true),
                        IdKonta = c.Int(nullable: false),
                        Kategoria = c.String(),
                        CyklWydatku = c.Int(nullable: false),
                        OplaconyWBiezacymCyklu = c.Boolean(nullable: false),
                        StalaKwota = c.Boolean(nullable: false),
                        Kwota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Uzytkownik_IdUzytkownika = c.Int(),
                    })
                .PrimaryKey(t => t.IdWydatkuStalego)
                .ForeignKey("dbo.Kontoes", t => t.IdKonta, cascadeDelete: true)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.IdKonta)
                .Index(t => t.Uzytkownik_IdUzytkownika);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WydatkiStale", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.WydatkiStale", "IdKonta", "dbo.Kontoes");
            DropForeignKey("dbo.WplywStalies", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.WplywStalies", "IdKonta", "dbo.Kontoes");
            DropForeignKey("dbo.WydatkiRaz", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.WydatkiRaz", "IdKonta", "dbo.Kontoes");
            DropForeignKey("dbo.WplywRazs", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.WplywRazs", "IdKonta", "dbo.Kontoes");
            DropForeignKey("dbo.Sesjas", "IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.Oszczednoscs", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.Kontoes", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.Oszczednoscs", "IdKonta", "dbo.Kontoes");
            DropIndex("dbo.WydatkiStale", new[] { "Uzytkownik_IdUzytkownika" });
            DropIndex("dbo.WydatkiStale", new[] { "IdKonta" });
            DropIndex("dbo.WplywStalies", new[] { "Uzytkownik_IdUzytkownika" });
            DropIndex("dbo.WplywStalies", new[] { "IdKonta" });
            DropIndex("dbo.WydatkiRaz", new[] { "Uzytkownik_IdUzytkownika" });
            DropIndex("dbo.WydatkiRaz", new[] { "IdKonta" });
            DropIndex("dbo.WplywRazs", new[] { "Uzytkownik_IdUzytkownika" });
            DropIndex("dbo.WplywRazs", new[] { "IdKonta" });
            DropIndex("dbo.Sesjas", new[] { "IdUzytkownika" });
            DropIndex("dbo.Oszczednoscs", new[] { "Uzytkownik_IdUzytkownika" });
            DropIndex("dbo.Oszczednoscs", new[] { "IdKonta" });
            DropIndex("dbo.Kontoes", new[] { "Uzytkownik_IdUzytkownika" });
            DropTable("dbo.WydatkiStale");
            DropTable("dbo.WplywStalies");
            DropTable("dbo.WydatkiRaz");
            DropTable("dbo.WplywRazs");
            DropTable("dbo.Sesjas");
            DropTable("dbo.Uzytkowniks");
            DropTable("dbo.Oszczednoscs");
            DropTable("dbo.Kontoes");
        }
    }
}
