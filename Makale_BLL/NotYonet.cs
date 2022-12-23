using Makale_dataAccessLayer;
using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class NotYonet
    {
		BusinessLayer_Sonuc<Note> notsonuc=new BusinessLayer_Sonuc<Note>();
        repository<Note> rep_not=new repository<Note>();
         public List<Note> listele()
        {

            return rep_not.liste();
        }
        public IQueryable<Note> listeleQueryable()
        {
            return rep_not.listeQ();
        }

		public Note NotBul(int? id)
		{
			return rep_not.Find(x=>x.ID==id);
		}

		public BusinessLayer_Sonuc<Note> NotKaydet(Note note)
		{
			notsonuc.nesne=rep_not.Find(x=>x.Baslik==note.Baslik&&x.KategoriId==note.KategoriId);

			if (notsonuc.nesne != null)
			{
				notsonuc.hatalar.Add("bu kategoride bu isimde makale kayıtlı");
			}
			else
			{
				int sonuc=rep_not.Insert(note);
				if (sonuc < 1)
				
					notsonuc.hatalar.Add("kayıt eklenemedi");				
			}
			return notsonuc;
		}

		public BusinessLayer_Sonuc<Note> NotUpdate(Note note)
		{
			notsonuc.nesne= rep_not.Find(x=>x.ID==note.ID);

			if(notsonuc.nesne != null)
			{
				notsonuc.nesne.Baslik=note.Baslik;
				notsonuc.nesne.Text= note.Text;
				notsonuc.nesne.Taslak=note.Taslak;
				notsonuc.nesne.KategoriId=note.KategoriId;
				if (rep_not.Update(notsonuc.nesne) < 1)
				{
					notsonuc.hatalar.Add("güncellenemedi");
				}
		    }
			return notsonuc;
		}

		public BusinessLayer_Sonuc<Note> NotSil(Note note)
		{
			notsonuc.nesne = rep_not.Find(x=>x.ID==note.ID);
			if(notsonuc.nesne != null)
			{
				int sonuc=rep_not.Delete(note);
				if (sonuc < 1)
				{
					notsonuc.hatalar.Add("kayıt silinemedi");
				}
			}
			else
			{
				notsonuc.hatalar.Add("kayıt bulunamamadı");
			}
			return notsonuc;
		}
	}
}
