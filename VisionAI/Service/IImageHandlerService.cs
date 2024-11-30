namespace VisionAI.Service;

public interface IImageHandlerService
{
    Task<string?> HandleImageAsync(IFormFile image);
}