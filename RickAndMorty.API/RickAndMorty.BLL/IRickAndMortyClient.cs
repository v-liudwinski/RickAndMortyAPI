using RickAndMorty.BLL.Models;

namespace RickAndMorty.BLL;

public interface IRickAndMortyClient
{
    Task<bool> IsCharacterInEpisode(string personName, string episodeName);
    Task<Character?> GetCharacterAsync(string name);
    Task<Episode?> GetEpisodeAsync(string name);
    Task AddToCache<T>(string key, T value) where T: IModel;
    Task<T?> GetFromCache<T>(string key) where T: IModel;
}