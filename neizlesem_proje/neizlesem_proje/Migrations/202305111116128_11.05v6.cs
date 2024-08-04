namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1105v6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Filmler", "film_casti_id", "dbo.Film_cast");
            DropForeignKey("dbo.Film_cast", "oyuncu_no", "dbo.Oyuncular");
            DropIndex("dbo.Filmler", new[] { "film_casti_id" });
            DropIndex("dbo.Film_cast", new[] { "oyuncu_no" });
            CreateTable(
                "dbo.Filmcasts",
                c => new
                    {
                        oyuncu_no = c.Int(nullable: false),
                        film_no = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.oyuncu_no, t.film_no })
                .ForeignKey("dbo.Oyuncular", t => t.oyuncu_no, cascadeDelete: true)
                .ForeignKey("dbo.Filmler", t => t.film_no, cascadeDelete: true)
                .Index(t => t.oyuncu_no)
                .Index(t => t.film_no);
            
            DropColumn("dbo.Filmler", "cast_no");
            DropColumn("dbo.Filmler", "film_casti_id");
            DropTable("dbo.Film_cast");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Film_cast",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        oyuncu_no = c.Int(nullable: false),
                        film_no = c.Int(nullable: false),
                        rol = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Filmler", "film_casti_id", c => c.Int());
            AddColumn("dbo.Filmler", "cast_no", c => c.Int(nullable: false));
            DropForeignKey("dbo.Filmcasts", "film_no", "dbo.Filmler");
            DropForeignKey("dbo.Filmcasts", "oyuncu_no", "dbo.Oyuncular");
            DropIndex("dbo.Filmcasts", new[] { "film_no" });
            DropIndex("dbo.Filmcasts", new[] { "oyuncu_no" });
            DropTable("dbo.Filmcasts");
            CreateIndex("dbo.Film_cast", "oyuncu_no");
            CreateIndex("dbo.Filmler", "film_casti_id");
            AddForeignKey("dbo.Film_cast", "oyuncu_no", "dbo.Oyuncular", "oyuncu_no", cascadeDelete: true);
            AddForeignKey("dbo.Filmler", "film_casti_id", "dbo.Film_cast", "id");
        }
    }
}
