using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace neizlesem_proje.Models
{
    public class Puan_dogrulama : ValidationAttribute
    {
        public override bool IsValid(object puan)
        {
            double deger = Convert.ToDouble(puan);
            if (deger >= 0 || deger <= 10)
            {
                return true;
            }
            else return false;
        }
    }
}