using Contoso.Models;

namespace Contoso.Services;

public interface IPizzaService
{
    IEnumerable<Pizza> GetAll();
    Pizza? Get(int id);
    Pizza Create(Pizza pizza);
    bool Update(int id, Pizza pizza);
    bool Delete(int id);
}
