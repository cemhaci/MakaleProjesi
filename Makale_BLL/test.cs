using Makale_dataAccessLayer;
using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class test
    {
         public test()
        {
            /* DatabaseContext db =new DatabaseContext();*/ //burada örneklememizi yaptığımızda seed metodumuzu tetikklemek için set initilazer ımızla tetiklemeliyiz o yüzden database örneklendiğinde seed tetiklenicek
             //db.Database.CreateIfNotExists();   seed metodu oluşturduğumuz için artık veri tabanı oluştur metodumuzda. oyüzden buna gerek yok veri tabanımızı veri tabanı oluştur metodunda oluşturcak
             //db.kategoriler.ToList();
            
            // oluşturduğumuz repositroy classıyla çağırıcaz bunu artık.listelemeyi vs

            repository<Kategori> rep_kat=new repository<Kategori>();
            List<Kategori> kategoriler=rep_kat.liste();  //kategorileri çağrdığımızdan dayı liste metodunda ki t tipimiz direkt kategorileri bulup listeledii
             List<Kategori> kategoriler2=rep_kat.liste(x=>x.ID<5);  //listenin içine yazdığımız koşulumuz bu yazdığımız repository ye gidiyor ve metodumuz iççinde ki kosula döndürüyoruz o da bize id si 5 den kğüçük olanları listeliyor
        }
         repository<Kullanici> rep_kul=new repository<Kullanici>();
        public void InsertTest()
        {
           
            rep_kul.Insert(new Kullanici(){ 
                Ad="ali",Soyad="yücel",Email="aliyucel@gmail.com",Aktif=true,AktifGuid=Guid.NewGuid(), Admin=true,KullaniciAd="aliyüc",Sifre="1234",KayitTarihi=DateTime.Now,DegistirmeTarihi=DateTime.Now.AddDays(5),DegistirenKullanici="admin"    //AddDays(5) 5 dk sonra eklensin demek
                });
        }
        public void UpdateTest()
        {
            Kullanici kullanici=rep_kul.Find(x=>x.KullaniciAd=="aliyüc");
            if (kullanici != null)
            {
                kullanici.KullaniciAd="aliyücel34";
                rep_kul.Update();
            }
        }
        public void DeleteTest()
        {
            Kullanici kullanici=rep_kul.Find(x=>x.KullaniciAd=="aliyüc");
            if(kullanici != null)
            {
                rep_kul.Delete(kullanici);
            }

        }
        public void YorumEkle()
        {
            //1 id li kullanıcı 1 id li nota yorum yapmasını sağlıycaz
            repository<Yorum> rep_yorum=new repository<Yorum>();
            repository<Note> rep_note = new repository<Note>();
            Kullanici kullanici= rep_kul.Find(x=>x.ID==1);
            Note not= rep_note.Find(x=>x.ID==1);
            rep_yorum.Insert(new Yorum(){ 
                kullanici=kullanici,
                not=not,
                Text="deneme deneme",
                KayitTarihi=DateTime.Now,
                DegistirmeTarihi=DateTime.Now.AddHours(1),
                DegistirenKullanici=kullanici.KullaniciAd
                });
        }
    }
}
