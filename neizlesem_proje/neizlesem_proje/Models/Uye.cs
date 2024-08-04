using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neizlesem_proje.Models
{
    [Table("Uyeler")]
    public class Uye
    {
        public Uye()
        {
            this.favori = new HashSet<Favori>();
            this.izlenecek = new HashSet<Izlenecek>();
        }

        [Key]
        public int uye_no { get; set; }
        [Required(ErrorMessage = "Üye ad soyad boş geçilemez.")]
        [StringLength(40, ErrorMessage = "Ad soyad en fazla 40 karakter olabilir.")]
        public string ad_soyad { get; set; }

        [EmailAddress,Required(ErrorMessage = "Üye e-mail boş geçilemez.")]
        public string uye_mail { get; set; }

        
        [Required,StringLength(20, ErrorMessage = "Kullanıcı adı en fazla 20 karakter olabilir.")]
        public string kul_adi { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez.")]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakter olmalı.")]
        public string sifre { get; set; }

        public virtual ICollection<Favori> favori { get; set; }
        public virtual ICollection<Izlenecek> izlenecek { get; set; }

    }
}