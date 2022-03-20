namespace WCD_Project.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WCD_Project.Models.StoreEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WCD_Project.Models.StoreEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Categories.AddOrUpdate(
                new Models.Category { CategoryID = 1, Name = "Quần áo mùa hè" },
                new Models.Category { CategoryID = 2, Name = "Quần áo mùa đông" }
                );
            context.Products.AddOrUpdate(
                new Models.Product { ProductID = 1, CategoryID = 1, Name = "QUẦN COTTON HÌNH CUTE", Thumbnail = "https://bizweb.dktcdn.net/100/447/374/products/1ad0254c-0e31-4d5f-b4cf-07cf730ad019.jpg?v=1646801395967", Price = 75000 },
                new Models.Product { ProductID = 2, CategoryID = 1, Name = "ÁO COTTON TOM JERRY", Thumbnail = "https://bizweb.dktcdn.net/100/447/374/products/74851edf-42d6-4adf-a773-d608f70381c2.jpg?v=1647505468400", Price = 105000 },
                new Models.Product { ProductID = 3, CategoryID = 2, Name = "BỘ NỈ THÊU DIOR", Thumbnail = "https://bizweb.dktcdn.net/100/447/374/products/5c72732f-41d1-4460-b1ec-ff9c88240b1d.jpg?v=1647500025417", Price = 210000 },
                new Models.Product { ProductID = 4, CategoryID = 2, Name = "BỘ NỈ DA CÁ PHỐI THÂN SUPREME", Thumbnail = "https://bizweb.dktcdn.net/100/447/374/products/fb73da38-8dd2-43dc-8090-b22fcb3bc882.jpg?v=1647505464077", Price = 220000 }
                );
        }
    }
}