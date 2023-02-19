using FindCoachApi.Controllers.Dtos;
using FindCoachApi.Entities;
using FindCoachApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FindCoachApi.Controllers;

[ApiController]
[Route("api/requests")]
public class RequestController: ControllerBase
{
    private readonly IRequestService _requestService;
    private readonly ICoachesService _coachesService;

    public RequestController(IRequestService requestService, ICoachesService coachesService)
    {
        _requestService = requestService;
        _coachesService = coachesService;
    }

    [HttpGet("{coachId:int}")]
    public async Task<ActionResult<List<Request>>> GetRequestsByCoachId(int coachId)
    {
        if (!await _coachesService.IsCoachRegistered(coachId))
        {
            return BadRequest("This coach is not registered");
        }

        var requests = await _requestService.GetRequestsByCoachId(coachId);
        return Ok(requests);
    }

    [HttpPost]
    public async Task<ActionResult> AddRequestToCoach(RequestToAddDto requestToAdd)
    {
        if (!await _coachesService.IsCoachRegistered(requestToAdd.CoachId))
        {
            return BadRequest("This coach is not registered");
        }
        
        var request = new Request
        {
            CoachId = requestToAdd.CoachId,
            UserEmail = requestToAdd.UserEmail,
            Message = requestToAdd.Message
        };

        var savedRequest = await _requestService.AddRequest(request);
        return Ok(savedRequest);
    }
    
    
}