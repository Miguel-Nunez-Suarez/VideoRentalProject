namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3cba7a14-f579-4a1c-863e-f3ea05bb1abf', N'admin@vidly.com', 0, N'AIvsoH5E9KAAZeqU6qYbNPcEVZOc6lNLVjwcsstRbvVLBwGIyIWIpHIW79HTtwhD+Q==', N'3ea33afb-6959-475f-b222-d1b98bf24053', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3d83f32a-2a63-4f6c-84cc-33ca85ba09ce', N'guest@vidly.com', 0, N'AI8kZHFYN+nKdyx06dqXK0BUU9wHiSRaHvUOdHbLhmv0c/keB+YKPdWRVPbnC6+28A==', N'5a17c017-1a65-4a15-9609-81a3c75fe58f', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
    
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a76120ab-939c-4e44-a941-a2352ece81ad', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3cba7a14-f579-4a1c-863e-f3ea05bb1abf', N'a76120ab-939c-4e44-a941-a2352ece81ad')

");
        }
        
        public override void Down()
        {
        }
    }
}
