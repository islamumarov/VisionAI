using Microsoft.Extensions.AI;
using Microsoft.KernelMemory;
using VisionAI.Service;

namespace VisionAI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddScoped<IImageHandlerService, ImageHandlerService>();

        var endpoint = builder.Configuration["AI:Ollama:Chat:Endpoint"];
        builder.Services.AddChatClient(client => new OllamaChatClient(
            new Uri(endpoint),
            modelId: builder.Configuration["AI:Ollama:Chat:ModelId"]
        ));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
