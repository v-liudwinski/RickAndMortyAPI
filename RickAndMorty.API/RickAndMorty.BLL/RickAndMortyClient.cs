using Newtonsoft.Json;
using RickAndMorty.DAL.Models;
using RickAndMorty.DTO.RequestForms;

namespace RickAndMorty.BLL;

public class RickAndMortyClient : IRickAndMortyClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RickAndMortyClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> IsCharacterInEpisode(CheckPerson checkPerson)
    {
        var character = await GetCharacterAsync(checkPerson.PersonName);
        var episode = await GetEpisodeAsync(checkPerson.EpisodeName); 
        if (character is null || episode is null) return false;
        return episode.Characters.Any(x => x == character.Url);
    }

    public async Task<Character?> GetCharacterAsync(string name)
    {
        var client = _httpClientFactory.CreateClient("rickAndMorty");
        var response = await client.GetAsync
            ($"{client.BaseAddress}/character/?name={name.ToLower()}");
        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        var characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(json);
        return characterResponse.Results.First();
    }

    public async Task<Episode?> GetEpisodeAsync(string name)
    {
        var client = _httpClientFactory.CreateClient("rickAndMorty");
        var response = await client.GetAsync
            ($"{client.BaseAddress}/episode/?name={name.ToLower()}");
        if (!response.IsSuccessStatusCode) return null;
        var json = await response.Content.ReadAsStringAsync();
        var episodeResponse = JsonConvert.DeserializeObject<EpisodeResponse>(json);
        return episodeResponse.Results.First();
    }
}