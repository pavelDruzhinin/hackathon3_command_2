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
            //�����������, ������ �������� � ����� ��
            //db.UserRoles.Add(new UserRole {  Name = "������" });
            //db.UserRoles.Add(new UserRole {  Name = "��������" });
            //db.UserRoles.Add(new UserRole {  Name = "�����" });
            //db.UserRoles.Add(new UserRole {  Name = "������" });
            //db.UserRoles.Add(new UserRole {  Name = "�������������" });


            //db.Products.Add(new Product { Name = "������ ����", Weight = 600, Price = 671, ImageLink = "/img/1.jpg", Description = "�������� ����, ���������, ��� ������ � ����� ����� ������ � ��������", Type = 1 });
            //db.Products.Add(new Product { Name = "������� ���������", Weight = 610, ImageLink = "/img/2.jpg", Description = "�������� ����, ��������� � ��������� ���������", Price = 672, Type = 1 });
            //db.Products.Add(new Product { Name = "�����������", Weight = 610, Type = 1, ImageLink = "/img/3.jpg", Description = "�������� ����, ��������� ���������, ����������, ���������, ������� � �������", Price = 673 });
            //db.Products.Add(new Product { Name = "����-�����", Weight = 620, Type = 1, ImageLink = "/img/4.jpg", Description = "��������, �������, ���� ����, ���������, ������ � ������", Price = 674 });
            //db.Products.Add(new Product { Name = "������", Weight = 630, Type = 1, ImageLink = "/img/5.jpg", Description = "�������� ����, ���������, ��������� ��������, ������� � �����", Price = 675 });
            //db.Products.Add(new Product { Name = "����� � �������", Weight = 620, Type = 1, ImageLink = "/img/6.jpg", Description = "�������� ����, ���������, ������� � ����������", Price = 676 });
            //db.Products.Add(new Product { Name = "�����-�����", Weight = 600, Type = 1, ImageLink = "/img/7.jpg", Description = "������ ���������, �������� � �������", Price = 677 });
            //db.Products.Add(new Product { Name = "���������", Weight = 610, Type = 1, ImageLink = "/img/8.jpg", Description = "�������� ����, ������, ��������� � �������", Price = 678 });

            //db.Users.Add(new User { FirstName = "���������", LastName = "������", BirthDate = DateTime.Now, Sex = true, Email = "client@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 1 });
            //db.Users.Add(new User { FirstName = "��������", LastName = "�������������", BirthDate = DateTime.Now, Sex = true, Email = "admin@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 5 });
            //db.Users.Add(new User { FirstName = "����", LastName = "��������", BirthDate = DateTime.Now, Sex = true, Email = "manager@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 2 });
            //db.Users.Add(new User { FirstName = "ϸ��", LastName = "�����", BirthDate = DateTime.Now, Sex = true, Email = "cook@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 3 });
            //db.Users.Add(new User { FirstName = "����", LastName = "������", BirthDate = DateTime.Now, Sex = true, Email = "courier@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 4 });
            //db.Users.Add(new User { FirstName = "���������", LastName = "������", BirthDate = DateTime.Now, Sex = true, Email = "client2@domino.loc", Password = "12345", PasswordConfirm = "12345", UserRoleId = 1 });
            //db.Contacts.Add(new Contact { FullName = "��������� ������", Phone = "+71234567890", Address = "����� ���������� ������� �� Contacts", OrderDateTime = DateTime.Now, UserId = 6 });


            //db.OnlineChatMessages.Add(new OnlineChatMessage { UserId = 2, OnlineChatUniqueId = "WelcomeToChat", DateTime = DateTime.Now, Assigned = false, IsByManager = true, Text = "����� ���������� � ���! �������� ��������� �� ��������.", IsNew = true, ManagerId = 1 });

            //��� ��������� ���� ��������� � ������� Task ������ ���� ������� � Id = 1
            //db.UserComments.Add(new UserComment { Text = "������� ����� �� �����", DateTime = DateTime.Now, UserId = 1, TaskId = 1 });
            //db.UserComments.Add(new UserComment { Text = "����� ����� ������� � ����", DateTime = DateTime.Now, UserId = 2, TaskId = 1 });


        }
    }
}
