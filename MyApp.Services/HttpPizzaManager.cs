using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class HttpPizzaManager : IPizzaManager
    {
        private readonly HttpClient _client;
        public HttpPizzaManager(HttpClient client)
        {
            this._client = client;
        }
        public Task AddOrUpdate(Pizza p)
        {
           return _client.PostAsJsonAsync("api/pizzas", p);
        }

        public Task<IEnumerable<Pizza>> GetPizza()
        {
            return _client.GetFromJsonAsync<IEnumerable<Pizza>>("api/pizzas");
        }
    }
}
