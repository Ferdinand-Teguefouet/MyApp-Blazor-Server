using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface IPizzaManager
    {
        Task<IEnumerable<Pizza>> GetPizza();
        Task AddOrUpdate(Pizza p);
    }
}
