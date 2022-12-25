using Makale.Common;
using Makale_dataAccessLayer;
using Makale_Entity;
using Makale_Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class KullaniciYonet
    {
        repository<Kullanici> rep_kul= new repository<Kullanici>();

        public List<Kullanici> listele()
		{
            return rep_kul.liste();
		}

        public BusinessLayer_Sonuc<Kullanici> kaydet(KayitModel model) //BusinessLayer_Sonuc<Kullanici> bu tür de döndürmesini istiyoruz çünkü model e hata varsa onları yoksa T tipine gönderddiğimiz bilgileri döndürsün
        {
            BusinessLayer_Sonuc<Kullanici> sonuc=new BusinessLayer_Sonuc<Kullanici>();
            Kullanici k=new Kullanici();
            k.KullaniciAd = model.kullaniciad;
            k.Email = model.email;

            sonuc = kullanicikontrol(k);

            if (sonuc.hatalar.Count > 0)
            {
                sonuc.nesne = k;
                return sonuc;
            }
            else
            {
               int kaydet= rep_kul.Insert(new Kullanici()
                {
                    Email=model.email,
                    KullaniciAd=model.kullaniciad,
                    Sifre=model.sifre,
                    AktifGuid=Guid.NewGuid(),
                    //DegistirmeTarihi=DateTime.Now,
                    //DegistirenKullanici="system",
                    Admin=false,
                    Aktif=false,
                   
                });
                if (kaydet > 0)
                {
                    sonuc.nesne=rep_kul.Find(x=>x.Email==model.email&&x.KullaniciAd==model.kullaniciad);

                    string siteUrl=ConfigHelper.Get<string>("SiteRootUri");
                    string activateUrl =$"{ siteUrl}/Home/UserActivate/{sonuc.nesne.AktifGuid}";
                    string body=$"Merhaba{sonuc.nesne.KullaniciAd} <br/> Hesabınızı aktifleştirmek için <a href='{activateUrl}'>tıklayınız</a> ";
                    MailHelper.SendMail(body,sonuc.nesne.Email,"hesap aktifleştirme");

                }
               
            }
             return sonuc;
        }
        private BusinessLayer_Sonuc<Kullanici> kullanicikontrol(Kullanici kullanici)
        {
            BusinessLayer_Sonuc<Kullanici> sonuc = new BusinessLayer_Sonuc<Kullanici>();
            Kullanici k1 = rep_kul.Find(x => x.KullaniciAd == kullanici.KullaniciAd);

            Kullanici k2=rep_kul.Find(x=>x.Email == kullanici.Email);

            if(k1!=null &&k1.ID != kullanici.ID)
            {
                sonuc.hatalar.Add("kullanıcı adı sistemde kayıtlı");

            }

            if(k2!=null &&k2.ID != kullanici.ID)
            {
                sonuc.hatalar.Add("email sistemde kayıtlı");
            }
            return sonuc;
        }
        public BusinessLayer_Sonuc<Kullanici> loginKontrol(LoginModel model)
        {
            BusinessLayer_Sonuc<Kullanici> sonuc = new BusinessLayer_Sonuc<Kullanici>();
            sonuc.nesne =rep_kul.Find(x=>x.KullaniciAd==model.KullaniciAd&&x.Sifre==model.Sifre);

            if (sonuc.nesne!= null)
            {
                if (!sonuc.nesne.Aktif)
                {
                    sonuc.hatalar.Add("kullanici aktif değildir.aktivasyon için e postaa adresini kontrol ediniz");
                }
            }
            else
            {
                sonuc.hatalar.Add("kullanıcı adı ve şifre eşleşmiyor");
            }
            return sonuc;
        }

        public BusinessLayer_Sonuc<Kullanici> ActivateUser(Guid id)
        {
            BusinessLayer_Sonuc<Kullanici> sonuc = new BusinessLayer_Sonuc<Kullanici>();

            sonuc.nesne=rep_kul.Find(x=>x.AktifGuid==id);
            if(sonuc.nesne != null)
            {
                if (sonuc.nesne.Aktif)
                {
                    sonuc.hatalar.Add("kullanıcı zaten aktif");
                    return sonuc;
                }
                sonuc.nesne.Aktif=true;
                rep_kul.Update(sonuc.nesne);            
            }

            else
            {
                sonuc.hatalar.Add("aktifleştirrilecek kullanıcı bulunamadı");
            }
            return sonuc;
        }
        public BusinessLayer_Sonuc<Kullanici> kullaniciUpdate(Kullanici kul)
        {


            BusinessLayer_Sonuc<Kullanici> sonuc=new BusinessLayer_Sonuc<Kullanici>();
            //Kullanici k1 = rep_kul.Find(x => x.KullaniciAd == kul.KullaniciAd);
            //Kullanici k2=rep_kul.Find(x=>x.Email==kul.Email);

            //if(k1!= null && k1.ID != kul.ID)  //database de böyle bir kullanıcı varsa
            //{
            //     //if (k.KullaniciAd == kul.KullaniciAd)

            //        sonuc.hatalar.Add("kullanıcı adı sistemde kayıtlı");  //sonucun hatalarına bunu ekle
            //}
            //if (k2!=null&&k2.ID!=kul.ID)
            //    {
            //        sonuc.hatalar.Add("email sistemde kayıtlı");
            //    }

            sonuc = kullanicikontrol(kul);
            if (sonuc.hatalar.Count > 0)
            {
                sonuc.nesne = kul;
                return sonuc;
            }
            /*sonuc.nesne=kul;*/ //hoome controller daki profildeğiştir nesne döndürüyor bunları nesneye atmazsak profil degistirde nesne boş olucaktır.
                 return sonuc;
           
            sonuc.nesne=rep_kul.Find(x=>x.ID==kul.ID);
            sonuc.nesne.Ad=kul.Ad;
            sonuc.nesne.Email = kul.Email;
            sonuc.nesne.Soyad=kul.Soyad;
            sonuc.nesne.KullaniciAd=kul.KullaniciAd;
            sonuc.nesne.Sifre=kul.Sifre;
            if(!string.IsNullOrEmpty(kul.profilresim))
            sonuc.nesne.profilresim=kul.profilresim;

           int updateSonuc= rep_kul.Update(sonuc.nesne);
            if (updateSonuc < 1)
            {
                sonuc.hatalar.Add("profil güncellenemedi");
            }
            return sonuc;
        }

		public BusinessLayer_Sonuc<Kullanici> kullaniciSil(int ıD)
		{
			BusinessLayer_Sonuc<Kullanici> sonuc=new BusinessLayer_Sonuc<Kullanici>();
            sonuc.nesne=rep_kul.Find(x=>x.ID==ıD);
			if (sonuc.nesne != null)
			{
               int silSonuc= rep_kul.Delete(sonuc.nesne);
                if(silSonuc < 1)
				{
                    sonuc.hatalar.Add("kullanıcı silinemedi");
				}
			}
			else
			{
                sonuc.hatalar.Add("kullanıcı bulunamadı");
			}
            return sonuc;
		}

        public Kullanici KullaniciBul(int? id)
        {
           return rep_kul.Find(x=>x.ID==id);
        }
        //
        public BusinessLayer_Sonuc<Kullanici> KullaniciKaydet(Kullanici kullanici)
            
        {//yönetici bir kullanıcı kaydetmek isterse bu metotdu çalıştırıyoruz
            BusinessLayer_Sonuc<Kullanici> sonuc = new BusinessLayer_Sonuc<Kullanici>();
            sonuc = kullanicikontrol(kullanici);

            if (sonuc.hatalar.Count > 0)
            {
                sonuc.nesne = kullanici;
                return sonuc;
            }
            else
            {
                int kayit = rep_kul.Insert(kullanici);
                if(kayit < 1)
                {
                    sonuc.hatalar.Add("kategori kaydedilemedi");
                }
            }
            return sonuc;
        }
    }
}
