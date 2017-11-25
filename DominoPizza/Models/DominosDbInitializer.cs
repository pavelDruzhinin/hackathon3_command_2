using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class DominosDbInitializer : DropCreateDatabaseAlways<DominosContext>
    {
        protected override void Seed(DominosContext db)
        {
            db.UserRolesDbSet.Add(new UserRoles { RoleId = 1, RoleName = "Менеджер" });
            db.UserRolesDbSet.Add(new UserRoles { RoleId = 2, RoleName = "Повар" });
            db.UserRolesDbSet.Add(new UserRoles { RoleId = 3, RoleName = "Курьер" });
            //db.ProductsDbSet.Add(new Products { ProductName = "Четыре сыра", ImageLink = "/img/1.jpg", ProductDescription = "Томатный соус, моцарелла, сыр блючиз и смесь сыров чеддар и пармезан", ProductRecipe = "рецепт 1", ProductPrice = 671 });
            //db.ProductsDbSet.Add(new Products { ProductName = "Двойная пепперони", ImageLink = "/img/2.jpg", ProductDescription = "Томатный соус, моцарелла и пикантная пепперони", ProductRecipe = "рецепт 2", ProductPrice = 672 });
            //db.ProductsDbSet.Add(new Products { ProductName = "Итальянская", ImageLink = "/img/3.jpg", ProductDescription = "Томатный соус, пикантная пепперони, шампиньоны, моцарелла, маслины и орегано", ProductRecipe = "рецепт 3", ProductPrice = 673 });
            //db.ProductsDbSet.Add(new Products { ProductName = "Ранч-пицца", ImageLink = "/img/4.jpg", ProductDescription = "Цыпленок, ветчина, соус ранч, моцарелла, томаты и чеснок", ProductRecipe = "рецепт 4", ProductPrice = 674 });
            //db.ProductsDbSet.Add(new Products { ProductName = "Мясная", ImageLink = "/img/5.jpg", ProductDescription = "Томатный соус, моцарелла, охотничьи колбаски, ветчина и бекон", ProductRecipe = "рецепт 5", ProductPrice = 675 });
            //db.ProductsDbSet.Add(new Products { ProductName = "Грибы и ветчина", ImageLink = "/img/6.jpg", ProductDescription = "Томатный соус, моцарелла, ветчина и шампиньоны", ProductRecipe = "рецепт 6", ProductPrice = 676 });
            //db.ProductsDbSet.Add(new Products { ProductName = "Пицца-пирог", ImageLink = "/img/7.jpg", ProductDescription = "Молоко сгущенное, брусника и ананасы", ProductRecipe = "рецепт 7", ProductPrice = 677 });
            //db.ProductsDbSet.Add(new Products { ProductName = "Маргарита", ImageLink = "/img/8.jpg", ProductDescription = "Томатный соус, томаты, моцарелла и орегано", ProductRecipe = "рецепт 8", ProductPrice = 678 });
            db.UsersDbSet.Add(new Users { UserRoleId = 1, UserName = "АннаМенеджер" });
            db.UsersDbSet.Add(new Users { UserRoleId = 2, UserName = "ПётрПовар" });
            db.UsersDbSet.Add(new Users { UserRoleId = 3, UserName = "ИванКурьер" });
            db.TaskCommentsDbSet.Add(new TaskComments { TaskId = 1, UserName = "ПётрПовар", CommentText = "не успею сделать в срок, последняя банка с шампиньонами оказалась испорченной" });
            db.TaskCommentsDbSet.Add(new TaskComments { TaskId = 1, UserName = "ИванКурьер", CommentText = "я пока свободен, сейчас сгоняю в магазин" });
            db.TaskCommentsDbSet.Add(new TaskComments { TaskId = 1, UserName = "АннаМенеджер", CommentText = "ребята, заказ очень важный, не подведите" });

            base.Seed(db);
        }
    }
}