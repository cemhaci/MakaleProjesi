using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entity.ViewModel
{
    public class LoginModel
    {
        [DisplayName("Kullanıcı Adı"),Required(ErrorMessage ="{0} alanı boş geçilemez"), StringLength(50)]
        public string KullaniciAd { get; set; }

          [DisplayName("Şifre"),Required(ErrorMessage ="{0} alanı boş geçilemez"), StringLength(50)]
        public string Sifre { get; set; }


    }
}
