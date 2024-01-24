namespace ProjektSQL
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WplywStalies", "IdKategorii");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WplywStalies", "IdKategorii", c => c.Int(nullable: false));
        }
    }
}
