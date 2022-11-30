using RickAndMorty.BLL.Models;

namespace RickAndMorty.BLL;

public interface IRickAndMortyClient
{
    Task<bool> IsCharacterInEpisode(string personName, string episodeName);
    Task<Character?> GetCharacterAsync(string name);
    Task<Episode?> GetEpisodeAsync(string name);
}