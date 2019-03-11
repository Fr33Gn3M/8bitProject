namespace TH.ServerFramework.IO
{
    using System;

    public interface IFileSystemItem<T> : IFileSystemItem where T: IFileSystemItem
    {
        void Create();
    }
}

