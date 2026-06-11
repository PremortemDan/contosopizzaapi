using Contoso.Models;

namespace Contoso.Services;

public class PizzaService : IPizzaService
{
    private readonly List<Pizza> _pizzas = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public PizzaService()
    {
        // Seed with some sample data
        Create(new Pizza { Name = "Margherita", Description = "Tomato, mozzarella, basil", Price = 6.50m });
        Create(new Pizza { Name = "Pepperoni", Description = "Pepperoni and cheese", Price = 8.00m });
    }

    public IEnumerable<Pizza> GetAll()
    {
        lock (_lock)
        {
            return _pizzas.Select(p => p).ToList();
        }
    }

    public Pizza? Get(int id)
    {
        lock (_lock)
        {
            return _pizzas.FirstOrDefault(p => p.Id == id);
        }
    }

    public Pizza Create(Pizza pizza)
    {
        lock (_lock)
        {
            pizza.Id = _nextId++;
            _pizzas.Add(pizza);
            return pizza;
        }
    }

    public bool Update(int id, Pizza pizza)
    {
        lock (_lock)
        {
            var existing = _pizzas.FirstOrDefault(p => p.Id == id);
            if (existing == null) return false;
            existing.Name = pizza.Name;
            existing.Description = pizza.Description;
            existing.Price = pizza.Price;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var existing = _pizzas.FirstOrDefault(p => p.Id == id);
            if (existing == null) return false;
            _pizzas.Remove(existing);
            return true;
        }
    }
}
