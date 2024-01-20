namespace ProjektSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracja1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kontoes",
                c => new
                    {
                        IdKonta = c.Int(nullable: false, identity: true),
                        IdUzytkownika = c.Int(nullable: false),
                        NazwaBanku = c.String(),
                        StanKonta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Nazwa = c.String(),
                        Uzytkownik_IdUzytkownika = c.Int(),
                        Uzytkownik_IdUzytkownika1 = c.Int(),
                        Uzytkownik1_IdUzytkownika = c.Int(),
                    })
                .PrimaryKey(t => t.IdKonta)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika1)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik1_IdUzytkownika)
                .Index(t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.Uzytkownik_IdUzytkownika1)
                .Index(t => t.Uzytkownik1_IdUzytkownika);
            
            CreateTable(
                "dbo.Oszczednoscs",
                c => new
                    {
                        IdOszczednosci = c.Int(nullable: false, identity: true),
                        IdKonta = c.Int(nullable: false),
                        Cel = c.String(),
                        Kwota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Kategoria_IdKategorii = c.Int(),
                        Kategorie_IdKategorii = c.Int(),
                        Uzytkownik_IdUzytkownika = c.Int(),
                    })
                .PrimaryKey(t => t.IdOszczednosci)
                .ForeignKey("dbo.Kategorias", t => t.Kategoria_IdKategorii)
                .ForeignKey("dbo.Kategorias", t => t.Kategorie_IdKategorii)
                .ForeignKey("dbo.Kontoes", t => t.IdKonta, cascadeDelete: true)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.IdKonta)
                .Index(t => t.Kategoria_IdKategorii)
                .Index(t => t.Kategorie_IdKategorii)
                .Index(t => t.Uzytkownik_IdUzytkownika);
            
            CreateTable(
                "dbo.Kategorias",
                c => new
                    {
                        IdKategorii = c.Int(nullable: false, identity: true),
                        NazwaKategorii = c.String(),
                        IdWplywu1 = c.Int(nullable: false),
                        IdWydatek = c.Int(nullable: false),
                        IdWydatkuStalego = c.Int(nullable: false),
                        Wplyw_IdWplywu = c.Int(),
                        WydatekRaz_IdWydatek = c.Int(),
                        WydatekStaly_IdWydatkuStalego = c.Int(),
                    })
                .PrimaryKey(t => t.IdKategorii)
                .ForeignKey("dbo.Wplyws", t => t.Wplyw_IdWplywu)
                .ForeignKey("dbo.WydatkiRaz", t => t.WydatekRaz_IdWydatek)
                .ForeignKey("dbo.WydatkiStale", t => t.WydatekStaly_IdWydatkuStalego)
                .Index(t => t.Wplyw_IdWplywu)
                .Index(t => t.WydatekRaz_IdWydatek)
                .Index(t => t.WydatekStaly_IdWydatkuStalego);
            
            CreateTable(
                "dbo.Wplyws",
                c => new
                    {
                        IdWplywu = c.Int(nullable: false, identity: true),
                        Kwota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        CyklWplywu = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Kategoria_IdKategorii = c.Int(),
                        Kategorie_IdKategorii = c.Int(),
                        Uzytkownik_IdUzytkownika = c.Int(),
                        Konto_IdKonta = c.Int(),
                    })
                .PrimaryKey(t => t.IdWplywu)
                .ForeignKey("dbo.Kategorias", t => t.Kategoria_IdKategorii)
                .ForeignKey("dbo.Kategorias", t => t.Kategorie_IdKategorii)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .ForeignKey("dbo.Kontoes", t => t.Konto_IdKonta)
                .Index(t => t.Kategoria_IdKategorii)
                .Index(t => t.Kategorie_IdKategorii)
                .Index(t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.Konto_IdKonta);
            
            CreateTable(
                "dbo.WydatkiRaz",
                c => new
                    {
                        IdWydatek = c.Int(nullable: false, identity: true),
                        IdKonta = c.Int(nullable: false),
                        Kwota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Kategoria_IdKategorii = c.Int(),
                        Kategorie_IdKategorii = c.Int(),
                        Uzytkownik_IdUzytkownika = c.Int(),
                    })
                .PrimaryKey(t => t.IdWydatek)
                .ForeignKey("dbo.Kategorias", t => t.Kategoria_IdKategorii)
                .ForeignKey("dbo.Kategorias", t => t.Kategorie_IdKategorii)
                .ForeignKey("dbo.Kontoes", t => t.IdKonta, cascadeDelete: true)
                .ForeignKey("dbo.Uzytkowniks", t => t.Uzytkownik_IdUzytkownika)
                .Index(t => t.IdKonta)
                .Index(t => t.Kategoria_IdKategorii)
                .Index(t => t.Kategorie_IdKategorii)
                .Index(t => t.Uzytkownik_IdUzytkownika);
            
            CreateTable(
                "dbo.WydatkiStale",
                c => new
                    {
                        IdWydatkuStalego = c.Int(nullable: false, identity: true),
                        IdKonta = c.Int(nullable: false),
                        CyklWydatku = c.Int(nullable: false),
                        OplaconyWBiezacymCyklu = c.Boolean(nullable: false),
                        StalaKwota = c.Boolean(nullable: false),
                        Kwota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data = c.DateTime(nullable: false),
                        Kategoria_IdKategorii = c.Int(),
                        Kategorie_IdKategorii = c.Int(),
                    })
                .PrimaryKey(t => t.IdWydatkuStalego)
                .ForeignKey("dbo.Kategorias", t => t.Kategoria_IdKategorii)
                .ForeignKey("dbo.Kategorias", t => t.Kategorie_IdKategorii)
                .ForeignKey("dbo.Kontoes", t => t.IdKonta, cascadeDelete: true)
                .Index(t => t.IdKonta)
                .Index(t => t.Kategoria_IdKategorii)
                .Index(t => t.Kategorie_IdKategorii);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wplyws", "Konto_IdKonta", "dbo.Kontoes");
            DropForeignKey("dbo.Kontoes", "Uzytkownik1_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.Kontoes", "Uzytkownik_IdUzytkownika1", "dbo.Uzytkowniks");
            DropForeignKey("dbo.WydatkiRaz", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.Wplyws", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.Sesjas", "IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.Oszczednoscs", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.Kontoes", "Uzytkownik_IdUzytkownika", "dbo.Uzytkowniks");
            DropForeignKey("dbo.Oszczednoscs", "IdKonta", "dbo.Kontoes");
            DropForeignKey("dbo.Oszczednoscs", "Kategorie_IdKategorii", "dbo.Kategorias");
            DropForeignKey("dbo.Oszczednoscs", "Kategoria_IdKategorii", "dbo.Kategorias");
            DropForeignKey("dbo.Kategorias", "WydatekStaly_IdWydatkuStalego", "dbo.WydatkiStale");
            DropForeignKey("dbo.WydatkiStale", "IdKonta", "dbo.Kontoes");
            DropForeignKey("dbo.WydatkiStale", "Kategorie_IdKategorii", "dbo.Kategorias");
            DropForeignKey("dbo.WydatkiStale", "Kategoria_IdKategorii", "dbo.Kategorias");
            DropForeignKey("dbo.Kategorias", "WydatekRaz_IdWydatek", "dbo.WydatkiRaz");
            DropForeignKey("dbo.WydatkiRaz", "IdKonta", "dbo.Kontoes");
            DropForeignKey("dbo.WydatkiRaz", "Kategorie_IdKategorii", "dbo.Kategorias");
            DropForeignKey("dbo.WydatkiRaz", "Kategoria_IdKategorii", "dbo.Kategorias");
            DropForeignKey("dbo.Kategorias", "Wplyw_IdWplywu", "dbo.Wplyws");
            DropForeignKey("dbo.Wplyws", "Kategorie_IdKategorii", "dbo.Kategorias");
            DropForeignKey("dbo.Wplyws", "Kategoria_IdKategorii", "dbo.Kategorias");
            DropIndex("dbo.Sesjas", new[] { "IdUzytkownika" });
            DropIndex("dbo.WydatkiStale", new[] { "Kategorie_IdKategorii" });
            DropIndex("dbo.WydatkiStale", new[] { "Kategoria_IdKategorii" });
            DropIndex("dbo.WydatkiStale", new[] { "IdKonta" });
            DropIndex("dbo.WydatkiRaz", new[] { "Uzytkownik_IdUzytkownika" });
            DropIndex("dbo.WydatkiRaz", new[] { "Kategorie_IdKategorii" });
            DropIndex("dbo.WydatkiRaz", new[] { "Kategoria_IdKategorii" });
            DropIndex("dbo.WydatkiRaz", new[] { "IdKonta" });
            DropIndex("dbo.Wplyws", new[] { "Konto_IdKonta" });
            DropIndex("dbo.Wplyws", new[] { "Uzytkownik_IdUzytkownika" });
            DropIndex("dbo.Wplyws", new[] { "Kategorie_IdKategorii" });
            DropIndex("dbo.Wplyws", new[] { "Kategoria_IdKategorii" });
            DropIndex("dbo.Kategorias", new[] { "WydatekStaly_IdWydatkuStalego" });
            DropIndex("dbo.Kategorias", new[] { "WydatekRaz_IdWydatek" });
            DropIndex("dbo.Kategorias", new[] { "Wplyw_IdWplywu" });
            DropIndex("dbo.Oszczednoscs", new[] { "Uzytkownik_IdUzytkownika" });
            DropIndex("dbo.Oszczednoscs", new[] { "Kategorie_IdKategorii" });
            DropIndex("dbo.Oszczednoscs", new[] { "Kategoria_IdKategorii" });
            DropIndex("dbo.Oszczednoscs", new[] { "IdKonta" });
            DropIndex("dbo.Kontoes", new[] { "Uzytkownik1_IdUzytkownika" });
            DropIndex("dbo.Kontoes", new[] { "Uzytkownik_IdUzytkownika1" });
            DropIndex("dbo.Kontoes", new[] { "Uzytkownik_IdUzytkownika" });
            DropTable("dbo.Sesjas");
            DropTable("dbo.Uzytkowniks");
            DropTable("dbo.WydatkiStale");
            DropTable("dbo.WydatkiRaz");
            DropTable("dbo.Wplyws");
            DropTable("dbo.Kategorias");
            DropTable("dbo.Oszczednoscs");
            DropTable("dbo.Kontoes");
        }
    }
}
