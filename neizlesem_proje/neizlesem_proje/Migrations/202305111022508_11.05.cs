namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1105 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Film_yonetmen", "yonetmen_no", "dbo.Yonetmenler");
            DropForeignKey("dbo.Film_yonetmen", "film_no", "dbo.Filmler");
            DropIndex("dbo.Film_yonetmen", new[] { "yonetmen_no" });
            DropIndex("dbo.Film_yonetmen", new[] { "film_no" });
            AddColumn("dbo.Filmler", "yonetmen_no", c => c.Int(nullable: true));
            CreateIndex("dbo.Filmler", "yonetmen_no");
            AddForeignKey("dbo.Filmler", "yonetmen_no", "dbo.Yonetmenler", "yonetmen_no", cascadeDelete: true);
            DropTable("dbo.Film_yonetmen");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Film_yonetmen",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        yonetmen_no = c.Int(nullable: false),
                        film_no = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.Filmler", "yonetmen_no", "dbo.Yonetmenler");
            DropIndex("dbo.Filmler", new[] { "yonetmen_no" });
            DropColumn("dbo.Filmler", "yonetmen_no");
            CreateIndex("dbo.Film_yonetmen", "film_no");
            CreateIndex("dbo.Film_yonetmen", "yonetmen_no");
            AddForeignKey("dbo.Film_yonetmen", "film_no", "dbo.Filmler", "film_no", cascadeDelete: true);
            AddForeignKey("dbo.Film_yonetmen", "yonetmen_no", "dbo.Yonetmenler", "yonetmen_no", cascadeDelete: true);
        }
    }
}
