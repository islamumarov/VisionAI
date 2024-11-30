using System.Text.Json;
using Microsoft.Extensions.AI;
using VisionAI.Model;

namespace VisionAI.Service;

public class ImageHandlerService(IChatClient chatClient) : IImageHandlerService
{
    public async Task<string?> HandleImageAsync(IFormFile image)
    {
        var imageInBase64 = await ConvertToBase64(image);

        var message = new Message
        {
            Role = "User",
            Content = "What is in this image?",
            Image = imageInBase64
        };
        Message[] messages = [message];

        var response = await chatClient.CompleteAsync(JsonSerializer.Serialize(messages));
        return response.Message.Text;
    }

    private async Task<string> ConvertToBase64(IFormFile image)
    {
        using var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);
        var base64 = Convert.ToBase64String(memoryStream.ToArray());

        return base64;
    }
}
