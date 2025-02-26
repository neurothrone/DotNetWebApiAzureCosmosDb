namespace DotNetWebApiAzureCosmosDb.Api.DTOs;

public class InputProjectDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public InputPersonDto Owner { get; set; }

    public List<InputSkillDto> Skill { get; set; }
}