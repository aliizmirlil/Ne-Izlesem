using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neizlesem_proje.Models
{
    [Table("Oyuncular")]
    public class Oyuncu
    {
        [Key]
        public int oyuncu_no { get; set; }

        [Required(ErrorMessage = "Oyuncu ad soyadı boş geçilemez.")]
        [MaxLength(50, ErrorMessage = "Oyuncu ad soyadı en fazla 50 karakter olabilir.")]
        public string oyuncu_adsoyad { get; set; }
        public string oyuncu_resim { get; set; }

        public List<Filmcast> filmcast { get; set; }

        public override string ToString()
        {
            return oyuncu_adsoyad;
        }
    }
}