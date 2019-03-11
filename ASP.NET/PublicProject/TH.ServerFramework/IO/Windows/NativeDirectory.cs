namespace TH.ServerFramework.IO.Windows
{
    using Microsoft.VisualBasic;
    using TH.ServerFramework.IO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class NativeDirectory : IDirectory
    {
        private readonly DirectoryInfo _directory;
        private readonly DirectoryInfo _rootDirectory;

        public NativeDirectory(DirectoryInfo directory, DirectoryInfo rootDirectory)
        {
            this._directory = directory;
            this._rootDirectory = rootDirectory;
            if (this._rootDirectory == null)
            {
                this._rootDirectory = this._directory;
            }
        }

        //[CompilerGenerated]
        //private NativeDirectory GetDirectories(DirectoryInfo e)
        //{
        //    return new NativeDirectory(e, this._rootDirectory);
        //}

        //[CompilerGenerated]
        //private NativeDirectory GetDirectories(string  e)
        //{
        //    return new NativeDirectory(e, this._rootDirectory);
        //}

        //[CompilerGenerated]
        //private NativeFile GetDirectory(FileInfo e)
        //{
        //    return new NativeFile(e, this._rootDirectory);
        //}

        //[CompilerGenerated]
        //private NativeFile GetDirectory(FileInfo e)
        //{
        //    return new NativeFile(e, this._rootDirectory);
        //}

        public void CopyTo(IFileSystemItem item)
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            this._directory.Create();
        }

        public void Delete()
        {
            this._directory.Delete(true);
        }

        public IEnumerable<IDirectory> GetDirectories()
        {
            if (_directory.Exists == true)
            {
                var files = _directory.GetDirectories();
                if (files != null)
                {
                    var result = (from e in files select new NativeDirectory(e, _rootDirectory)).ToArray();
                    return result;
                }
            }
            return null;
          //  return this._directory.GetDirectories().Select<DirectoryInfo, NativeDirectory>(new Func<DirectoryInfo, NativeDirectory>(this.GetDirectories)).ToArray<NativeDirectory>();
        }

        public IEnumerable<IDirectory> GetDirectories(string filter, SearchOption scope)
        {
            var result = (from e in _directory.GetDirectories(filter, scope) select new NativeDirectory(e, _rootDirectory)).ToArray();
            return result;
          //  return this._directory.GetDirectories(filter, scope).Select<DirectoryInfo, NativeDirectory>(new Func<DirectoryInfo, NativeDirectory>(this.GetDirectories)).ToArray<NativeDirectory>();
        }

        public IDirectory GetDirectory(string directoryName)
        {
             var   path = this._directory.FullName +"\\"+ directoryName;
            return new NativeDirectory(new DirectoryInfo(path), this._rootDirectory);
        }

        public IFile GetFile(string fileName)
        {
              var  path = this._directory.FullName +"\\"+ fileName;
            return new NativeFile(new FileInfo(path), this._rootDirectory);
        }

        public IEnumerable<IFile> GetFiles()
        {
            if (_directory.Exists == true)
            {
                var files = _directory.GetFiles();
                if (files != null)
                {
                    var result = (from e in files select new NativeFile(e, _rootDirectory)).ToArray();
                    return result;
                }
            }
            return null;
          //  return this._directory.GetFiles().Select<FileInfo, NativeFile>(new Func<FileInfo, NativeFile>(this.GetDirectory)).ToArray<NativeFile>();
        }

        public IEnumerable<IFile> GetFiles(string filter, SearchOption scope)
        {
            var result = (from e in _directory.GetFiles(filter, scope) select new NativeFile(e, _rootDirectory)).ToArray();
            return result;
        }

        public void MoveTo(IFileSystemItem item)
        {
            throw new NotImplementedException();
        }

        public void Rename(string name)
        {
            this._directory.MoveTo(Path.Combine(this._directory.Parent.FullName, name));
        }

        public DateTime CreatedDateUtc
        {
            get
            {
                return this._directory.CreationTimeUtc;
            }
        }

        public bool Exists
        {
            get
            {
                return this._directory.Exists;
            }
        }

        public string FullName
        {
            get
            {
                return _directory.FullName;
            }
        }

        public DateTime LastModifiedTimeUtc
        {
            get
            {
                return this._directory.LastWriteTimeUtc;
            }
        }

        public string Name
        {
            get
            {
                return this._directory.Name;
            }
        }

        public IDictionary Parent
        {
            get
            {
                DirectoryInfo directoryInfo = this._directory.Parent;
                if (directoryInfo == null)
                {
                    return null;
                }
                NativeDirectory directory = new NativeDirectory(directoryInfo, this._rootDirectory);
                return (IDictionary) directory;
            }
        }


        public string FullPath
        {
            get
            {
               // return this._directory.FullName;
                return NativePathHandler.GetRelatedPath(this._rootDirectory, this._directory);
            }
        }
    }
}

