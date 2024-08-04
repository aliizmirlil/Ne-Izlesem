using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neizlesem_proje.Models
{
    [Table("Turler")]
    public class Tur
    {
        [Key]
        public int tur_no { get; set; }

        [Required(ErrorMessage ="Tür adı boş geçilemez.")]
        public string tur_adi { get; set; }

        public virtual ICollection<Film> film { get; set; }
    }
}