using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entity
{
    [Table("Kategori")]
    public class Kategori:EntitiesBase
    {
         [Required, StringLength(50)]
        public string Baslik { get; set; }  //bir kategorinin birden fazla notu olucak.her notun da kategoridi plucak
          [Required, StringLength(300)]
        public string Aciklama { get; set; }
      public virtual List<Note> notes { get; set; }

        public Kategori()
        {
            notes=new List<Note>(); //bunları örneklememiz lazım çünkü veri tabanı oluştur classımızda kat.notes.Add(not) diye ekleme yaparken içi null atıyor. o yüzden kategorilerde ki notlara bir şey atabilmemiz için bunu örneklememiz gerekiyor
        }
    }
}
