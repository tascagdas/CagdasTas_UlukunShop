using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using UlukunShopAPI.Application.Abstractions.Storage.Local;

namespace UlukunShopAPI.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{
    readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    private async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None,
                1024 * 1024, useAsync: false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception ex)
        {
            //todo log!
            throw ex;
        }
    }

    public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string path,
        IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> datas = new();

        foreach (IFormFile file in files)
        {
            // string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

            await CopyFileAsync($"{uploadPath}/{file.Name}", file);
            datas.Add((file.Name, $"{uploadPath}/{file.Name}"));
        }


        return datas;
    }

    public async Task DeleteAsync(string path, string fileName)
    {
        File.Delete($"{path}\\{fileName}");
    }

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directory = new(path);
        return directory.GetFiles().Select(f => f.Name).ToList();
    }

    public bool HasFile(string path, string fileName)
    => File.Exists($"{path}\\{fileName}");
    
}