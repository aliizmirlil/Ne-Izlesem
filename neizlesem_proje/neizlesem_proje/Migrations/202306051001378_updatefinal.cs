namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefinal : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Favoriler", new[] { "film_no" });
            CreateIndex("dbo.Favoriler", "film_no", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Favoriler", new[] { "film_no" });
            CreateIndex("dbo.Favoriler", "film_no");
        }
    }
}
