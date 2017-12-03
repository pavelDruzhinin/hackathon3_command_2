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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DominosPizza.Models.DominosContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        /*              db.UserRoles.Add(new UserRole { UserRoleId = 1, UserRoleName = "��������" });
           db.UserRoles.Add(new UserRole { UserRoleId = 2, UserRoleName = "�����" });
           db.UserRoles.Add(new UserRole { UserRoleId = 3, UserRoleName = "������" });
            db.Products.Add(new Product { ProductName = "������ ����", ProductWeight = 600, ProductPrice = 671, ImageLink = "/img/1.jpg", ProductDescription = "�������� ����, ���������, ��� ������ � ����� ����� ������ � ��������", ProductType = 1 });
             db.Products.Add(new Product { ProductName = "������� ���������", ProductWeight = 610, ImageLink = "/img/2.jpg", ProductDescription = "�������� ����, ��������� � ��������� ���������", ProductPrice = 672, ProductType = 1 });
              db.Products.Add(new Product { ProductName = "�����������", ProductWeight = 610, ProductType = 1, ImageLink = "/img/3.jpg", ProductDescription = "�������� ����, ��������� ���������, ����������, ���������, ������� � �������", ProductPrice = 673 });
             db.Products.Add(new Product { ProductName = "����-�����", ProductWeight = 620, ProductType = 1, ImageLink = "/img/4.jpg", ProductDescription = "��������, �������, ���� ����, ���������, ������ � ������", ProductPrice = 674 });
              db.Products.Add(new Product { ProductName = "������", ProductWeight = 630, ProductType = 1, ImageLink = "/img/5.jpg", ProductDescription = "�������� ����, ���������, ��������� ��������, ������� � �����", ProductPrice = 675 });
               db.Products.Add(new Product { ProductName = "����� � �������", ProductWeight = 620, ProductType = 1, ImageLink = "/img/6.jpg", ProductDescription = "�������� ����, ���������, ������� � ����������", ProductPrice = 676 });
             db.Products.Add(new Product { ProductName = "�����-�����", ProductWeight = 600, ProductType = 1, ImageLink = "/img/7.jpg", ProductDescription = "������ ���������, �������� � �������", ProductPrice = 677 });
               db.Products.Add(new Product { ProductName = "���������", ProductWeight = 610, ProductType = 1, ImageLink = "/img/8.jpg", ProductDescription = "�������� ����, ������, ��������� � �������", ProductPrice = 678 });
           db.Users.Add(new User { UserRoleId = 1, UserFirstName = "������������" });
           db.Users.Add(new User { UserRoleId = 2, UserFirstName = "ϸ�������" });
           db.Users.Add(new User { UserRoleId = 3, UserFirstName = "����������" });
           db.Customers.Add(new Customer {  CustomerFirstName = "������������", CustomerBirthDate = DateTime.Now });
           db.Customers.Add(new Customer {  CustomerFirstName = "ϸ�������", CustomerBirthDate = DateTime.Now });
           db.Customers.Add(new Customer {  CustomerFirstName = "����������", CustomerBirthDate = DateTime.Now });
           //db.TaskCommentsDbSet.Add(new TaskComments { TaskId = 1, UserName = "ϸ�������", CommentText = "�� ����� ������� � ����, ��������� ����� � ������������ ��������� �����������" });
           //db.TaskCommentsDbSet.Add(new TaskComments { TaskId = 1, UserName = "����������", CommentText = "� ���� ��������, ������ ������ � �������" });
           //db.TaskCommentsDbSet.Add(new TaskComments { TaskId = 1, UserName = "������������", CommentText = "������, ����� ����� ������, �� ���������" });
           */
        }
    }
}
