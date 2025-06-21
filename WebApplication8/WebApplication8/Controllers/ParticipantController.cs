using System.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication8.Exceptions;
using WebApplication8.Services;

namespace WebApplication8.Controllers;



[ApiController]
[Route("[controller]")]
public class ParticipantController(IDbService service) : Controller
{
    [HttpPost("{participantId}/event/{eventId}")]
    public async Task<IActionResult> RegisterParticipantToEventAsync([FromRoute]int participantId, [FromRoute]int eventId)
    {
        try
        {
            await service.RegisterParticipantToEventAsync(participantId, eventId);
            return Created();
        }
        catch (NotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete("{participantId}")]
    public async Task<IActionResult> DeleteRegistrationAsync(int participantId)
    {
        try
        {
            await service.DeleteRegistrationAsync(participantId);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}