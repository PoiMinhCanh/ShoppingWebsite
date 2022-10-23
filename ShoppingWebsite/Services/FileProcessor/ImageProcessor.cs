namespace ShoppingWebsite.Services.FileProcessor;

public class ImageProcessor
{
    public static string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    public static string relativeImageFolderPath = "images";

    public static async Task<string> SaveImage(IFormFile imageFile, string fileName)
    {
        // Chỉnh bên ProductCreateView thì không cần phải kiểm tra file image
        if (!imageFile.ContentType.StartsWith("image"))
        {
            return null;
        }

        // wwwroot + image
        createFolderIfNotExist(Path.Combine(rootFolderPath, relativeImageFolderPath));

        // image + file name (relative path)
        string relativeSaveFilePath = Path.Combine(relativeImageFolderPath, fileName + Path.GetExtension(imageFile.FileName));

        // wwwroot + image + file name
        using (var fileStream = new FileStream(Path.Combine(rootFolderPath, relativeSaveFilePath), FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        // image + file name (relative path)
        return relativeSaveFilePath;
    }

    public static void createFolderIfNotExist(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public static void deleteFileIfExist(string fileName)
    {
        string path = Path.Combine(rootFolderPath, fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }


}
