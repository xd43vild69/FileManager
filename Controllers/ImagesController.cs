using Microsoft.AspNetCore.Mvc;
using Model;
using Domain;

namespace ManageImages.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    public IImageManager ImageManager { get; set; }
   
    public ImageController(IImageManager imageManager)
    {
        ImageManager = imageManager;
    }


    [HttpGet(Name = "GetImages")]
    public IEnumerable<ImageFile> Get()
    {
        //TODO: Dependecy injection
        var ImageFiles = new List<ImageFile>();
        ImageManager.MoveImages();
        return ImageFiles;
    }
}
