using System.IO;

namespace Build;

public static class DirectoryUtils
{
    // Copied from `https://learn.microsoft.com/en-us/dotnet/standard/io/how-to-copy-directories`
    public static void Copy(string source, string dest)
    {
        DirectoryInfo sourceDir = new(source);

        if (!sourceDir.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory not found: {sourceDir.FullName}");
        }

        Directory.CreateDirectory(dest);

        foreach (FileInfo file in sourceDir.GetFiles())
        {
            file.CopyTo(Path.Combine(dest, file.Name), overwrite: true);
        }

        foreach (DirectoryInfo dir in sourceDir.GetDirectories())
        {
            Copy(dir.FullName, Path.Combine(dest, dir.Name));
        }
    }
}
