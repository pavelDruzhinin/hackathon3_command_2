using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosPizza.Models
{
    public class Ingredients
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public double IngredientWeight { get; set; }
    }
}