namespace webAPISecSess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id_Category = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Category);
            
            CreateTable(
                "dbo.GuidedTours",
                c => new
                    {
                        Id_GuidedTour = c.Int(nullable: false, identity: true),
                        GuidedTourName = c.String(nullable: false),
                        Distance = c.Double(nullable: false),
                        Id_Transport = c.Int(nullable: false),
                        Id_PlaceToEat = c.Int(),
                        Id_Category = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id_GuidedTour)
                .ForeignKey("dbo.Categories", t => t.Id_Category, cascadeDelete: true)
                .ForeignKey("dbo.PlaceToEats", t => t.Id_PlaceToEat)
                .ForeignKey("dbo.Transports", t => t.Id_Transport, cascadeDelete: true)
                .Index(t => t.Id_Transport)
                .Index(t => t.Id_PlaceToEat)
                .Index(t => t.Id_Category);
            
            CreateTable(
                "dbo.PlaceToEats",
                c => new
                    {
                        Id_PlaceToEat = c.Int(nullable: false, identity: true),
                        PlaceToEatName = c.String(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Address = c.String(nullable: false),
                        Id_Photo = c.String(nullable: false),
                        Price_Min = c.Int(nullable: false),
                        Price_Max = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_PlaceToEat);
            
            CreateTable(
                "dbo.PlaceWithOrders",
                c => new
                    {
                        Id_PlaceWithOrder = c.Int(nullable: false, identity: true),
                        OrderNumber = c.Int(nullable: false),
                        Id_GuidedTour = c.Int(nullable: false),
                        Id_TouristPlace = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_PlaceWithOrder)
                .ForeignKey("dbo.GuidedTours", t => t.Id_GuidedTour, cascadeDelete: true)
                .ForeignKey("dbo.TouristPlaces", t => t.Id_TouristPlace, cascadeDelete: true)
                .Index(t => new { t.Id_GuidedTour, t.OrderNumber }, unique: true, name: "OrdreTour")
                .Index(t => new { t.Id_GuidedTour, t.Id_TouristPlace }, unique: true, name: "PlaceTour");
            
            CreateTable(
                "dbo.TouristPlaces",
                c => new
                    {
                        Id_TouristPlace = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        TouristPlaceName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Time = c.Int(nullable: false),
                        Id_Photo = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id_TouristPlace);
            
            CreateTable(
                "dbo.Transports",
                c => new
                    {
                        Id_Transport = c.Int(nullable: false, identity: true),
                        TransportName = c.String(nullable: false, maxLength: 50),
                        Speed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Transport);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 256),
                        PasswordHash = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Address = c.String(maxLength: 500),
                        BirthDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.GuidedTours", "Id_Transport", "dbo.Transports");
            DropForeignKey("dbo.PlaceWithOrders", "Id_TouristPlace", "dbo.TouristPlaces");
            DropForeignKey("dbo.PlaceWithOrders", "Id_GuidedTour", "dbo.GuidedTours");
            DropForeignKey("dbo.GuidedTours", "Id_PlaceToEat", "dbo.PlaceToEats");
            DropForeignKey("dbo.GuidedTours", "Id_Category", "dbo.Categories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PlaceWithOrders", "PlaceTour");
            DropIndex("dbo.PlaceWithOrders", "OrdreTour");
            DropIndex("dbo.GuidedTours", new[] { "Id_Category" });
            DropIndex("dbo.GuidedTours", new[] { "Id_PlaceToEat" });
            DropIndex("dbo.GuidedTours", new[] { "Id_Transport" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Transports");
            DropTable("dbo.TouristPlaces");
            DropTable("dbo.PlaceWithOrders");
            DropTable("dbo.PlaceToEats");
            DropTable("dbo.GuidedTours");
            DropTable("dbo.Categories");
        }
    }
}
