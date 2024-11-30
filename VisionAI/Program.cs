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

        builder.Services.AddChatClient(client => new OllamaChatClient(
            new Uri(builder.Configuration["AI:Ollama:Chat:ModelId"]),
            modelId: builder.Configuration["AI:Ollama:Chat:Endpoint"]
        ));

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

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
