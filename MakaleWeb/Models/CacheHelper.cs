using Makale_BLL;
using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MakaleWeb.Models
{
    public class CacheHelper
    {
        public static  List<Kategori> kategoriler()
        {// her sayfa açıldığında database den kategorileri getiriyor. sürekli veri tabanına girmesini önlemek için cache yapıyoruz. ilk çalıştırğında veri tabınnından kategorileri getiriyor ve onu cache atıyor böylelikle her kategorileri sıraladığımızda 20 dk boyunca cachen alıyor bu bilgileri. bu olayın aynısını session da da yapıyor<
            var sonuc =WebCache.Get("kategori-cache");  //cache de bilgi varsa onu get et yani çalıştır diyoruz. 20 dk süre biterse cache in içi boşalıcaktır ve if in içine girecektir.
            if (sonuc == null)  //eğer cache deki bilgi null ise
            {
                KategoriYonet ky = new KategoriYonet();   
                sonuc=ky.listele();    
               WebCache.Set("kategori-cahe",sonuc,20,true);  //cache cilgimize ky.listeleyi set et, 20 dk sonra cache i sil, her çalıştırıldığında cache çalışsın 20 dk lık ömrü olsun

            }
            return sonuc;
        }

    }
}