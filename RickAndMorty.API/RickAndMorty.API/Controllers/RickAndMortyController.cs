using Microsoft.AspNetCore.Mvc;
using RickAndMorty.API.Extensions;
using RickAndMorty.API.RequestForms;
using RickAndMorty.API.ResponseForms;
using RickAndMorty.BLL;

namespace RickAndMorty.API.Controllers;

[ApiController]
[Route("api/v1/")]
public class RickAndMortyController : Controller
{
    private readonly IRickAndMortyClient _rickAndMortyClient;
    
    public RickAndMortyController(IRickAndMortyClient rickAndMortyClient)
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
        var dto = character.ToResponse();
        return Ok(dto);
    }
}