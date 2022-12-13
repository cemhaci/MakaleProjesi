using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entity
{
    [Table("Notlar")]
    public class Note:EntitiesBase
    {
          [Required, StringLength(50)]
        public string Baslik { get; set; }
          [Required, StringLength(250)]
        public string Text { get; set; }
        public bool Taslak { get; set; }
        public int BegeniSayisi { get; set; }
        public int KategoriId { get; set; }  //not üzerinden kategori id ye kolay erişebilmke için yazdık

          public virtual Kategori kategori{ get; set; }
         public virtual Kullanici kullanici{ get; set; }
         public virtual List<Yorum> yorumlar{ get; set; }
         public virtual List<Like> like{ get; set; }  //notun birden fazla like ı vardı.bu notu kim likle mış onu öğrenmek için bağladık

        public Note()
        {
           yorumlar= new List<Yorum>();
            like= new List<Like>();
        }
    }

}
