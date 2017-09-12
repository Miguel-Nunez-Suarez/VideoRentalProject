namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsSuscribedToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "IsSuscbribedToNewsletter", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "IsSuscbribedToNewsletter");
        }
    }
}
