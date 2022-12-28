using Makale_dataAccessLayer;
using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Makale_BLL
{
	public class LikeYonet
	{
		repository<Like> rep_like=new repository<Like>();

		public IQueryable<Like> listeleQuaryable()
		{
			return rep_like.listeQ();
		}
		public List<Like> Listele(Expression<Func<Like, bool>> kosul)
		{
            return rep_like.liste(kosul);
        }

		public Like BegeniBul(int notid, int kullaniciId)
		{
            return rep_like.Find(x => x.not.ID == notid && x.kullanici.ID == kullaniciId);
        }

		public int BegeniSil(Like begen)
		{
            return rep_like.Delete(begen);
        }

		public int BegeniEkle(Like begen)
		{
            
            return rep_like.Insert(begen);
        }
	}
}
