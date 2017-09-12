namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Genres (Id, Name) Values (1,'Horror')");
            Sql("insert into Genres (Id, Name) Values (2,'Sci-Fi')");
            Sql("insert into Genres (Id, Name) Values (3,'Drama')");
            Sql("insert into Genres (Id, Name) Values (4,'Thriller')");
            Sql("insert into Genres (Id, Name) Values (5,'Comedy')");
            Sql("insert into Genres (Id, Name) Values (6,'Animated')");
            Sql("insert into Genres (Id, Name) Values (7,'Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
