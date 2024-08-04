using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace neizlesem_proje.Models
{
    [Table("Yonetmenler")]
    public class Yonetmen
    {
        [Key]
        public int yonetmen_no { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="Yönetmen ad soyadı en fazla 50 karakter olabilir.")]
        [Index(IsUnique =true)]
        public string yonetmen_adsoyad { get; set; }

        public virtual ICollection<Film> film { get; set; }
    }
}