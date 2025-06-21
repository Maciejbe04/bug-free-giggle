using Microsoft.AspNetCore.Mvc;
using WebApplication8.DTOs;
using WebApplication8.Exceptions;
using WebApplication8.Models;
using WebApplication8.DTOs;
using WebApplication8.Services;

namespace WebApplication8.Controllers;


[ApiController]
[Route("[controller]")]
public class EventController(IDbService service) : Controller
{
    

    [HttpGet]
    public async Task<IActionResult> GetAllEventsAsync()
    {
        return Ok(await service.GetAllEventsAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] CreateEventDto ewent){

        try
        {
            var created = await service.CreateNewEventAsync(ewent);
            return Created(string.Empty, created);
        }
        catch(NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}