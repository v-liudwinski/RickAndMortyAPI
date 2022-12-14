using Newtonsoft.Json;
using RickAndMorty.BLL.Models;

namespace RickAndMorty.BLL;

public class RickAndMortyClient : IRickAndMortyClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICacheService _cacheService;

    public RickAndMortyClient(IHttpClientFactory httpClientFactory, ICacheService cacheService)
    {
        _httpClientFactory = httpClientFactory;
        _cacheService = cacheService;
    }

    public async Task<bool> IsCharacterInEpisode(string personName, string episodeName)
    {
        var character = await GetCharacterAsync(personName);
        var episode = await GetEpisodeAsync(episodeName); 
        if (character is null || episode is null) return default;
        return episode.Characters.Any(x => x == character.Url);
    }

    public async Task<Character?> GetCharacterAsync(string name)
    {
        var client = _httpClientFactory.CreateClient("rickAndMorty");
        var response = await client.GetAsync
            ($"{client.BaseAddress}/character/?name={name.ToLower()}");
        if (!response.IsSuccessStatusCode) return default;
        var json = await response.Content.ReadAsStringAsync();
        var characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(json);
        return characterResponse.Results.FirstOrDefault();
    }
    
    public async Task<Episode?> GetEpisodeAsync(string name)
    {
        var client = _httpClientFactory.CreateClient("rickAndMorty");
        var response = await client.GetAsync
            ($"{client.BaseAddress}/episode/?name={name.ToLower()}");
        if (!response.IsSuccessStatusCode) return default;
        var json = await response.Content.ReadAsStringAsync();
        var episodeResponse = JsonConvert.DeserializeObject<EpisodeResponse>(json);
        return episodeResponse.Results.FirstOrDefault();
    }

    public async Task AddToCache<T>(string key, T value) where T : IModel
    {
        _cacheService.AddToCache(key, value);
    }

    public async Task<T?> GetFromCache<T>(string key) where T : IModel
    {
        return _cacheService.GetCached<T>(key);
    }
}