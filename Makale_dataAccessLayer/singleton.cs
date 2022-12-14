using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_dataAccessLayer
{
    public class singleton
    {
        public static DatabaseContext db;   //çoğu yerde databasecontexti new le ile örneklediğimizdden hata verdi o yüzden singleton pattern kullanıp bir kez örneklenmesini sağlıycaz. test classımızda her repostory yi çağırdığımızda tekrar tekrar databasecontexti örnekliyor. bu da sistemin hata vermesine sebep veriyor. o yüzden singleton pattern yapıyoruz ve bunu static yaptığımızda hepsi için bir kez örnekliyor.bunu repository classımızın ctor ında da yapabiliriz
        public singleton()
        {
            if(db == null)
            {
                db= new DatabaseContext();  
            }
        }
    }
}
