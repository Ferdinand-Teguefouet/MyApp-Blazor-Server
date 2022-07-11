using Microsoft.AspNetCore.Components;
using MyApp.Models;
using MyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Pages
{
    public class PizzasBase : ComponentBase
    {
        [Inject]
        private IPizzaManager pizzaManager { get; set; } // private pour empêcher la vue d'avoir accès à notre service

        protected bool IsAdmin;
        protected List<Pizza> Basket;
        protected List<Pizza> MyPizzas;
        protected Pizza EditingPizza;

        protected override async Task OnInitializedAsync()
        {
            Basket = new List<Pizza>();
            MyPizzas = (await pizzaManager.GetPizza()).ToList(); // cette ligne justifie l'utilisation de OnInitializedAsync()

            EditingPizza = null;
            IsAdmin = false;
        }

        protected string Ingredients // Propriété proxy
        {
            get
            {
                return EditingPizza != null ? string.Join(separator: ", ", EditingPizza.Ingredients) : null;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    EditingPizza.Ingredients = value.Split(separator: ',').Select(v => v.Trim()).ToArray();
                }
                else
                {
                    EditingPizza.Ingredients = null;
                }
            }
        }

        public void AddToBasket(Pizza pizza) => Basket.Add(pizza);
        public void RemoveFromBasket(Pizza pizza) => Basket.Remove(pizza);

        public void EditPizza( Pizza p)
        {
            //EditingPizza = p; // Il s'agit d'une affectation de référence (les deux pointeurs sont sur la même zone mémoire. Donc en modifiant un on modifie forcément l'autre
            
            // Ici, on a cloné la pizza sur laquelle on a cliqué pour demander l'édition
            EditingPizza = new Pizza
            {
                Id = p.Id,
                Name = p.Name,
                ImageName = p.ImageName,
                Ingredients = p.Ingredients,
                Price = p.Price
            };
        }
        public async Task Close()
        {
            await pizzaManager.AddOrUpdate(EditingPizza);
            var pizza = MyPizzas.Find(p => p.Id == EditingPizza.Id);
            pizza.Price = EditingPizza.Price;
            pizza.Name = EditingPizza.Name;
            pizza.Ingredients = EditingPizza.Ingredients;
            EditingPizza = null;
        }
    }
}
