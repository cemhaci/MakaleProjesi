using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class DatabaseContext :DbContext
    {
        public DbSet<Kategori> kategoriler { get; set; }
        public DbSet<Kullanici> kullanicilar { get; set; }
        public DbSet<Yorum> yorumlar { get; set; }
        public DbSet<Note> notlar{ get; set; }
        public DbSet<Like> likes { get; set; }
            
    }
}
