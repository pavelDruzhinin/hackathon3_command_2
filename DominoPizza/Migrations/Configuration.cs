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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DominosPizza.Models.DominosContext db)
        {
            //db.UserRolesDbSet.Add(new UserRoles { RoleId = 1, RoleName = "��������" });
            //db.UserRolesDbSet.Add(new UserRoles { RoleId = 2, RoleName = "�����" });
            //db.UserRolesDbSet.Add(new UserRoles { RoleId = 3, RoleName = "������" });
          db.Products.Add(new Product { ProductName = "������ ����", ProductWeight = 600, ProductPrice = 671, ImageLink = "/img/1.jpg", ProductDescription = "�������� ����, ���������, ��� ������ � ����� ����� ������ � ��������", ProductType = 1 });
           db.Products.Add(new Product { ProductName = "������� ���������", ProductWeight = 610, ImageLink = "/img/2.jpg", ProductDescription = "�������� ����, ��������� � ��������� ���������", ProductPrice = 672, ProductType = 1 });
          db.Products.Add(new Product { ProductName = "�����������", ProductWeight = 610, ProductType = 1, ImageLink = "/img/3.jpg", ProductDescription = "�������� ����, ��������� ���������, ����������, ���������, ������� � �������", ProductPrice = 673 });
            db.Products.Add(new Product { ProductName = "����-�����", ProductWeight = 620, ProductType = 1, ImageLink = "/img/4.jpg", ProductDescription = "��������, �������, ���� ����, ���������, ������ � ������", ProductPrice = 674 });
           db.Products.Add(new Product { ProductName = "������", ProductWeight = 630, ProductType = 1, ImageLink = "/img/5.jpg", ProductDescription = "�������� ����, ���������, ��������� ��������, ������� � �����", ProductPrice = 675 });
          db.Products.Add(new Product { ProductName = "����� � �������", ProductWeight = 620, ProductType = 1, ImageLink = "/img/6.jpg", ProductDescription = "�������� ����, ���������, ������� � ����������", ProductPrice = 676 });
            db.Products.Add(new Product { ProductName = "�����-�����", ProductWeight = 600, ProductType = 1, ImageLink = "/img/7.jpg", ProductDescription = "������ ���������, �������� � �������", ProductPrice = 677 });
            db.Products.Add(new Product { ProductName = "���������", ProductWeight = 610, ProductType = 1, ImageLink = "/img/8.jpg", ProductDescription = "�������� ����, ������, ��������� � �������", ProductPrice = 678 });
          //  db.UsersDbSet.Add(new Users { UserRoleId = 1, UserName = "������������" });
          //  db.UsersDbSet.Add(new Users { UserRoleId = 2, UserName = "ϸ�������" });
          //  db.UsersDbSet.Add(new Users { UserRoleId = 3, UserName = "����������" });
          //  db.TaskCommentsDbSet.Add(new TaskComments { TaskId = 1, UserName = "ϸ�������", CommentText = "�� ����� ������� � ����, ��������� ����� � ������������ ��������� �����������" });
          // db.TaskCommentsDbSet.Add(new TaskComments { TaskId = 1, UserName = "����������", CommentText = "� ���� ��������, ������ ������ � �������" });
          //  db.TaskCommentsDbSet.Add(new TaskComments { TaskId = 1, UserName = "������������", CommentText = "������, ����� ����� ������, �� ���������" });

        }
    }
}
