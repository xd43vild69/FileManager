using Model;
using MetadataExtractor;
using Directory = System.IO.Directory;

namespace Domain;

public class ImageManager
{
    private ImageFile? ImageFile { get; set; }
    const string pathImages = @"C:\Users\D13\Desktop\ImagesTest\";
    const string imageName = "img";
    const string folderOutput = "folder1";

    public void GetAllImages()
    {
        //Read Path
        foreach (string file in System.IO.Directory.EnumerateFiles(@$"{pathImages}", "*.jpg"))
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
        string path = @$"{pathImages}";
        string path2 = @$"{pathImages}{folderOutput}\";
        try
        {

            string[] filePaths = Directory.GetFiles(path, "*.jpg");
            var counter = 1;

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            List<FileInfo> files = new List<FileInfo>();
            files.AddRange(directoryInfo.GetFiles());

            IEnumerable<FileInfo> filesSorted = files.OrderByDescending(x => x.LastWriteTime).ToList();

            foreach (var f in filesSorted)
            {
                if (File.Exists(path2 + f.Name))
                    File.Delete(path2 + f.Name);

                // Move the file.
                File.Move(path + f.Name, path2 + $"{imageName}{counter}.jpg");
                counter++;
            }        

        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }

}