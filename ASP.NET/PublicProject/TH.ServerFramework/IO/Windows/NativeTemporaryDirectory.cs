namespace TH.ServerFramework.IO.Windows
{
    using TH.ServerFramework.IO;
    using System;
    using System.IO;

    internal class NativeTemporaryDirectory : NativeDirectory, ITemporaryDirectory
    {
        private bool disposedValue;

        public NativeTemporaryDirectory() : base(CreateTempDirectory(), null)
        {
        }

        private static DirectoryInfo CreateTempDirectory()
        {
            string tempPath = Path.GetTempPath();
            string tempFolderName = Guid.NewGuid().ToString().Replace("-", "");
            DirectoryInfo di = new DirectoryInfo(Path.Combine(tempPath, tempFolderName));
            di.Create();
            return di;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
                try
                {
                    this.Delete();
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                }
            }
            this.disposedValue = true;
        }


        public string FullPath
        {
            get { throw new NotImplementedException(); }
        }
    }
}

