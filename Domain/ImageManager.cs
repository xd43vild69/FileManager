using Model;
using MetadataExtractor;
using Directory = System.IO.Directory;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Configuration;

namespace Domain;

public class ImageManager: IImageManager
{
    private readonly IConfiguration Configuration;
    private ImageFile? ImageFile { get; set; }
    const string imagePrefixName = "_img";
    const string folderOutput = "Output";
    private readonly string path; 
    private readonly string pathOutput;

    public ImageManager(IConfiguration configuration)
    {
        Configuration = configuration;    
        path = $"{Configuration["PathFiles"]}";;
        pathOutput = @$"{folderOutput}\";
    }

    public void GetAllImages()
    {        
        string[] allowedExtensions = new [] {".jpeg", ".jpg", ".png"}; 
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

    public void MoveImages()
    {

        try
        {
            var searchPattern = new Regex(@"$(?<=\.(jpg|jpeg))");
            
            string[] allowedExtensions = new [] {".jpeg", ".jpg"}; 
            //string[] filePaths = Directory.GetFiles(path, "*.jpg");
            List<string> filePaths = Directory.GetFiles(@$"{path}").Where(file => searchPattern.IsMatch(file)).ToList();
            
            var counter = 1;

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            List<FileInfo> files = new List<FileInfo>();
            files.AddRange(directoryInfo.GetFiles());

            IEnumerable<FileInfo> filesSorted = files.OrderBy(x => x.LastWriteTime).ToList();

            if (filesSorted.Count() == 0) return;

            foreach (var f in filesSorted)
            {
                if (File.Exists(pathOutput + f.Name))
                    File.Delete(pathOutput + f.Name);

                // Move the file.
                File.Move($"{path}\\{f.Name}", $"{path}\\{pathOutput}{counter}{imagePrefixName}{f.Extension}");
                counter++;
            }        

        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }



}