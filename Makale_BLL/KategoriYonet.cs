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
		BusinessLayer_Sonuc<Kategori> sonuc = new BusinessLayer_Sonuc<Kategori>();
		repository<Kategori> rep_kat=new repository<Kategori>();
        public List<Kategori> listele()
        {
            return rep_kat.liste();
        }
        public Kategori kategoribul(int id)
        {
            return rep_kat.Find(x=>x.ID==id);
        }

		public BusinessLayer_Sonuc<Kategori> KategoriUpdate(Kategori kategori)
		{
			sonuc.nesne=rep_kat.Find(x=>x.Baslik==kategori.Baslik);
			if (sonuc.nesne != null)
			{
				sonuc.nesne.Baslik=kategori.Baslik;
				sonuc.nesne.Aciklama=kategori.Aciklama;
				int updatesonuc=rep_kat.Update(sonuc.nesne);
				if (updatesonuc < 1)
				{
					sonuc.hatalar.Add("kategori bilgileri değiştirilemedi");
				}
			}
			return sonuc;
		}

		public void kategoriSil(Kategori kategori)
		{
			throw new NotImplementedException();
		}

		public BusinessLayer_Sonuc<Kategori> KategoriEkle(Kategori kategori)
		{
			
			sonuc.nesne=rep_kat.Find(x=>x.Baslik==kategori.Baslik);
			if (sonuc.nesne != null)
			{
				sonuc.hatalar.Add("bu katagori kayıtlı");

			}
			else
			{
				int kayit=rep_kat.Insert(kategori);
				if (kayit > 1)
				{
					sonuc.hatalar.Add("kategori kaydedilemedi");
				}
			}
			return sonuc;
		}
	}
}
