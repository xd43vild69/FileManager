using Model;
using MetadataExtractor;
using Directory = System.IO.Directory;
using System.Text.RegularExpressions;

namespace Domain;

public class ImageManager
{
    private ImageFile? ImageFile { get; set; }
    const string pathImages = @"C:\Users\D13\Desktop\ImagesTest\";
    const string imageName = "_img";
    const string folderOutput = "folder1";
    const string path = @$"{pathImages}";
    const string path2 = @$"{pathImages}{folderOutput}\";
    

    public void GetAllImages()
    {

        
        string[] allowedExtensions = new [] {".jpeg", ".jpg", ".png"}; 
        //Read Path
        foreach (string file in System.IO.Directory.EnumerateFiles(@$"{pathImages}").Where(file => allowedExtensions.Any(file.ToLower().EndsWith)).ToList())
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
            List<string> filePaths = Directory.GetFiles(@$"{pathImages}").Where(file => searchPattern.IsMatch(file)).ToList();
            
            var counter = 1;

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            List<FileInfo> files = new List<FileInfo>();
            files.AddRange(directoryInfo.GetFiles());

            IEnumerable<FileInfo> filesSorted = files.OrderByDescending(x => x.LastWriteTime).ToList();

            if (filesSorted.Count() == 0) return;

            foreach (var f in filesSorted)
            {
                if (File.Exists(path2 + f.Name))
                    File.Delete(path2 + f.Name);

                // Move the file.
                File.Move(path + f.Name, path2 + $"{counter}{imageName}{f.Extension}");
                counter++;
            }        

        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }

}