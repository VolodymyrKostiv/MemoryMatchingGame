using MemoryMatchingGame.WPF.Services.Interfaces;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MemoryMatchingGame.WPF.Services.Implementations;

public class ImageCacheService : IImageCacheService
{ 
    private readonly string _cacheDir;

    public ImageCacheService()
    {
        _cacheDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MyApp", "ImageCache");
        Directory.CreateDirectory(_cacheDir);
    }

    public string GetCachePath(string originalPath)
    {
        var hash = Convert.ToHexStringLower(
            MD5.HashData(System.Text.Encoding.UTF8.GetBytes(originalPath)));

        return Path.Combine(_cacheDir, $"{hash}.jpg");
    }

    public async Task<BitmapImage> LoadAsync(string originalPath)
    {
        var cachedPath = GetCachePath(originalPath);

        if (!File.Exists(cachedPath))
        {
            await CreateThumbnailAsync(originalPath, cachedPath);
        }

        var image = new BitmapImage();
        image.BeginInit();
        image.UriSource = new Uri(cachedPath);
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.EndInit();
        image.Freeze();

        return image;
    }

    private async Task CreateThumbnailAsync(string sourcePath, string targetPath, int maxWidth = 256)
    {
        await Task.Run(() =>
        {
            var bitmap = new BitmapImage(new Uri(sourcePath, UriKind.RelativeOrAbsolute));

            double scale = maxWidth / bitmap.PixelWidth;

            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(
                new TransformedBitmap(bitmap, new System.Windows.Media.ScaleTransform(scale, scale))));

            using var fs = new FileStream(targetPath, FileMode.Create);
            encoder.Save(fs);
        });
    }
}
