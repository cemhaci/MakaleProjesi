using Makale_dataAccessLayer;
using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class KategoriYonet
    {
        repository<Kategori> rep_kat=new repository<Kategori>();
        public List<Kategori> listele()
        {
            return rep_kat.liste();
        }
        public Kategori kategoribul(int id)
        {
            return rep_kat.Find(x=>x.ID==id);
        }
    }
}
