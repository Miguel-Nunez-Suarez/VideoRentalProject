namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateNameFieldInMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("update MembershipTypes set Name='Pay As You Go' where Id=1");
            Sql("update MembershipTypes set Name='Monthly' where Id=2");
            Sql("update MembershipTypes set Name='Quarterly' where Id=3");
            Sql("update MembershipTypes set Name='Annual' where Id=4");

        }

        public override void Down()
        {
        }
    }
}
