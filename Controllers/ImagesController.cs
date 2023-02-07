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

    [HttpPost(Name = "MoveImages")]
    public IEnumerable<ImageFile> Post()
    {
        var ImageFiles = new List<ImageFile>();
        ImageManager.MoveImages();
        return ImageFiles;
    }


    [HttpGet(Name = "GetImages")]
    public IEnumerable<ImageFile> Get()
    {
        var ImageFiles = new List<ImageFile>();
        ImageManager.MoveImages();
        return ImageFiles;
    }
}
