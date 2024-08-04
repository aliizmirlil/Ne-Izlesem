namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30032023guncel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Uyeler", new[] { "kul_adi" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Uyeler", "kul_adi", unique: true);
        }
    }
}
