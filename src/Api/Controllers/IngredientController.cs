using Cocktail.Application.Handlers.Commands;
using Cocktail.Application.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cocktail.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class IngredientController(ISender mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await mediator.Send(new IngredientQuery()));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(IngredientCreateCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    [HttpPut]
    public async Task<IActionResult> Update(IngredientUpdateCommand request)
    {
        return Ok(await mediator.Send(request));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Create(Guid id)
    {
        await mediator.Send(new IngredientDeleteCommand(id));
        return NoContent();
    }
}