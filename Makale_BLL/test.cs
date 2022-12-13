using Makale_dataAccessLayer;
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
             DatabaseContext db =new DatabaseContext(); //burada örneklememizi yaptığımızda seed metodumuzu tetikklemek için set initilazer ımızla tetiklemeliyiz o yüzden database örneklendiğinde seed tetiklenicek
             //db.Database.CreateIfNotExists();   seed metodu oluşturduğumuz için artık veri tabanı oluştur metodumuzda. oyüzden buna gerek yok veri tabanımızı veri tabanı oluştur metodunda oluşturcak
             db.kategoriler.ToList();
           
            
        }
    }
}
