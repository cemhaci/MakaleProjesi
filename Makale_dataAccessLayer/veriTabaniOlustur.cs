using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_dataAccessLayer
{
    public class veriTabaniOlustur:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Kullanici admin =new Kullanici()
            {
                Ad="cem",
                Soyad="haci",
                Email="cemhaci@gmail.com",
                Sifre="12345",
                Aktif=true,
                Admin=true,
                AktifGuid=Guid.NewGuid(),
                KullaniciAd="cemhaci",
                KayitTarihi=DateTime.Now,
                DegistirmeTarihi=DateTime.Now.AddMinutes(5),
                DegistirenKullanici="cemhaci"
            };
            context.kullanicilar.Add(admin);
            for(int i = 1; i < 6; i++)
            {
                Kullanici users=new Kullanici()
                {
                    Ad=FakeData.NameData.GetFirstName(),
                    Soyad=FakeData.NameData.GetSurname(),
                    Email=FakeData.NetworkData.GetEmail(),
                    Sifre="12345",
                    KullaniciAd="user"+i,
                    Aktif=true,
                    Admin=false,
                    AktifGuid=Guid.NewGuid(),
                    KayitTarihi=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                    DegistirmeTarihi=DateTime.Now,
                    DegistirenKullanici="user"+i
                     
                };
                context.kullanicilar.Add(users);
              context.SaveChanges();
            }
            context.SaveChanges();

            List<Kullanici> kullanicilistesi=context.kullanicilar.ToList();
            //fake kategori ekle

            for(int i = 1; i < 11; i++)
            {
                Kategori kat=new Kategori()
                {
                    Baslik=FakeData.PlaceData.GetStreetName(),
                    Aciklama=FakeData.PlaceData.GetAddress(),
                    DegistirmeTarihi=DateTime.Now,
                    KayitTarihi=DateTime.Now,
                    DegistirenKullanici ="cemhaci"
                };
                context.kategoriler.Add(kat);

                //for not ekleme
                for(int j = 1; j < 6; j++)
                {
                    Note not=new Note()
                    {
                        Baslik=FakeData.TextData.GetAlphabetical(20),
                        Text=FakeData.TextData.GetSentences(3),
                        Taslak=false,    //taslak olmadığı için
                        BegeniSayisi=FakeData.NumberData.GetNumber(1,9),  //her notun beğeni sayısı farkklı olsun diye aralık verdik
                        kullanici=kullanicilistesi[FakeData.NumberData.GetNumber(1,6)],
                        KayitTarihi=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                        DegistirmeTarihi=DateTime.Now,
                        DegistirenKullanici=kullanicilistesi[FakeData.NumberData.GetNumber(1,6)].KullaniciAd
                    };
                    kat.notes.Add(not); //kategorilerin içinde notları ekledik
                    
                    //fake yorum ekle

                    for(int k=1;k<4;k++)  //her bir notun 3 yorumu olucak
                    {
                        Yorum y=new Yorum()
                        {
                          Text=FakeData.TextData.GetSentences(3),                      
                          kullanici=kullanicilistesi[FakeData.NumberData.GetNumber(1,6)],
                          KayitTarihi=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                          DegistirmeTarihi=DateTime.Now,
                          DegistirenKullanici=kullanicilistesi[FakeData.NumberData.GetNumber(1,6)].KullaniciAd  //  DegistirenKullanici bize bir string dönüyor kullanıcı listesi ise bir nesne dönüyor bize bu nesnenin kullanıcı adı lazım o uüzden kullanıcı adını ekledik
                        };
                        not.yorumlar.Add(y);  //notların içine yorumları ekledik
                    }
                       for(int t = 0; t < not.BegeniSayisi; t++)
                    {
                        Like l=new Like()
                        {
                            kullanici=kullanicilistesi[FakeData.NumberData.GetNumber(1,6)]
                        };
                        not.like.Add(l);
                    }                 
                }
            }
            context.SaveChanges();
        }
    }
}
