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
			throw new NotImplementedException();
		}

		public void NotKaydet(Note note)
		{
			throw new NotImplementedException();
		}

		public void NotUpdate(Note note)
		{
			throw new NotImplementedException();
		}

		public void NotSil(Note note)
		{
			throw new NotImplementedException();
		}
	}
}
