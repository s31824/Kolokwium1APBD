using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;


[Route("api/[controller]")]
[ApiController]
public class VisitsController : ControllerBase
{
    private readonly IVisitsService _visitsService;
    
    public VisitsController(IVisitsService _visitsService)
    {
        _visitsService = _visitsService;
    }
    
    
    [HttpGet("{id}/trips")]
    public async Task<IActionResult> GetClientTrips(int id)
    {
        var trips = await _visitsService.GetClientsVisits(id);
        return Ok(trips);
    }
}