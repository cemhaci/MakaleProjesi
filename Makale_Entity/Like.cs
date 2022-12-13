using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entity
{
    [Table("Like")]
    public class Like
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public virtual Kullanici kullanici { get;set;}   //kim hangi notu beğeni onun için bağladık
         public virtual Note not{ get; set; }

    }
}
