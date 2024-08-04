using System;
using System.Data.Entity;
using System.Linq;

namespace neizlesem_proje.Models
{
    public class neizlesem : DbContext
    {
        // Your context has been configured to use a 'neizlesem' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'neizlesem_proje.Models.neizlesem' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'neizlesem' 
        // connection string in the application configuration file.
        public neizlesem()
            : base("name=neizlesem")
        {
        }


        public virtual DbSet<Uye> uyeler { get; set; }
        public virtual DbSet<Film> filmler { get; set; }
        public virtual DbSet<Favori> favoriler  { get; set; }
        public virtual DbSet<Tur> turler { get; set; }
        public virtual DbSet<Oyuncu> oyuncular { get; set; }
        public virtual DbSet<Yonetmen> yonetmenler { get; set; }
        public virtual DbSet<Izlenecek> izlenecekler { get; set; }
        public virtual DbSet<Filmcast> filmcast { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Uye>()
                .HasMany(x => x.favori)
                .WithRequired(x => x.uye)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Uye>()
                .HasMany(x => x.izlenecek)
                .WithRequired(x => x.uye)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Yonetmen>()
                .HasMany(x => x.film)
                .WithRequired(x => x.yonetmeni)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Oyuncu>()
                .HasMany(x => x.filmcast)
                .WithRequired(x => x.oyuncu)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Film>()
                .HasMany(x => x.favoriler)
                .WithRequired(x => x.film)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Film>()
                .HasMany(x => x.izlenecekler)
                .WithRequired(x => x.film)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Tur>()
                .HasMany(x => x.film)
                .WithRequired(x=>x.tur)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Film>()
                .HasMany(x => x.filmcasti)
                .WithRequired(x => x.film)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Filmcast>().HasKey(am => new { am.oyuncu_no, am.film_no });

            modelBuilder.Entity<Film>().Property(x => x.film_cikis_tarih).HasColumnType("smalldatetime");
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}