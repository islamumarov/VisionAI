namespace VisionAI.Service;

internal interface IImageHandlerService
{
    Task<string?> HandleImageAsync(IFormFile image);
}