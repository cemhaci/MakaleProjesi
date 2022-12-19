namespace Makale_dataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kullaniciprofilresimekle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.kullanici","profilresim",c=>c.String(maxLength:50));
            Sql("update kullanici set profilresim='Centaur-icon.png'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kullanici", "profilresim");
        }
    }
}
