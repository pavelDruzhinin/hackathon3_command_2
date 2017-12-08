using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models 
{
    public class TaskStatusChange
    {
        public int TaskStatusChangeId { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime In { get; set; }    /*время принятия заказа в работу сотрудником*/
        public DateTime Out { get; set; }   /*время окончания работы сотрудника == смена статуса заказа == 
                                               времени принятия заказа из инет-магазина*/

    }
}