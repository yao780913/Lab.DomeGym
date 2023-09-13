﻿using DomeGym.Application.Gyms.Commands.CreateGym;
using DomeGym.Application.Gyms.Commands.Queries.GetGym;
using DomeGym.Application.Gyms.Commands.Queries.ListGyms;
using DomeGym.Contracts.Gyms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;

[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController : ApiController
{
    private readonly ISender _sender;

    public GymsController (ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGym (CreateGymRequest request, Guid subscriptionId)
    {
        var command = new CreateGymCommand(request.Name, subscriptionId);

        var createGymResult = await _sender.Send(command);

        return createGymResult.Match(
            gym => CreatedAtAction(
                nameof(GetGym),
                new { subscriptionId, GymId = gym.Id },
                new GymResponse(gym.Id, gym.Name)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListGyms (Guid subscriptionId)
    {
        var command = new ListGymsQuery(subscriptionId);

        var listGymsResult = await _sender.Send(command);

        return listGymsResult.Match(
            gyms => Ok(gyms.ConvertAll(gym => new GymResponse(gym.Id, gym.Name))),
            Problem);
    }

    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym (Guid subscriptionId, Guid gymId)
    {
        var command = new GetGymQuery(subscriptionId, gymId);
        var getGymResult = await _sender.Send(command);
        
        return getGymResult.Match(
            gym => Ok(new GymResponse(gym.Id, gym.Name)),
            Problem);
    }
    
}