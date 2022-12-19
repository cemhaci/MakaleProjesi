namespace Makale_dataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(nullable: false, maxLength: 300),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Notlar",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 50),
                        Text = c.String(nullable: false, maxLength: 250),
                        Taslak = c.Boolean(nullable: false),
                        BegeniSayisi = c.Int(nullable: false),
                        KategoriId = c.Int(nullable: false),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                        kullanici_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kategori", t => t.KategoriId, cascadeDelete: true)
                .ForeignKey("dbo.Kullanici", t => t.kullanici_ID)
                .Index(t => t.KategoriId)
                .Index(t => t.kullanici_ID);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ad = c.String(maxLength: 50),
                        Soyad = c.String(maxLength: 50),
                        KullaniciAd = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Sifre = c.String(nullable: false, maxLength: 50),
                        Aktif = c.Boolean(nullable: false),
                        Admin = c.Boolean(nullable: false),
                        AktifGuid = c.Guid(nullable: false),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        kullanici_ID = c.Int(),
                        not_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kullanici", t => t.kullanici_ID)
                .ForeignKey("dbo.Notlar", t => t.not_ID)
                .Index(t => t.kullanici_ID)
                .Index(t => t.not_ID);
            
            CreateTable(
                "dbo.Yorum",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 250),
                        KayitTarihi = c.DateTime(nullable: false),
                        DegistirmeTarihi = c.DateTime(nullable: false),
                        DegistirenKullanici = c.String(nullable: false, maxLength: 20),
                        kullanici_ID = c.Int(),
                        not_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kullanici", t => t.kullanici_ID)
                .ForeignKey("dbo.Notlar", t => t.not_ID)
                .Index(t => t.kullanici_ID)
                .Index(t => t.not_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorum", "not_ID", "dbo.Notlar");
            DropForeignKey("dbo.Yorum", "kullanici_ID", "dbo.Kullanici");
            DropForeignKey("dbo.Notlar", "kullanici_ID", "dbo.Kullanici");
            DropForeignKey("dbo.Like", "not_ID", "dbo.Notlar");
            DropForeignKey("dbo.Like", "kullanici_ID", "dbo.Kullanici");
            DropForeignKey("dbo.Notlar", "KategoriId", "dbo.Kategori");
            DropIndex("dbo.Yorum", new[] { "not_ID" });
            DropIndex("dbo.Yorum", new[] { "kullanici_ID" });
            DropIndex("dbo.Like", new[] { "not_ID" });
            DropIndex("dbo.Like", new[] { "kullanici_ID" });
            DropIndex("dbo.Notlar", new[] { "kullanici_ID" });
            DropIndex("dbo.Notlar", new[] { "KategoriId" });
            DropTable("dbo.Yorum");
            DropTable("dbo.Like");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Notlar");
            DropTable("dbo.Kategori");
        }
    }
}
