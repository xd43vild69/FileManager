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

    // [HttpPost("MoveImagesPath")]
    // public IEnumerable<ImageFile> Post(String path)
    // {
    //     var ImageFiles = new List<ImageFile>();
    //     ImageManager.MoveImages(path);
    //     return ImageFiles;
    // }

    [HttpPost("MoveImages")]
    public IEnumerable<ImageFile> Post()
    {
        var ImageFiles = new List<ImageFile>();
        ImageManager.MoveImages();
        return ImageFiles;
    }

    [HttpPost("InsertDatabase")]
    public void Post(String pathImages)
    {        
        ImageManager.InsertDatabase(pathImages);     
    }

    [HttpGet(Name = "GetImages")]
    public IEnumerable<ImageFile> Get()
    {
        var ImageFiles = new List<ImageFile>();
        ImageManager.MoveImages();
        return ImageFiles;
    }
}
