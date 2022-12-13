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
         [Required, StringLength(0)]
        public string Balik { get; set; }  //bir kategorinin birden fazla notu olucak.her notun da kategoridi plucak
          [Required, StringLength(150)]
        public string Aciklama { get; set; }
      public virtual List<Note> notes { get; set; }
    }
}
