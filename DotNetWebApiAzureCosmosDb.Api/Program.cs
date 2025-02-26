using DotNetWebApiAzureCosmosDb.Api.Data;
using DotNetWebApiAzureCosmosDb.Api.DTOs;
using DotNetWebApiAzureCosmosDb.Api.Extensions;
using DotNetWebApiAzureCosmosDb.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CosmosRepository>(_ =>
    new CosmosRepository(
        // connectionString: builder.Configuration.GetConnectionString("DockerConnection") ??
        connectionString: builder.Configuration.GetConnectionString("AzureConnection") ??
                          throw new Exception("Connection string not found"),
        databaseName: "ProjectsDB"
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/projects", async (InputProjectDto project, CosmosRepository repo) =>
{
    var createdProject = await repo.AddAsync("Projects", project.ToModel());
    return createdProject is null
        ? Results.BadRequest("Failed to add project")
        : Results.Created($"/api/projects/{createdProject.Id}", createdProject);
});

app.MapGet("/api/projects", async (CosmosRepository repo) =>
{
    var projects = await repo.GetAllAsync<Project>("Projects");
    return Results.Ok(projects);
});

app.MapGet("/api/projects/{id:length(24)}", async (string id, CosmosRepository repo) =>
{
    var project = await repo.GetByIdAsync<Project>("Projects", id);
    return project is not null ? Results.Ok(project) : Results.NotFound();
});

app.MapPut("/api/projects/{id:length(24)}", async (string id, InputProjectDto project, CosmosRepository repo) =>
{
    var updatedProject = await repo.UpdateAsync("Projects", id, project.ToModel(id: id));
    return updatedProject is not null ? Results.Ok(updatedProject) : Results.NotFound();
});

app.MapDelete("/api/projects/{id:length(24)}", async (string id, CosmosRepository repo) =>
{
    var deleted = await repo.DeleteAsync<Project>("Projects", id);
    return deleted ? Results.NoContent() : Results.NotFound();
});

app.Run();