using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entity
{
    public class EntitiesBase
    {[Required]
         public DateTime KayitTarihi { get; set; }   //yötetici hesabı kaç içerde nasıl bir değişiklik yaptı hangi tarihte yaptı onların gözükmasiniistediğimizden bu class ı kullaniciyr kalıttık
        [Required]
        public DateTime DegistirmeTarihi { get; set; }
        [Required,StringLength(20)]
        public string DegistirenKullanici { get; set; }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int ID { get; set; }

    }
}
