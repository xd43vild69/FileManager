using Microsoft.AspNetCore.Mvc;
using Model;
using Domain;

namespace ManageImages.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
   

    [HttpGet(Name = "GetImages")]
    public IEnumerable<ImageFile> Get()
    {
        //TODO: Dependecy injection

        var FileManager = new ImageManager();
        var ImageFiles = new List<ImageFile>();
        FileManager.MoveImages();
        return ImageFiles;
    }
}
