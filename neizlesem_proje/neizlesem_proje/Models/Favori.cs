using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neizlesem_proje.Models
{
    [Table("Favoriler")]
    public class Favori
    {
        [Key]
        public int favori_no { get; set; }
        [Required]
        [Index(IsUnique =false)]
        public int film_no { get; set; }
        [Required]
        public int uye_no { get; set; }

        public virtual Film film { get; set; }
        public virtual Uye uye { get; set; }
    }
}