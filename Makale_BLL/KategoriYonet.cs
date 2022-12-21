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

		public BusinessLayer_Sonuc<Kategori> kategoriSil(Kategori kategori)
		{
			BusinessLayer_Sonuc<Kategori> sonuc = new BusinessLayer_Sonuc<Kategori>();
			sonuc.nesne = rep_kat.Find(x => x.ID == kategori.ID);

			repository<Note> rep_note = new repository<Note>();
			repository<Yorum> rep_yorum = new repository<Yorum>();
			repository<Like> rep_like = new repository<Like>();

            if (sonuc.nesne != null)
            {
                //foreach (Not not in sonuc.nesne.Notlar.ToList())
                //{
                //    foreach (Yorum yorum in not.Yorumlar.ToList())
                //    {
                //        rep_yorum.Delete(yorum);
                //        //yorumlar silinecek
                //    }

                //    foreach (Begeni begen in not.Begeniler.ToList())
                //    {
                //        rep_begeni.Delete(begen);
                //        //beğeniler silinecek
                //    }

                //    //notlar silinecek
                //    rep_not.Delete(not);
                //}

                int silsonuc = rep_kat.Delete(sonuc.nesne);//kategori siliniyor
                if (silsonuc < 1)
                    sonuc.hatalar.Add("Kullanıcı silinemedi.");
            }
            else
            {
                sonuc.hatalar.Add("Kullanıcı bulunamadı");
            }
            return sonuc;
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
				if (kayit < 1)
				{
					sonuc.hatalar.Add("kategori kaydedilemedi");
				}
			}
			return sonuc;
		}
	}
}
