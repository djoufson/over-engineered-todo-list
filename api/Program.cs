using api.Data;
using api.Extensions;
using api.Features.Demo;
using api.Features.Tags;
using api.Features.Todos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddSqlite<TodoDbContext>("Data Source=./app.db;");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.MigrateDatabaseAsync();

app.UseCors();

app.UseHttpsRedirection();

app
    .MapGroup("todos")
    .WithTags("Todos")
    .MapTodoEndpoints();

app
    .MapGroup("tags")
    .WithTags("Tags")
    .MapTagsEndpoints();

app
    .MapGroup("demo")
    .WithTags("Demo")
    .MapDemoEndpoints();

app.Run();
