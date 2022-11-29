﻿using RickAndMorty.DAL.Models;

namespace RickAndMorty.DTO.DTOs;

public class CharacterDTO
{
    public string Name { get; set; }
    public string Status { get; set; }
    public string Species { get; set; }
    public string Type { get; set; }
    public string Gender { get; set; }
    public Origin Origin { get; set; }
}