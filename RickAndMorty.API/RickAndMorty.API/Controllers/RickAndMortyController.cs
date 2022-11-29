using Microsoft.AspNetCore.Mvc;
using RickAndMorty.API.ResponseForms;
using RickAndMorty.BLL;
using RickAndMorty.BLL.Extensions;
using RickAndMorty.DTO.RequestForms;

namespace RickAndMorty.API.Controllers;

[ApiController]
[Route("api/v1/")]
public class RickAndMortyController : Controller
{
    private readonly IRickAndMortyClient _rickAndMortyService;

    public RickAndMortyController(IRickAndMortyClient rickAndMortyService)
    {
        _rickAndMortyService = rickAndMortyService;
    }

    [HttpPost]
    [Route("check-person")]
    public async Task<IActionResult> IsPersonInEpisode(CheckPerson checkPerson)
    {
        var character = await _rickAndMortyService.GetCharacterAsync(checkPerson.PersonName);
        var episode = await _rickAndMortyService.GetEpisodeAsync(checkPerson.EpisodeName);
        if (character is null || episode is null) return NotFound();
        var response = new CheckPersonResponse
        {
            PersonName = character.Name,
            EpisodeName = episode.Name,
            IsPersonInEpisode = _rickAndMortyService.IsCharacterInEpisode(checkPerson).Result
        };
        return Ok(response);
    }
    
    [HttpGet]
    [Route("person")]
    public async Task<IActionResult> GetCharacterAsync(string name)
    {
        var character = await _rickAndMortyService.GetCharacterAsync(name);
        if (character is null) return NotFound();
        var dto = character.ToDto();
        return Ok(character);
    }
}