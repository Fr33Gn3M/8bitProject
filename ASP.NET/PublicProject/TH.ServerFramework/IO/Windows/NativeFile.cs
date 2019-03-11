namespace TH.ServerFramework.IO.Windows
{
    using TH.ServerFramework.IO;
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class NativeFile : IFile
    {
        private readonly FileInfo _file;
        private readonly DirectoryInfo _rootDirectory;

        public NativeFile(FileInfo file, DirectoryInfo rootDirectory)
        {
            this._file = file;
            this._rootDirectory = rootDirectory;
        }

        public void CopyTo(IFileSystemItem item)
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            var dir = _file.DirectoryName;
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            using (this._file.Create())
            {
            }
        }

        public void Create(long size)
        {
            using (FileStream fs = this._file.Create())
            {
                fs.SetLength(size);
            }
        }

        public void Delete()
        {
            this._file.Delete();
        }

        public void MoveTo(IFileSystemItem item)
        {
            throw new NotImplementedException();
        }

        public Stream Open(FileMode fileMode, FileAccess fileAccess, FileShare fileShare, long bufferSize = 0x100000L)
        {
            var stream = this._file.Open(fileMode, fileAccess, fileShare);
            return new BufferedStream(stream);
        }

        public void Rename(string name)
        {
            this._file.MoveTo(Path.Combine(this._file.DirectoryName, name));
        }

        public DateTime CreatedDateUtc
        {
            get
            {
                return this._file.CreationTimeUtc;
            }
        }

        public bool Exists
        {
            get
            {
                return this._file.Exists;
            }
        }

        public string Extension
        {
            get
            {
                return this._file.Extension;
            }
        }

        public string FullName
        {
            get
            {
                return _file.FullName;
                //return NativePathHandler.GetRelatedPath(this._rootDirectory, this._file);
            }
        }

        public DateTime LastModifiedTimeUtc
        {
            get
            {
                return this._file.LastWriteTimeUtc;
            }
        }

        public string Name
        {
            get
            {
                return this._file.Name;
            }
        }

        public string NameWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this._file.Name);
            }
        }

        public IDictionary Parent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public long Size
        {
            get
            {
                return this._file.Length;
            }
        }


        public string FullPath
        {
            get { return NativePathHandler.GetRelatedPath(this._rootDirectory, this._file); }
        }


        public DateTime UpdateDate
        {
            get { return this._file.LastWriteTime; }
        }
    }
}

