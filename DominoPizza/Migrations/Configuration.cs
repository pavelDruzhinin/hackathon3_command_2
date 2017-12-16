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

        protected override void Seed(DominosContext db)
        {
            //db.UserRoles.Add(new UserRole { UserRoleId = 1, UserRoleName = "������������" });
            //db.UserRoles.Add(new UserRole { UserRoleId = 2, UserRoleName = "�������������" });
            //db.UserRoles.Add(new UserRole { UserRoleId = 3, UserRoleName = "��������" });
            //db.UserRoles.Add(new UserRole { UserRoleId = 4, UserRoleName = "�����" });
            //db.UserRoles.Add(new UserRole { UserRoleId = 5, UserRoleName = "������" });

            //db.Customers.Add(new Customer { CustomerEmail = "admin@dominos.com", CustomerFirstName = "�������", CustomerPatronymic = "�����������", CustomerLastName = "�������������", CustomerBirthDate = DateTime.Now, CustomerSex = "Male", CustomerPassword = "12345", CustomerPasswordConfirm = "12345", CustomerRoleId = 2, CustomerPhone = "79601005060" });
            ////��������� ������ ����� ��� ������ ����, CustomerId = 2 �������� � ���� MessagingController/AddNewMessageToChat, ���� ���� ����� ���� ��, �� � ���� ���� ��� ��������
            //db.Customers.Add(new Customer { CustomerEmail = "unknownclient@dominos.loc", CustomerFirstName = "���������", CustomerPatronymic = "��������������������", CustomerLastName = "������", CustomerBirthDate = DateTime.Now, CustomerSex = "Male", CustomerPassword = "12345", CustomerPasswordConfirm = "12345", CustomerRoleId = 1, CustomerPhone = "70000000000" });

            //db.Products.Add(new Product { ProductName = "������ ����", ProductWeight = 600, ProductPrice = 671, ImageLink = "/img/5.jpg", ProductDescription = "�������� ����, ���������, ��� ������ � ����� ����� ������ � ��������", ProductType = 1 });
            //db.Products.Add(new Product { ProductName = "������� ���������", ProductWeight = 610, ImageLink = "/img/1.jpg", ProductDescription = "�������� ����, ��������� � ��������� ���������", ProductPrice = 672, ProductType = 1 });
            //db.Products.Add(new Product { ProductName = "�����������", ProductWeight = 610, ProductType = 1, ImageLink = "/img/10.jpg", ProductDescription = "�������� ����, ��������� ���������, ����������, ���������, ������� � �������", ProductPrice = 673 });
            //db.Products.Add(new Product { ProductName = "����-�����", ProductWeight = 620, ProductType = 1, ImageLink = "/img/7.jpg", ProductDescription = "��������, �������, ���� ����, ���������, ������ � ������", ProductPrice = 674 });
            //db.Products.Add(new Product { ProductName = "������", ProductWeight = 630, ProductType = 1, ImageLink = "/img/9.jpg", ProductDescription = "�������� ����, ���������, ��������� ��������, ������� � �����", ProductPrice = 675 });
            //db.Products.Add(new Product { ProductName = "����� � �������", ProductWeight = 620, ProductType = 1, ImageLink = "/img/6.jpg", ProductDescription = "�������� ����, ���������, ������� � ����������", ProductPrice = 676 });
            //db.Products.Add(new Product { ProductName = "�����-�����", ProductWeight = 600, ProductType = 1, ImageLink = "/img/4.jpg", ProductDescription = "������ ���������, �������� � �������", ProductPrice = 677 });
            //db.Products.Add(new Product { ProductName = "���������", ProductWeight = 610, ProductType = 1, ImageLink = "/img/8.jpg", ProductDescription = "�������� ����, ������, ��������� � �������", ProductPrice = 678 });
            //db.Products.Add(new Product { ProductName = "������������", ProductWeight = 600, ProductPrice = 671, ImageLink = "/img/11.jpg", ProductDescription = "�������� ����, ��������, ������� �����, ��� �������, ����������, ���������, ������ � ������ ����� ���������", ProductType = 1 });
            //db.Products.Add(new Product { ProductName = "������� �������", ProductWeight = 610, ImageLink = "/img/12.jpg", ProductDescription = "�������� ����, ��������, ��� �������, ���������, ���� ������� � �����", ProductPrice = 672, ProductType = 1 });
            //db.Products.Add(new Product { ProductName = "�������", ProductWeight = 610, ProductType = 1, ImageLink = "/img/13.jpg", ProductDescription = "�������� ����, �������, ������� �����, ��� �������, ��������� � ��������", ProductPrice = 673 });
            //db.Products.Add(new Product { ProductName = "������ ������", ProductWeight = 620, ProductType = 1, ImageLink = "/img/14.jpg", ProductDescription = "�������� ����, �������, ������ ������, ����������, ������, �������, ��������� ��������� � ���������", ProductPrice = 674 });
            //db.Products.Add(new Product { ProductName = "���������", ProductWeight = 630, ProductType = 1, ImageLink = " /img/15.jpg", ProductDescription = "�������� ����, �������, ��������� � ��������", ProductPrice = 675 });
            //db.Products.Add(new Product { ProductName = "��� �����", ProductWeight = 620, ProductType = 1, ImageLink = "/img/16.jpg", ProductDescription = "�������� ����, ��������, ��������� ���������, ��� �������, ��������� � �����", ProductPrice = 676 });
            //db.Products.Add(new Product { ProductName = "���������-�����", ProductWeight = 600, ProductType = 1, ImageLink = "/img/17.jpg", ProductDescription = "�������� (����), ������ ����, �����, ��� �������, ���������, ������ � ������ ����������������", ProductPrice = 677 });
            //db.Products.Add(new Product { ProductName = "����������� ", ProductWeight = 610, ProductType = 1, ImageLink = "/img/18.jpg", ProductDescription = "�������� ����, ��������, �������� (����), ��������� ���������, ���������, ��������� ������ � �����", ProductPrice = 678 });

        }
    }
}
