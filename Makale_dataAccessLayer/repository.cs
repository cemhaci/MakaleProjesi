using Makale_Common;
using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Makale_dataAccessLayer
{
 
    public class repository<T>:singleton where T:class//tüm entityler içiçn bu t tipimizi kullanıcaz.where diyerek T tipimizin class olduğunu belirttik. belirtmezssek set de hata vericektir.singleton ı buraya kalıttık çünkğ databasecontext in tekrar tekrar örneklenmesini istemiyıruz 
    {
           DbSet<T> _objectset;
           public repository()
        {
            _objectset=db.Set<T>();  
        }
        
        public List<T> liste()
        {

            return _objectset.ToList();  //burada gelenler yazdğımız tüm dbsetleri ifade ediyor ve bu dbsetleri alıp bir liste gönderiyor
        }
        public List<T> liste(Expression<Func<T,bool>>kosul)  //koşullamamızı sağladı listelerken koşullu olarak listelememizi sağlayacaktır. koşulumuzu da test classına yazıcaz
        {
            //return db.Set<T>().Where(kosul).ToList();
            return _objectset.Where(kosul).ToList();  
        }

          public IQueryable<T> listeQ()  //notları tarihe göre sırlamak istiyoruz oyüzden bunu ekledik
        {
        
            return _objectset.AsQueryable();
        }

        public T Find(Expression<Func<T, bool>> kosul)  //liste koşulumuz giibi testede örneğin kategorilerde araba diye bir kategori var mı onu bulmaya yarıyor. testte bulmak istediğimiz şeyi yazıyoruz onu buradaki koşula getirip firstor default ile onu buluyor
        {
            return _objectset.FirstOrDefault(kosul);
        }

        public int Insert(T nesne) //int dememizin sebebi sql de 1 row effected diye yazmasından dolayı 1 başarılı ekleme tarzı döndürdüğünden dolayı. biz bunu if ile eklenip eklenmediğini 1 den büyük küçük şeklinde kontrol ettirebiliriz
        {
            _objectset.Add(nesne);  //kullanıcı veya kategori hangi db seti çağırırsak onu ekliycek
            EntitiesBase obj= nesne as EntitiesBase;
            DateTime time=DateTime.Now;
            if(nesne is EntitiesBase)      //bunlarıher insert ettiğimizde eklemek zorundayız çünkü requried bilgi bunlar ve her intert bilgi eklediğimizden sürekli yazmamıza gerek kalmayacak 
            {
                obj.KayitTarihi=time;
                obj.DegistirmeTarihi=time;
                obj.DegistirenKullanici=uygulama.kullaniciAd;
            }
            return db.SaveChanges();
        }
        public int Delete(T nesne)
        {
            _objectset.Remove(nesne);
            return db.SaveChanges();
        }
           public int Update(T nesne)
        {
             EntitiesBase obj= nesne as EntitiesBase;
            //bu metoda gelicek nesne zaten değişceğinden sadece kaydetmemiz yeticektir
            if(nesne is EntitiesBase)
            {
                obj.DegistirmeTarihi=DateTime.Now;
                obj.DegistirenKullanici=uygulama.kullaniciAd;
            }
            return db.SaveChanges();
        }
    }
}
