namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentalTableWithoutIntIdForMoviesAndCustomers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rentals", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Rentals", "MovieId", "dbo.Movies");
            DropIndex("dbo.Rentals", new[] { "CustomerId" });
            DropIndex("dbo.Rentals", new[] { "MovieId" });
            RenameColumn(table: "dbo.Rentals", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Rentals", name: "MovieId", newName: "Movie_Id");
            AlterColumn("dbo.Rentals", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Rentals", "Movie_Id", c => c.Int());
            CreateIndex("dbo.Rentals", "Customer_Id");
            CreateIndex("dbo.Rentals", "Movie_Id");
            AddForeignKey("dbo.Rentals", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Rentals", "Movie_Id", "dbo.Movies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Rentals", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Rentals", new[] { "Movie_Id" });
            DropIndex("dbo.Rentals", new[] { "Customer_Id" });
            AlterColumn("dbo.Rentals", "Movie_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Rentals", "Customer_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Rentals", name: "Movie_Id", newName: "MovieId");
            RenameColumn(table: "dbo.Rentals", name: "Customer_Id", newName: "CustomerId");
            CreateIndex("dbo.Rentals", "MovieId");
            CreateIndex("dbo.Rentals", "CustomerId");
            AddForeignKey("dbo.Rentals", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rentals", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
