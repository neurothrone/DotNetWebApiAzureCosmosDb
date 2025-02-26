namespace DotNetWebApiAzureCosmosDb.Api.DTOs;

public class InputPersonDto
{
    public string Name { get; set; }
    public int Age { get; set; }
    public InputAddressDto Address { get; set; }
}