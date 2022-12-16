using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class BusinessLayer_Sonuc<T> where T:class
    {
        public List<string> hatalar { get; set; }
        public T nesne { get; set; }
        public BusinessLayer_Sonuc()
        {
            hatalar=new List<string>();
        }
    }
}
