using Microsoft.AspNetCore.Mvc;
using WebApplication8.Exceptions;
using WebApplication8.Services;

namespace WebApplication8.Controllers;

[ApiController]
[Route("[controller]")]
public class PrelegentController(IDbService service) : Controller
{

    [HttpPost("{prelegentId}/event/{eventId}")]
    public async Task<IActionResult> RegisterPrelegentToEventAsync([FromRoute]int prelegentId, [FromRoute]int eventId)
    {
        try
        {
            await service.RegisterPrelegentToEventAsync(prelegentId, eventId);
            return Created();
        }
        catch (NotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}