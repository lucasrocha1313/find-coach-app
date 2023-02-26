using FindCoachApi.Entities;
using FindCoachApi.Helpers;
using FindCoachApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FindCoachApi.Controllers;

[ApiController]
[Route("api/coaches")]
public class CoachesController: ControllerBase
{
    private readonly ICoachesService _coachesService;

    public CoachesController(ICoachesService coachesService)
    {
        _coachesService = coachesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Coach>>> GetCoaches()
    {
        var coaches = await _coachesService.GetCoaches();
        return Ok(coaches);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<List<Coach>>> GetCoachById(int id)
    {
        var coach = await _coachesService.GetCoachById(id);
        return Ok(coach);
    }
}