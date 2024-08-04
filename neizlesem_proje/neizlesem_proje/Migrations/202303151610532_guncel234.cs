namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guncel234 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Filmler", "film_ozeti", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Filmler", "film_ozeti", c => c.String(nullable: false));
        }
    }
}
