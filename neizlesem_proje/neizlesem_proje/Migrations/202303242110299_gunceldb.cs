namespace neizlesem_proje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gunceldb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Filmler", "oyuncu_no", "dbo.Oyuncular");
            DropForeignKey("dbo.Filmler", "tur_no", "dbo.Turler");
            DropForeignKey("dbo.Filmler", "yonetmen_no", "dbo.Yonetmenler");
            DropIndex("dbo.Filmler", new[] { "tur_no" });
            DropIndex("dbo.Filmler", new[] { "yonetmen_no" });
            DropIndex("dbo.Filmler", new[] { "oyuncu_no" });
            CreateTable(
                "dbo.Film_cast",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        oyuncu_no = c.Int(nullable: false),
                        film_no = c.Int(nullable: false),
                        rol = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Oyuncular", t => t.oyuncu_no, cascadeDelete: true)
                .ForeignKey("dbo.Filmler", t => t.film_no, cascadeDelete: true)
                .Index(t => t.oyuncu_no)
                .Index(t => t.film_no);
            
            CreateTable(
                "dbo.Film_turu",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        tur_no = c.Int(nullable: false),
                        film_no = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Turler", t => t.tur_no, cascadeDelete: true)
                .ForeignKey("dbo.Filmler", t => t.film_no, cascadeDelete: true)
                .Index(t => t.tur_no)
                .Index(t => t.film_no);
            
            CreateTable(
                "dbo.Film_yonetmen",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        yonetmen_no = c.Int(nullable: false),
                        film_no = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Yonetmenler", t => t.yonetmen_no, cascadeDelete: true)
                .ForeignKey("dbo.Filmler", t => t.film_no, cascadeDelete: true)
                .Index(t => t.yonetmen_no)
                .Index(t => t.film_no);
            
            AlterColumn("dbo.Filmler", "film_ozeti", c => c.String(nullable: false));
            DropColumn("dbo.Filmler", "tur_no");
            DropColumn("dbo.Filmler", "yonetmen_no");
            DropColumn("dbo.Filmler", "oyuncu_no");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Filmler", "oyuncu_no", c => c.Int(nullable: false));
            AddColumn("dbo.Filmler", "yonetmen_no", c => c.Int(nullable: false));
            AddColumn("dbo.Filmler", "tur_no", c => c.Int(nullable: false));
            DropForeignKey("dbo.Film_yonetmen", "film_no", "dbo.Filmler");
            DropForeignKey("dbo.Film_yonetmen", "yonetmen_no", "dbo.Yonetmenler");
            DropForeignKey("dbo.Film_turu", "film_no", "dbo.Filmler");
            DropForeignKey("dbo.Film_turu", "tur_no", "dbo.Turler");
            DropForeignKey("dbo.Film_cast", "film_no", "dbo.Filmler");
            DropForeignKey("dbo.Film_cast", "oyuncu_no", "dbo.Oyuncular");
            DropIndex("dbo.Film_yonetmen", new[] { "film_no" });
            DropIndex("dbo.Film_yonetmen", new[] { "yonetmen_no" });
            DropIndex("dbo.Film_turu", new[] { "film_no" });
            DropIndex("dbo.Film_turu", new[] { "tur_no" });
            DropIndex("dbo.Film_cast", new[] { "film_no" });
            DropIndex("dbo.Film_cast", new[] { "oyuncu_no" });
            AlterColumn("dbo.Filmler", "film_ozeti", c => c.String());
            DropTable("dbo.Film_yonetmen");
            DropTable("dbo.Film_turu");
            DropTable("dbo.Film_cast");
            CreateIndex("dbo.Filmler", "oyuncu_no");
            CreateIndex("dbo.Filmler", "yonetmen_no");
            CreateIndex("dbo.Filmler", "tur_no");
            AddForeignKey("dbo.Filmler", "yonetmen_no", "dbo.Yonetmenler", "yonetmen_no", cascadeDelete: true);
            AddForeignKey("dbo.Filmler", "tur_no", "dbo.Turler", "tur_no", cascadeDelete: true);
            AddForeignKey("dbo.Filmler", "oyuncu_no", "dbo.Oyuncular", "oyuncu_no", cascadeDelete: true);
        }
    }
}
