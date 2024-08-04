namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guncel345 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Filmler", "puan", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Filmler", "puan", c => c.Int());
        }
    }
}
