namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guncel123 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Filmler", "film_cikis_tarih", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Filmler", "film_cikis_tarih", c => c.DateTime(nullable: false));
        }
    }
}
