using RickAndMorty.DAL.Models;
using RickAndMorty.DTO.DTOs;

namespace RickAndMorty.BLL.Extensions;

public static class CharacterExtension
{
    public static CharacterDTO ToDto(this Character character)
    {
        return new CharacterDTO
        {
            Name = character.Name,
            Status = character.Status,
            Species = character.Species,
            Type = character.Type,
            Gender = character.Gender,
            Origin = new Origin
            {
                Name = character.Origin.Name,
                Type = character.Origin.Type,
                LocationUrl = character.Origin.LocationUrl
            }
        };
    }
}