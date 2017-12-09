namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DominosPizza.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DominosPizza.Models.DominosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DominosContext db)
        {
            //переработал, теперь работает с нашей БД
            //db.UserRoles.Add(new UserRole {  Name = "Клиент" });
            //db.UserRoles.Add(new UserRole {  Name = "Менеджер" });
            //db.UserRoles.Add(new UserRole {  Name = "Повар" });
            //db.UserRoles.Add(new UserRole {  Name = "Курьер" });
            //db.UserRoles.Add(new UserRole {  Name = "Администратор" });


            //db.Products.Add(new Product { Name = "Четыре сыра", Weight = 600, Price = 671, ImageLink = "/img/1.jpg", Description = "Томатный соус, моцарелла, сыр блючиз и смесь сыров чеддар и пармезан", Type = 1 });
            //db.Products.Add(new Product { Name = "Двойная пепперони", Weight = 610, ImageLink = "/img/2.jpg", Description = "Томатный соус, моцарелла и пикантная пепперони", Price = 672, Type = 1 });
            //db.Products.Add(new Product { Name = "Итальянская", Weight = 610, Type = 1, ImageLink = "/img/3.jpg", Description = "Томатный соус, пикантная пепперони, шампиньоны, моцарелла, маслины и орегано", Price = 673 });
            //db.Products.Add(new Product { Name = "Ранч-пицца", Weight = 620, Type = 1, ImageLink = "/img/4.jpg", Description = "Цыпленок, ветчина, соус ранч, моцарелла, томаты и чеснок", Price = 674 });
            //db.Products.Add(new Product { Name = "Мясная", Weight = 630, Type = 1, ImageLink = "/img/5.jpg", Description = "Томатный соус, моцарелла, охотничьи колбаски, ветчина и бекон", Price = 675 });
            //db.Products.Add(new Product { Name = "Грибы и ветчина", Weight = 620, Type = 1, ImageLink = "/img/6.jpg", Description = "Томатный соус, моцарелла, ветчина и шампиньоны", Price = 676 });
            //db.Products.Add(new Product { Name = "Пицца-пирог", Weight = 600, Type = 1, ImageLink = "/img/7.jpg", Description = "Молоко сгущенное, брусника и ананасы", Price = 677 });
            //db.Products.Add(new Product { Name = "Маргарита", Weight = 610, Type = 1, ImageLink = "/img/8.jpg", Description = "Томатный соус, томаты, моцарелла и орегано", Price = 678 });

            //db.Users.Add(new User { FirstName = "Анонимный", LastName = "Клиент", BirthDate = DateTime.Now, Sex = true, Email = "client@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 1 });
            //db.Users.Add(new User { FirstName = "Тестовый", LastName = "Администратор", BirthDate = DateTime.Now, Sex = true, Email = "admin@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 5 });
            //db.Users.Add(new User { FirstName = "Анна", LastName = "менеджер", BirthDate = DateTime.Now, Sex = true, Email = "manager@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 2 });
            //db.Users.Add(new User { FirstName = "Пётр", LastName = "повар", BirthDate = DateTime.Now, Sex = true, Email = "cook@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 3 });
            //db.Users.Add(new User { FirstName = "Иван", LastName = "курьер", BirthDate = DateTime.Now, Sex = true, Email = "courier@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 4 });
            //db.Users.Add(new User { FirstName = "Довольный", LastName = "Клиент", BirthDate = DateTime.Now, Sex = true, Email = "client2@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 1 });
            //db.Contacts.Add(new Contact { FullName = "Довольный Клиент", Phone = "+71234567890", Address = "Адрес Довольного клиента из Contacts", OrderDateTime = DateTime.Now, UserId = 6 });


            //db.OnlineChatMessages.Add(new OnlineChatMessage { UserId = 2, OnlineChatUniqueId = "WelcomeToChat", DateTime = DateTime.Now, Assigned = false, IsByManager = true, Text = "Добро пожаловать в чат! Ожидайте сообщения от клиентов.", IsNew = true, ManagerId = 1 });

            //для дбавления этих комментов в таблице Task должно быть задание с Id = 1
            //db.UserComments.Add(new UserComment { Text = "примите заказ на пиццу", DateTime = DateTime.Now, UserId = 1, TaskId = 1 });
            //db.UserComments.Add(new UserComment { Text = "очень важно сделать в срок", DateTime = DateTime.Now, UserId = 2, TaskId = 1 });


        }
    }
}
