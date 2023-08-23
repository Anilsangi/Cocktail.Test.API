using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktail.Test.API.Models
{
    public class IngredientResponse
    {
       
        public string idIngredient { get; set; }
        public string strIngredient { get; set; }
        public string strDescription { get; set; }
        public string strType { get; set; }
        public string strAlcohol { get; set; }
        public string strABV { get; set; }
    }

    public class Ingredients
    {
        public List<IngredientResponse> ingredients { get; set; }
    }
}
