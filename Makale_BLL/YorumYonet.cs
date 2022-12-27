using Makale_dataAccessLayer;
using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class YorumYonet
    {
        repository<Yorum> rep_yorum=new repository<Yorum>();
        public Yorum YorumBul(int id)
        {
            return rep_yorum.Find(x=>x.ID==id);
        }
        public int yorumguncelle(Yorum yorum)
        {
            return rep_yorum.Update(yorum);
        }
        public int delete(int id)
        {
            Yorum silinecekyorum=rep_yorum.Find(x=>x.ID==id);
            return rep_yorum.Delete(silinecekyorum);
        }
        public int yorumekle(Yorum yorum)
        {
            return rep_yorum.Insert(yorum);
        }
    }
}
