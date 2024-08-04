namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1105v7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Filmcasts", "rol", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Filmcasts", "rol");
        }
    }
}
