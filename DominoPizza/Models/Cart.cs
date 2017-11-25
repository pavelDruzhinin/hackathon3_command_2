using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Cart 
    {
        public int CartId { get; set; }
        public int Counter { get; set; }

    public Dictionary<int, int> cartlist = new Dictionary<int, int>(); //<ИД блюда в заказе, количество порций>

    //возвращаем список блюд в заказе
 //   public Dictionary<int, int> TaskList
  //  {
  //      get
  //      {
  //          return taskList;
  //      }
  //  }


    //при сборке заказа добавление пары значений <блюдо, количество>
    // при отмене позиции в заказе можно в кол-во записывать 0
    public int AddDishToCart(int productId, int amount)
    {
        //если блюдо уже есть в списке, то увеличиваем количество
        if (cartlist.ContainsKey(productId))
        {
                if (amount == 0)
                {
                    cartlist[productId] = 0;
                    cartlist.Remove(productId);
                }
                else cartlist[productId] += amount;  //
        }
        //если блюда нет, то добавляем новую пару значений
        else
        {
            cartlist.Add(productId, amount);
        }
            Counter = cartlist.Count();
        return Counter;
    }
        public int EditCartList(int productId, int amount)
        {
            //если блюдо уже есть в списке, то заменяем количество
            if (cartlist.ContainsKey(productId))
            {
                if (amount == 0)
                {
                    cartlist[productId] = 0;
                    cartlist.Remove(productId);
                }
                else cartlist[productId] = amount;  //
            }
            //если блюда нет, то добавляем новую пару значений
            else
            {
                cartlist.Add(productId, amount);
            }
            Counter = cartlist.Count();
            return Counter;
        }







    }
}