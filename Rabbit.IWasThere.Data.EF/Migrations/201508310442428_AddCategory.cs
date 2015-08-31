namespace Rabbit.IWasThere.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Message", "CategoryId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Message", "CategoryId");
        }
    }
}
