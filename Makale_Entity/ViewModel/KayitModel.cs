using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entity.ViewModel
{
    public class KayitModel
    {
        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage ="Kullanını adı boş geçilemez"),StringLength(50)]
        public string kullaniciad { get; set; }
         [DisplayName("şifre"),Required(ErrorMessage ="Minimum 6 maksimum 20 karakterli olmalıdır"),StringLength(50),MaxLength(20),MinLength(6)]
        public  string sifre { get; set; }
        [DisplayName("şifre"),Required(ErrorMessage ="Minimum 6 maksimum 20 karakterli olmalıdır"),StringLength(50),MaxLength(20),MinLength(6),Compare(nameof(sifre),ErrorMessage ="{0} ile {1} uyuşmuyor")]
        public string sifre2 { get; set; }
        
        public string email { get; set; }


    }
}
