using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entity
{
    [Table("Yorum")]
    public class Yorum:EntitiesBase
    {
          [Required, StringLength(250)]
        public string Text { get; set; }
        public virtual Kullanici kullanici { get; set; }
         public virtual Note not{ get; set; }
    }
}
