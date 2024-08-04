namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3003guncel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Uyeler", "uye_mail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Uyeler", "uye_mail", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
