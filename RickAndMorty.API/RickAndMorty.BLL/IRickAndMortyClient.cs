using RickAndMorty.DAL.Models;
using RickAndMorty.DTO.RequestForms;

namespace RickAndMorty.BLL;

public interface IRickAndMortyClient
{
    Task<bool> IsCharacterInEpisode(CheckPerson checkPerson);
    Task<Character?> GetCharacterAsync(string name);
    Task<Episode?> GetEpisodeAsync(string name);
}