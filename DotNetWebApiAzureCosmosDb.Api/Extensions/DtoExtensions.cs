using DotNetWebApiAzureCosmosDb.Api.DTOs;
using DotNetWebApiAzureCosmosDb.Api.Models;

namespace DotNetWebApiAzureCosmosDb.Api.Extensions;

public static class DtoExtensions
{
    public static Project ToModel(this InputProjectDto dto, string? id = null)
    {
        return new Project
        {
            Id = id ?? string.Empty,
            Title = dto.Title,
            Description = dto.Description,
            Owner = new Person
            {
                Name = dto.Owner.Name,
                Age = dto.Owner.Age,
                Address = new Address
                {
                    Street = dto.Owner.Address.Street,
                    City = dto.Owner.Address.City,
                    ZipCode = dto.Owner.Address.ZipCode,
                    State = dto.Owner.Address.State
                }
            },
            Skill = dto.Skill
                .Select(s => new Skill
                {
                    Name = s.Name,
                    ExperienceInYears = s.ExperienceInYears,
                    Proficiency = s.Proficiency
                })
                .ToList()
        };
    }
}