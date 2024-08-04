namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3103guncel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Uyeler", new[] { "uye_mail" });
            
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Uyeler", "uye_mail", unique: true);
        }
    }
}
