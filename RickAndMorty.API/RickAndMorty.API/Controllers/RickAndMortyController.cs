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
    private readonly ICacheService _cacheService;
    
    public RickAndMortyController(IRickAndMortyClient rickAndMortyClient, ICacheService cacheService)
    {
        _rickAndMortyClient = rickAndMortyClient;
        _cacheService = cacheService;
    }

    [HttpPost]
    [Route("check-person")]
    public async Task<IActionResult> IsPersonInEpisode([FromBody] CheckPerson checkPerson)
    {
        var character = await _rickAndMortyClient.GetCharacterAsync(checkPerson.PersonName);
        var episode = await _rickAndMortyClient.GetEpisodeAsync(checkPerson.EpisodeName);
        if (character is null || episode is null) return NotFound();
        
        _cacheService.AddToCache(/*$"{*/character.Name.ToLower()/*}-{Guid.NewGuid()}"*/, character);
        
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
        
        _cacheService.AddToCache(/*$"{*/name.ToLower()/*}-{Guid.NewGuid()}"*/, character);
        
        var dto = character.ToResponse();
        return Ok(dto);
    }
    
    /*[HttpGet]
    [Route("most-popular")]
    public IActionResult GetMostPopularCharacters()
    {
        var characters = _cacheService.GetTheMostPopular<Character>();
        var charactersDto = characters.ToResponseList();
        return Ok(charactersDto);
    }*/
}