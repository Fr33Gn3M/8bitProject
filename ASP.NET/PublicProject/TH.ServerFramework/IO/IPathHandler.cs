namespace TH.ServerFramework.IO
{
    using System;

    public interface IPathHandler
    {
        string Combine(params string[] paths);
        string GetDirectoryPath(string fileSystemItemPath);
        string GetFileExtension(string filePath);
        string GetFileNameWithoutExtension(string filePath);
        string GetFileSystemItemName(string fullPath);
        string[] Split(string fullPath);
    }
}

