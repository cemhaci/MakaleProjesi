using Makale_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_dataAccessLayer
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Kategori> kategoriler { get; set; }
        public DbSet<Kullanici> kullanicilar { get; set; }
        public DbSet<Yorum> yorumlar { get; set; }
        public DbSet<Note> notlar{ get; set; }
        public DbSet<Like> likes { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new veriTabaniOlustur());
        }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)  //bu işlem cascede ile silmemizi sağlıyor ve bu metotun adı fluentapi
		{
			modelBuilder.Entity<Note>().HasMany(n=>n.yorumlar).WithRequired(y=>y.not).WillCascadeOnDelete(true);
            //yorumlar notlarla ilişkilidir.ve her yorumun bir notu olmak zorunda diyip cascede ile bu ilişkiyle siliyor
            modelBuilder.Entity<Note>().HasMany(n=>n.like).WithRequired(b=>b.not).WillCascadeOnDelete(true);
		}
	}
}
