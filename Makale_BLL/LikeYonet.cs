using Makale_dataAccessLayer;
using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
	}
}
