namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guncel3456 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uyeler", "sifre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uyeler", "sifre");
        }
    }
}
