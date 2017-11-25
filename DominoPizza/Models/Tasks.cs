using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Tasks
    {
        public int TasksId { get; set; }
        int taskStatusId;
        public int TaskStatusId { get; set; } //1:"Заказ"; 2:"Приготовление"; 3:"Доставка"; 4:"Завершён";
        private Dictionary<int, DateTime> taskStatusChange = new Dictionary<int, DateTime>();//<ИД пользователя сменившего статус, время>
        public Dictionary<int, int> taskList = new Dictionary<int, int>(); //<ИД блюда в заказе, количество порций>
        public string TaskDeliveryCustomerAddress { get; set; }
        public string TaskDeliveryCustomerPhone { get; set; }
        public string TaskDeliveryCustomerName { get; set; }
        public DateTime TaskDeliveryDateTime { get; set; }
        // способ оплаты: предоплата или при получении
        public string TaskPaymentMethod { get; set; }  

        //возвращаем список блюд в заказе
        public Dictionary<int, int> TaskList
        {
            get
            {
                return taskList;
            }
        }

        //возвращаем строку адрес, имя, телефон клиента и время доставки
        public string TaskDeliveryFullInfo()
        {
            return $"{TaskDeliveryCustomerAddress}; {TaskDeliveryCustomerName}; {TaskDeliveryCustomerPhone}; {TaskDeliveryDateTime}";
        }

        //при сборке заказа добавление пары значений <блюдо, количество>
        // при отмене позиции в заказе можно в кол-во записывать 0
        public int AddDishToTaskList(int productId, int amount)
        {
            //если блюдо уже есть в списке, то увеличиваем количество
            if (taskList.ContainsKey(productId))
             {
                    taskList[productId] += amount;  //??? будет ли это работать ???
             }
            //если блюда нет, то добавляем новую пару значений
            else
            {
                taskList.Add(productId, amount);
            }
            return taskList.Count();
        }
        //изменяем статус заказа и запоминаем, кто сменил и время
        public int TaskStatusChange(int newTaskStatus, int userId, DateTime dateTime)
        {
            taskStatusId = newTaskStatus;
            taskStatusChange.Add(userId, dateTime);
            return taskStatusId;
        }
        //возвращаем список изменений статуса
        public Dictionary<int, DateTime> TaskStatusChangeHistory
        {
            get
            {
                return taskStatusChange;
            }
        }


        //возвращает строку с удобочитаемым статусом заказа
        public string TaskStatusName 
        {
            get
            {
                string taskStatusName = "";
                switch (taskStatusId)
                {
                    case 1:
                        taskStatusName = "Заказ";
                        break;
                    case 2:
                        taskStatusName = "Приготовление";
                        break;
                    case 3:
                        taskStatusName = "Доставка";
                        break;
                    case 4:
                        taskStatusName = "Завершён";
                        break;
                }
                return taskStatusName;
            }

        }
    }
}