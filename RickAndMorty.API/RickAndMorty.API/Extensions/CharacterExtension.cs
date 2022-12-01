using RickAndMorty.API.DTOs;
using RickAndMorty.BLL.Models;

namespace RickAndMorty.API.Extensions;

public static class CharacterExtension
{
    public static CharacterDTO ToResponse(this Character character)
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

    public static List<CharacterDTO> ToResponseList(this List<Character> characters)
    {
        return characters.Select(character => new CharacterDTO
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
            })
            .ToList();
    }
}