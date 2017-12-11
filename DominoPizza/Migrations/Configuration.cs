namespace DominoPizza.Migrations
{
    using DominosPizza.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DominosPizza.Models.DominosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DominosContext db)
        {
            //db.UserRoles.Add(new UserRole { UserRoleId = 1, UserRoleName = "Пользователь" });
            //db.UserRoles.Add(new UserRole { UserRoleId = 2, UserRoleName = "Администратор" });
            //db.UserRoles.Add(new UserRole { UserRoleId = 3, UserRoleName = "Менеджер" });
            //db.UserRoles.Add(new UserRole { UserRoleId = 4, UserRoleName = "Повар" });
            //db.UserRoles.Add(new UserRole { UserRoleId = 5, UserRoleName = "Курьер" });

            //db.Customers.Add(new Customer { CustomerEmail = "admin@dominos.com", CustomerFirstName = "Василий", CustomerPatronymic = "Алибабаевич", CustomerLastName = "Администратор", CustomerBirthDate = DateTime.Now, CustomerSex = "Male", CustomerPassword = "12345", CustomerPasswordConfirm = "12345", CustomerRoleId = 2, CustomerPhone = "79601005060" });
            //db.Customers.Add(new Customer { CustomerEmail = "adminos@dominos.com", CustomerFirstName = "Василий", CustomerPatronymic = "Алибабаевич", CustomerLastName = "Администратор", CustomerBirthDate = DateTime.Now, CustomerSex = "Male", CustomerPassword = "12345", CustomerPasswordConfirm = "12345", CustomerRoleId = 2, CustomerPhone = "79601005060" });
            //db.Customers.Add(new Customer { CustomerEmail = "admindos@dominos.com", CustomerFirstName = "Василий", CustomerPatronymic = "Алибабаевич", CustomerLastName = "Администратор", CustomerBirthDate = DateTime.Now, CustomerSex = "Male", CustomerPassword = "12345", CustomerPasswordConfirm = "12345", CustomerRoleId = 2, CustomerPhone = "79601005060" });
            //db.Customers.Add(new Customer { CustomerEmail = "adminips@dominos.com", CustomerFirstName = "Василий", CustomerPatronymic = "Алибабаевич", CustomerLastName = "Администратор", CustomerBirthDate = DateTime.Now, CustomerSex = "Male", CustomerPassword = "12345", CustomerPasswordConfirm = "12345", CustomerRoleId = 2, CustomerPhone = "79601005060" });

            //db.Products.Add(new Product { ProductName = "Четыре сыра", ProductWeight = 600, ProductPrice = 671, ImageLink = "/img/1.jpg", ProductDescription = "Томатный соус, моцарелла, сыр блючиз и смесь сыров чеддар и пармезан", ProductType = 1 });
            //db.Products.Add(new Product { ProductName = "Двойная пепперони", ProductWeight = 610, ImageLink = "/img/2.jpg", ProductDescription = "Томатный соус, моцарелла и пикантная пепперони", ProductPrice = 672, ProductType = 1 });
            //db.Products.Add(new Product { ProductName = "Итальянская", ProductWeight = 610, ProductType = 1, ImageLink = "/img/3.jpg", ProductDescription = "Томатный соус, пикантная пепперони, шампиньоны, моцарелла, маслины и орегано", ProductPrice = 673 });
            //db.Products.Add(new Product { ProductName = "Ранч-пицца", ProductWeight = 620, ProductType = 1, ImageLink = "/img/4.jpg", ProductDescription = "Цыпленок, ветчина, соус ранч, моцарелла, томаты и чеснок", ProductPrice = 674 });
            //db.Products.Add(new Product { ProductName = "Мясная", ProductWeight = 630, ProductType = 1, ImageLink = "/img/5.jpg", ProductDescription = "Томатный соус, моцарелла, охотничьи колбаски, ветчина и бекон", ProductPrice = 675 });
            //db.Products.Add(new Product { ProductName = "Грибы и ветчина", ProductWeight = 620, ProductType = 1, ImageLink = "/img/6.jpg", ProductDescription = "Томатный соус, моцарелла, ветчина и шампиньоны", ProductPrice = 676 });
            //db.Products.Add(new Product { ProductName = "Пицца-пирог", ProductWeight = 600, ProductType = 1, ImageLink = "/img/7.jpg", ProductDescription = "Молоко сгущенное, брусника и ананасы", ProductPrice = 677 });
            //db.Products.Add(new Product { ProductName = "Маргарита", ProductWeight = 610, ProductType = 1, ImageLink = "/img/8.jpg", ProductDescription = "Томатный соус, томаты, моцарелла и орегано", ProductPrice = 678 });

        }
    }
}
