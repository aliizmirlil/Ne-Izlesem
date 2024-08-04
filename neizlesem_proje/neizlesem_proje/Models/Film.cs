using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace neizlesem_proje.Models
{
    [Table("Filmler")]
    public class Film
    {
        [Key]
        public int film_no { get; set; }

        [Required(ErrorMessage = "Film adı boş geçilemez.")]
        public string film_adi { get; set; }

        [Required(ErrorMessage = "Film özeti boş geçilemez.")]
        public string film_ozeti { get; set; }
        public int tur_no { get; set; }
        public string film_afis { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime film_cikis_tarih { get; set; }

        [Puan_dogrulama(ErrorMessage = "Puan 0 ile 10 arasında olmalıdır.")]
        [Range(1,10)]
        public double? puan { get; set; }
        public int yonetmen_no { get; set; }

        public string yildizlar { get; set; }

       /* public List<Film_cast> film_cast { get; set; } */
        public virtual ICollection<Favori> favoriler { get; set; }
        public virtual ICollection<Izlenecek> izlenecekler { get; set; }
        public virtual Tur tur { get; set; }
        public virtual Yonetmen yonetmeni { get; set; }
        public List<Filmcast> filmcasti { get; set; }
       // public virtual Film_turu filmin_turu { get; set; }

    }
}