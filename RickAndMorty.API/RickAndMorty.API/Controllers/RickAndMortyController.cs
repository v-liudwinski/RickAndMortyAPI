using Microsoft.AspNetCore.Mvc;
using RickAndMorty.API.Extensions;
using RickAndMorty.API.RequestForms;
using RickAndMorty.API.ResponseForms;
using RickAndMorty.BLL;
using RickAndMorty.BLL.Models;

namespace RickAndMorty.API.Controllers;

[ApiController]
[Route("api/v1/")]
public class RickAndMortyController : Controller
{
    private readonly IRickAndMortyClient _rickAndMortyClient;
    
    public RickAndMortyController(IRickAndMortyClient rickAndMortyClient, ICacheService cacheService)
    {
        _rickAndMortyClient = rickAndMortyClient;
    }

    [HttpPost]
    [Route("check-person")]
    public async Task<IActionResult> IsPersonInEpisode([FromBody] CheckPerson checkPerson)
    {
        var character = await _rickAndMortyClient.GetCharacterAsync(checkPerson.PersonName);
        var episode = await _rickAndMortyClient.GetEpisodeAsync(checkPerson.EpisodeName);
        if (character is null || episode is null) return NotFound();

        await _rickAndMortyClient.AddToCache(character.Name.ToLower(), character);
        await _rickAndMortyClient.AddToCache(episode.Name.ToLower(), character);

        var response = new CheckPersonResponse
        {
            PersonName = character.Name,
            EpisodeName = episode.Name,
            IsPersonInEpisode = _rickAndMortyClient
                .IsCharacterInEpisode(checkPerson.PersonName, checkPerson.EpisodeName).Result
        };
        return Ok(response);
    }
    
    [HttpGet]
    [Route("person")]
    public async Task<IActionResult> GetCharacterAsync(string name)
    {
        var character = await _rickAndMortyClient.GetCharacterAsync(name);
        if (character is null) return NotFound();

        await _rickAndMortyClient.AddToCache(name.ToLower(), character);
        
        var dto = character.ToResponse();
        return Ok(dto);
    }
}