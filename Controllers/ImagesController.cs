using Microsoft.AspNetCore.Mvc;
using DTO;
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

    // [HttpPost("MoveImages")]
    // public IEnumerable<Image> Post()
    // {
    //     var images = new List<Image>();
    //     ImageManager.MoveImages();
    //     return images;
    // }

    // [HttpPost("InsertDatabase")]
    // public void Post()
    // {        
    //     ImageManager.InsertDatabase();     
    // }

    [HttpPost("SortExifFiles")]
    public void Post()
    {        
        ImageManager.SortingExifFiles();     
    }

    [HttpGet(Name = "GetImages")]
    public IEnumerable<Image> Get()
    {
        var images = new List<Image>();
        ImageManager.MoveImages();
        return images;
    }
}
