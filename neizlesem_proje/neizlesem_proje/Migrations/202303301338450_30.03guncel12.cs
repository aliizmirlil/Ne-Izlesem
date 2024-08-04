namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3003guncel12 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Uyeler", new[] { "kul_adi" });
            AlterColumn("dbo.Uyeler", "kul_adi", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Uyeler", "kul_adi", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Uyeler", new[] { "kul_adi" });
            AlterColumn("dbo.Uyeler", "kul_adi", c => c.String(maxLength: 20));
            CreateIndex("dbo.Uyeler", "kul_adi", unique: true);
        }
    }
}
