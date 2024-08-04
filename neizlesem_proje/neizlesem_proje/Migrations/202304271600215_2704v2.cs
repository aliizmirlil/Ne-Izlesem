namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2704v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Filmler", "tur_no", c => c.Int(nullable:true));
            CreateIndex("dbo.Filmler", "tur_no");
            AddForeignKey("dbo.Filmler", "tur_no", "dbo.Turler", "tur_no", cascadeDelete:false,"FK_Turler_Filmler");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Filmler", "tur_no", "dbo.Turler");
            DropIndex("dbo.Filmler", new[] { "tur_no" });
            DropColumn("dbo.Filmler", "tur_no");
        }
    }
}
