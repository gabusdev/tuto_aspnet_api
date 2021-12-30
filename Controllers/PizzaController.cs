using api_test.Models;
using api_test.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_test.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaService _pizzaService;
    public PizzaController(PizzaService pizzaService) => 
        _pizzaService = pizzaService;

    [HttpGet]
    public async Task<List<Pizza>> GetAll() =>
        await _pizzaService.GetAll();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Pizza>> Get(string id)
    {
        var pizza = await _pizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        return pizza;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pizza pizza)
    {
        await _pizzaService.Add(pizza);
        return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Pizza pizza)
    {
        // if (id != pizza.Id)
        //     return BadRequest();

        var existingPizza = await _pizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();
        pizza.Id = existingPizza.Id;
        await _pizzaService.Update(pizza);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var pizza = await _pizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        await _pizzaService.Delete(id);

        return NoContent();
    }
}