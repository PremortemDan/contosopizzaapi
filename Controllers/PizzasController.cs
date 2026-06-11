using Microsoft.AspNetCore.Mvc;
using Contoso.Models;
using Contoso.Services;

namespace Contoso.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzasController : ControllerBase
{
    private readonly IPizzaService _service;

    public PizzasController(IPizzaService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Pizza>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = _service.Get(id);
        if (pizza == null) return NotFound();
        return Ok(pizza);
    }

    [HttpPost]
    public ActionResult<Pizza> Create(Pizza pizza)
    {
        var created = _service.Create(pizza);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id && pizza.Id != 0)
        {
            return BadRequest("ID mismatch");
        }

        var ok = _service.Update(id, pizza);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var ok = _service.Delete(id);
        if (!ok) return NotFound();
        return NoContent();
    }
}
