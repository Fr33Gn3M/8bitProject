namespace TH.ServerFramework.Utility
{
    using Microsoft.VisualBasic;
    using ServerFramework.IO;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class IOExtensions
    {
        public static void LoadFromLocal(this IFile source, string filePath)
        {
            if (source.Exists)
            {
                source.Delete();
            }
            using (var fs1 = File.OpenRead(filePath))
            {
                using (var fs2 = source.Open(FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    fs1.CopyTo(fs2);
                    fs2.Flush();
                }
            }
        }

        public static void SaveToLocal(this IFile source, string filePath)
        {
            using (var fs1 = source.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var fs2 = File.Create(filePath))
                {
                    fs1.CopyTo(fs2);
                    fs2.Flush();
                }
            }
        }

        public static void LoadFromLocal(this IDirectory source, string folderPath)
        {
            if (!source.Exists)
            {
                source.Create();
            }
            dynamic files = Directory.GetFiles(folderPath);
            foreach (var filePath in files)
            {
                var file = source.GetFile(Path.GetFileName(filePath));
                file.LoadFromLocal(filePath);
            }
            dynamic dirs = Directory.GetDirectories(folderPath);
            foreach (var dirPath in dirs)
            {
               var folder = source.GetDirectory(Path.GetFileName(dirPath));
                folder.LoadFromLocal(dirPath);
            }
        }

        public static void SaveToLocal(this IDirectory source, string folderPath)
        {
            folderPath = Path.Combine(folderPath, source.Name);
            Directory.CreateDirectory(folderPath);
            foreach (var folder in source.GetDirectories())
            {
                SaveToLocal(folder, folderPath);
            }
            foreach (var f in source.GetFiles())
            {
                dynamic filePath = Path.Combine(folderPath, f.Name);
                SaveToLocal(f, filePath);
            }
        }

        public static void WriteAllBytes(this IFile source, byte[] bytes)
        {
            using (var s = source.Open(FileMode.Open, FileAccess.Write, FileShare.Write))
            {
                using (var ms = new MemoryStream(bytes))
                {
                    ms.CopyTo(s);
                }
            }
        }

        public static byte[] ReadAllBytes(this IFile source)
        {
            using (var s = source.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var ms = new MemoryStream())
                {
                    s.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        public static string ReadAllText(this IFile source, Encoding encoding = null)
        {
            using (var s = source.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (encoding == null)
                {
                    encoding = System.Text.Encoding.Default;
                }
                using (var sr = new StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private static bool IsRelativeLocalPath(string fileName)
        {
            if (fileName.Length < 2)
            {
                return false;
            }
            var secChar = fileName[1];
            return !(secChar == ':');
        }

        public static string SafeGetFullPath( this string fileOrFolderPath)
        {
            if (!IsRelativeLocalPath(fileOrFolderPath))
            {
                return fileOrFolderPath;
            }
            dynamic appDirPath = GetCurrentApplicationFolderPath();
            dynamic fullPath = Path.Combine(appDirPath, fileOrFolderPath);
            return fullPath;
        }

        public static string GetCurrentApplicationFolderPath()
        {
            dynamic appPath = Assembly.GetEntryAssembly().Location;
            dynamic appDirPath = Path.GetDirectoryName(appPath);
            return appDirPath;
        }

    }

}

