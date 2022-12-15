using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entity
{
    [Table("Kullanici")]
    public class Kullanici:EntitiesBase
    {
       [StringLength(50)]
        public string Ad { get; set; }
         [StringLength(50)]
        public string Soyad { get; set; }
         [Required, StringLength(50)]
        public string KullaniciAd { get; set; }
          [Required, StringLength(50)]
        public string Email { get; set; }
          [Required, StringLength(50)]
        public string Sifre { get; set; }
        public bool Aktif { get; set; }
        public bool Admin { get; set; }
          [Required]
        public Guid AktifGuid { get; set; } //aktivasyon kodu için yaptık ve zorunlu kıldıkki aktivayon kodu gönderilsin 
         public virtual List<Note> notlar{ get; set; } //bir kullanıcının birden fazla notu var
         public virtual List<Yorum> yorumlar{ get; set; }
         public virtual List<Like> likes{ get; set; }

        public Kullanici()
        {
           notlar= new List<Note>();
           yorumlar= new List<Yorum>();
            likes=new List<Like>();
        }
    }
}
