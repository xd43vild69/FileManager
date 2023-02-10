namespace Domain;

public interface IImageManager{
    public void GetAllImages();
    public void MoveImages();
    public void MoveImages(String path);    
    public void InsertDatabase();
}