namespace DTO;

public class Image
{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? Created { get; set; }
        public string? Tag { get; set; }
}