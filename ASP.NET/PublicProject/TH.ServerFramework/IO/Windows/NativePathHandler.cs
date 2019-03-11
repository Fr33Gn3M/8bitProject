namespace TH.ServerFramework.IO.Windows
{
    using TH.ServerFramework.IO;
    using System;
    using System.IO;

    internal class NativePathHandler : IPathHandler
    {
        public string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public string GetDirectoryPath(string fileSystemItemPath)
        {
            return Path.GetDirectoryName(fileSystemItemPath);
        }

        public string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        public string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public string GetFileSystemItemName(string fullPath)
        {
            return Path.GetFileName(fullPath);
        }

        public static string GetRelatedPath(DirectoryInfo rootFolder, FileSystemInfo fileItem)
        {
            if (rootFolder.FullName == fileItem.FullName)
            {
                return @"\";
            }
            if (!fileItem.FullName.StartsWith(rootFolder.FullName))
            {
                throw new ArgumentException("fileItem");
            }
            string relatedPath = fileItem.FullName.Substring(rootFolder.FullName.Length).Trim(new char[] { '\\' });
            return (@"\" + relatedPath);
        }

        public string[] Split(string fullPath)
        {
            return fullPath.Split(@"\");
        }
    }
}

