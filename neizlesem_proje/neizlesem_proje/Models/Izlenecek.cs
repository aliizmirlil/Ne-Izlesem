using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neizlesem_proje.Models
{
    [Table("Izlenecekler")]
    public class Izlenecek
    {
        [Key]
        public int izlenecek_no { get; set; }
        [Required]
        public int film_no { get; set; }
        [Required]
        public int uye_no { get; set; }

        public Uye uye { get; set; }
        public Film film { get; set; }
    }
}