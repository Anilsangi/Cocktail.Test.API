using Cocktail.Test.API.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cocktail.Test.API
{
    public class SearchIngredientTest
    {
        [Test]
        [TestCase("vodka")]
        [TestCase("Watermelon")]
        public void SearchIngredientsTest(string strIngredient)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://www.thecocktaildb.com/api/json/v1/1/search.php?i=" + strIngredient).Result;
            Assert.IsNotNull(response);
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            Assert.IsNotNull(jsonResponse);
            var ingredient = JsonSerializer.Deserialize<Ingredients>(jsonResponse);
            Assert.IsNotNull(ingredient);

            if (ingredient.ingredients[0].strAlcohol == "No")//if ingredient is non alcohol 
            {
                Console.WriteLine("Alcohol is :" + ingredient.ingredients[0].strAlcohol);
                Console.WriteLine(("ABV is:" + ingredient.ingredients[0].strABV));
            }
            else
            {
                Console.WriteLine("Alcohol is :" + ingredient.ingredients[0].strAlcohol);
                Console.WriteLine("ABV is:" + ingredient.ingredients[0].strABV);
            }

        }

        [Test]
        public void SearchCockTailsTest()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://www.thecocktaildb.com/api/json/v1/1/search.php?s=margarita").Result;
            Assert.IsNotNull(response);
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            JObject data = JObject.Parse(jsonResponse);
            var ingredient = JsonSerializer.Deserialize<DrinksModel>(jsonResponse);
            if (ingredient != null && ingredient.drinks != null && ingredient.drinks.Length > 0)
            {

                string schemaProperties = string.Join(',', "strDrink", "strTags", "strCategory", "strAlcoholic", "strGlass", "strInstructions", "strIngredient1", "strMeasure1", "strCreativeCommonsConfirmed", "dateModified");

                foreach (string property in schemaProperties.Split(','))
                {
                    for (int i = 0; i <= ingredient.drinks.Length - 1; i++)
                    {
                        Drink drink = ingredient.drinks[i];
                        Assert.IsNotNull(drink.GetType().GetProperty(property)); // Checks if Property is available or not
                        //Assert.IsNotNull(drink.GetType().GetProperty(property).GetValue(drink));//Checks if Property Value is not null, if nulls it fails test method.
                    }
                    Console.WriteLine(property);
                }

            }

        }


    }
}
