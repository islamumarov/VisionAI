using Microsoft.AspNetCore.Mvc;
using VisionAI.Service;

namespace VisionAI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController(IImageHandlerService imageHandlerService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm] IFormFile image)
    {
        var response = await imageHandlerService.HandleImageAsync(image);
        return Ok(response);
    }
}
