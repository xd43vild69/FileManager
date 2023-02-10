
using DTO;
using MetadataExtractor;
using Directory = System.IO.Directory;
using System.Text.RegularExpressions;
using Repositories;

namespace Domain;

public class ImageManager : IImageManager
{
    private readonly IConfiguration Configuration;
    private readonly IRepository<Image> Repository;
    private Image? ImageFile { get; set; }
    const string imagePrefixName = "_img";
    const string folderOutput = "Output";
    private string path;
    private readonly string pathOutput;

    public ImageManager(IConfiguration configuration, IRepository<Image> repository)
    {
        Configuration = configuration;
        Repository = repository;
        path = $"{Configuration["PathFiles"]}";
        pathOutput = @$"{folderOutput}";
    }

    public void InsertDatabase()
    {
        
        DirectoryInfo directoryInfo = new DirectoryInfo(path);                
        List<string> filePaths = Directory.GetFiles(@$"{path}", "*.*", SearchOption.AllDirectories ).Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg")).ToList();

        List<FileInfo> files = new List<FileInfo>();

        files.AddRange(directoryInfo.GetFiles("*.*", SearchOption.AllDirectories)
                        .Where(f => f.Extension == ".jpeg" || f.Extension == ".jpg"));

        IEnumerable<FileInfo> filesSorted = files.OrderBy(x => x.CreationTime).ToList();

        if (filesSorted.Count() == 0) return;

        foreach (var f in filesSorted)
        {
            //Send all the images to insert into the DB
            var image = new Image()
            {
                Name = f.Name, 
                Path = f.FullName,
                Created = f.CreationTime,                              
                LastModified = f.LastAccessTime,
                Tag = "X",   
            };

            // TODO: Automapper
            Repository.Insert(image);
        }
    }

    public void MoveImages(String pathRequest)
    {
        path = pathRequest;
        MoveImages(pathRequest);
    }

    public void MoveImages()
    {

        try
        {
            var searchPattern = new Regex(@"$(?<=\.(jpg|jpeg))");
            List<string> filePaths = Directory.GetFiles(@$"{path}").Where(file => searchPattern.IsMatch(file)).ToList();

            var counter = 1;

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            List<FileInfo> files = new List<FileInfo>();
            files.AddRange(directoryInfo.GetFiles());

            IEnumerable<FileInfo> filesSorted = files.OrderBy(x => x.LastWriteTime).ToList();

            if (filesSorted.Count() == 0) return;

            int counterFilesByFolder = 1;
            int topFolder = 200;

            foreach (var f in filesSorted)
            {
                if (File.Exists($"{pathOutput}{topFolder}\\{f.Name}"))
                    File.Delete($"{pathOutput}{topFolder}\\{f.Name}");

                if (!Directory.Exists($"{path}\\{pathOutput}{topFolder}\\"))
                {
                    Directory.CreateDirectory($"{path}\\{pathOutput}{topFolder}\\");
                }

                // Move the file.
                File.Move($"{path}\\{f.Name}", $"{path}\\{pathOutput}{topFolder}\\{counter}{imagePrefixName}{f.Extension}");

                counter++;
                counterFilesByFolder++;

                if (counterFilesByFolder >= topFolder)
                {
                    topFolder = topFolder + 200;
                }

            }

        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }
    public void GetAllImages()
    {
        string[] allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
        //Read Path
        foreach (string file in System.IO.Directory.EnumerateFiles(@$"{path}").Where(file => allowedExtensions.Any(file.ToLower().EndsWith)).ToList())
        {
            //string contents = File.ReadAllText(file);
            var directories = ImageMetadataReader.ReadMetadata(file);

            foreach (var directory in directories)
            {
                foreach (var tag in directory.Tags)
                    Console.WriteLine($"[{directory.Name}] {tag.Name} = {tag.Description}");

                if (directory.HasError)
                {
                    foreach (var error in directory.Errors)
                        Console.WriteLine($"ERROR: {error}");
                }
            }
        }
    }
}