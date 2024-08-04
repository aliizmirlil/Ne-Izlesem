using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neizlesem_proje.Models
{
    public class Filmcast
    {
        public int film_no { get; set; }
        [ForeignKey("film_no")]
        public Film film { get; set; }
        public int oyuncu_no { get; set; }
        [ForeignKey("oyuncu_no")]
        public Oyuncu oyuncu { get; set; }

        public string rol { get; set; }

        public override string ToString()
        {
            return oyuncu.oyuncu_adsoyad + (rol);
        }

    }
}