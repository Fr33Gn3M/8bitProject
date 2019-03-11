// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Microsoft.Practices.ServiceLocation;
using AutoMapper;
using System.IO;
using TH.ServerFramework.WebEndpoint;
using TH.ServerFramework.IO;
using TH.ServerFramework.Configuration;
using TH.ServerFramework.BackgroundTask;
using TH.ServerFramework.TokenService;
using TH.ServerFramework.IO.Windows;
using System.ServiceModel;
using BitMiracle.LibTiff.Classic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;


namespace TH.ServerFramework
{
    namespace WebService
    {
        [SilverlightFaultErrorHandler]
        public class FileSystemService : IFileSystemService
        {

            private readonly TokenProtectedInvoker _tokenProvider;
            private readonly IFileSystemProviderRepo _fileSystemRepo;

            public FileSystemService()
            {
                _tokenProvider = new TokenProtectedInvoker();
                _fileSystemRepo = ServiceLocator.Current.GetInstance<IFileSystemProviderRepo>();
            }

            public void CreateProviderInstance(Guid token, string providerName, string instanceName, IDictionary<string, string> arguments)
            {
                Action act = () =>
                {
                    _fileSystemRepo.RegisterProviderInstance(instanceName, providerName, arguments);
                };
                _tokenProvider.Invoke(token, act);
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public FileSystemItemBase[] GetFolderSubitems(Guid token, string instanceName, string folderFullPath)
            {
                FileSystemItemBase[] result = null;
                Action act = () =>
                {
                    var providerInstance = _fileSystemRepo.GetProviderInstance(instanceName);
                    var folder = providerInstance.GetDirectory(folderFullPath);
                    var subfiles = folder.GetFiles();
                    var subfolders = folder.GetDirectories();
                    List<FileSystemItemBase> ls = new List<FileSystemItemBase>();
                    if (subfiles != null)
                    {
                        foreach (var subfile in subfiles)
                        {
                            var fileItem = Mapper.Map<FileItem>(subfile);
                            ls.Add(fileItem);
                        }
                        foreach (var subFolder in subfolders)
                        {
                            var folderItem = Mapper.Map<FolderItem>(subFolder);
                            ls.Add(folderItem);
                        }
                    }
                    result = ls.ToArray();
                };
                _tokenProvider.Invoke(token, act);
                return result;
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public string[] GetInstalledProviderNames(Guid token)
            {
                string[] result = null;
                Action act = () =>
                {
                    result = _fileSystemRepo.GetProviderNames();
                };
                _tokenProvider.Invoke(token, act);
                return result;
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public string[] GetProviderInstanceNames(Guid token)
            {
                string[] result = null;
                Action act = () =>
                {
                    string providerName = typeof(NativeFileSystemProviderFactory).FullName; ;// "TH.ServerFramework.IO.Windows.NativeFileSystemProviderFactory";
                    result = _fileSystemRepo.GetProviderInstanceNames(providerName);
                };
                _tokenProvider.Invoke(token, act);
                return result;
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public void RemoveProviderInstance(Guid token, string instanceName)
            {
                Action act = () =>
                {
                    _fileSystemRepo.UnregisterProviderInstance(instanceName);
                };
                _tokenProvider.Invoke(token, act);
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public void CreateFolder(Guid token, string instanceName, string folderPath)
            {
                Action act = () =>
                {
                    var provider = _fileSystemRepo.GetProviderInstance(instanceName);
                    var folder = provider.GetDirectory(folderPath);
                    folder.Create();
                };
                _tokenProvider.Invoke(token, act);
            }

            private ITaskPool GetTaskPool()
            {
                var taskPoolName = SettingsSection.GetSection().ApplicationServices.FileSystemService.TaskPool;
                var taskPoolRepo = ServiceLocator.Current.GetInstance<ITaskPoolRepo>();
                var taskPool = taskPoolRepo.GetTaskPool(taskPoolName);
                return taskPool;
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public Guid ExecutePackFileSystemItemTask(Guid token, string instanceName, string fullPath, string archiveFilePath)
            {
                Guid result = Guid.Empty;
                Action act = () =>
                {
                    var taskPool = GetTaskPool();
                    PackFileSystemItemTask task = new PackFileSystemItemTask(instanceName, fullPath, archiveFilePath);
                    result = taskPool.RunTaskBackground(task, System.Threading.Tasks.TaskCreationOptions.None, TimeSpan.Zero);
                };
                _tokenProvider.Invoke(token, act);
                return result;
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public Guid ExecuteUnpackFileTask(Guid token, string instanceName, string packFile, string extraToFolder)
            {
                Guid result = Guid.Empty;
                Action act = () =>
                {
                    var taskPool = GetTaskPool();
                    UnpackFileTask task = new UnpackFileTask(instanceName, packFile, extraToFolder);
                    result = taskPool.RunTaskBackground(task, System.Threading.Tasks.TaskCreationOptions.None, TimeSpan.Zero);
                };
                _tokenProvider.Invoke(token, act);
                return result;
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public byte[] GetFileContent(Guid token, string instanceName, string filePath)
            {
                byte[] result = null;
                Action act = () =>
                {
                    var provider = _fileSystemRepo.GetProviderInstance(instanceName);
                    var rootDir = provider.GetDirectory(null);
                    var file = rootDir.GetFile(filePath);
                    if (!file.Exists)
                    {
                        result = null;
                    }
                    else
                        result = System.IO.File.ReadAllBytes(file.FullName);
                    //using (var  fs = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                    //{
                    //    fs.Seek(offset, SeekOrigin.Begin);
                    //    byte[] buffer = new byte[fetchSize - 1 + 1];
                    //    var readBytes = fs.Read(buffer, 0, (int)fetchSize);
                    //    if (readBytes < fetchSize)
                    //    {
                    //        buffer = new byte[System.Convert.ToInt32(readBytes) - 1 + 1];
                    //    }
                    //    result = buffer;
                    //}

                };
                _tokenProvider.Invoke(token, act);
                return result;
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public void RemoveFileSystemItem(Guid token, string instanceName, string fullPath)
            {
                Action act = () =>
                {
                    var provider = _fileSystemRepo.GetProviderInstance(instanceName);
                    var rootDir = provider.GetDirectory(null);
                    var file = rootDir.GetFile(fullPath);
                    if (file.Exists)
                    {
                        file.Delete();
                        return;
                    }
                    var dir = rootDir.GetDirectory(fullPath);
                    if (dir.Exists)
                    {
                        dir.Delete();
                        return;
                    }
                };
                _tokenProvider.Invoke(token, act);
            }

            //delegate void delegate_act(object )); // VBConversions Note: Generated to prevent error when assigning lambda expression to implicitly typed local variable.

            public void WriteFileContent(Guid token, string instanceName, string filePath, byte[] content)
            {
                Action act = () =>
                {
                    var provider = _fileSystemRepo.GetProviderInstance(instanceName);
                    var rootDir = provider.GetDirectory(null);
                    var file = rootDir.GetFile(filePath);
                    if (file.Exists)
                        file.Delete();
                    file.Create();
                    System.IO.File.WriteAllBytes(file.FullName, content);
                };
                _tokenProvider.Invoke(token, act);
            }


            public void UploadFile(RemoteFileRequest request)
            {
                throw new NotImplementedException();
            }

            public RemoteFileRequest DownloadFile(DownloadFileRequest request)
            {
                throw new NotImplementedException();
            }

            public RemoteFileRequest DownloadFile(string userLoginName, DownloadFileRequest request)
            {
                var provider = _fileSystemRepo.GetProviderInstance(request.InstanceName);
                var rootDir = provider.GetDirectory(request.FullPath);

                //string projectPath = Path.Combine(workPath, request.XM.NO);
                //string XZQPath = Path.Combine(projectPath, request.XZQ.XZQM + request.XZQ.XZQHDM, request.XZQVersionNo.ToString());
                //if (!Directory.Exists(XZQPath))
                //    throw new FaultException("请求的行政区不存在");

                //string fileName = GetFileName(request.FileType);
                //string filePath = Path.Combine(XZQPath, fileName);
                //if (!File.Exists(filePath))
                //    throw new FaultException("请求的文件" + fileName + "不存在");

                RemoteFileRequest result = new RemoteFileRequest
                {

                };
                //Stream ms = new MemoryStream();
                //try
                //{
                //    //FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                //    //fs.CopyTo(ms);
                //    //ms.Position = 0;  //重要，不为0的话，客户端读取有问题
                //    FileInfo fileInfo = new FileInfo(filePath);
                //    CusStreamReader fs = new CusStreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read), 0, fileInfo.Length);
                //    result.FileData = fs;
                //    //fs.Flush();
                //    //fs.Close();
                //}
                //catch (Exception e)
                //{
                //}
                return result;
            }


            public void UploadFile(string userLoginName, RemoteFileRequest request)
            {
                if (request == null)
                    throw new FaultException();
                var provider = _fileSystemRepo.GetProviderInstance(request.InstanceName);
                var rootDir = provider.GetDirectory(request.FullPath);
                var filePath = rootDir.FullName;
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                Stream sourceStream = request.FileData;
                FileStream targetStream = null;

                if (!sourceStream.CanRead)
                    throw new FaultException("数据流不可读!");

                //string filePath = Path.Combine(tempFilePath, GetFileName(request.FileType));
                using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    //read from the input stream in 4K chunks
                    //and save to output stream
                    const int bufferLen = 4096;
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                    {
                        targetStream.Write(buffer, 0, count);
                    }
                    targetStream.Close();
                    sourceStream.Close();
                }
            }


            public bool IsExistFolder(Guid token, string instanceName, string folderPath)
            {
                bool isExist = false;
                Action act = () =>
                {
                    var provider = _fileSystemRepo.GetProviderInstance(instanceName);
                    var folder = provider.GetDirectory(folderPath);
                    isExist = folder.Exists;
                };
                _tokenProvider.Invoke(token, act);
                return isExist;
            }

            public string GetDefaultInstanceName(Guid token)
            {
                return _fileSystemRepo.GetDefaultInstanceName();
            }

            public void SetDefaultInstanceName(Guid token, string instanceName)
            {
                Action act = () =>
                {
                    _fileSystemRepo.SetDefaultInstanceName(instanceName);
                };
                _tokenProvider.Invoke(token, act);
            }

            public void EditFolderName(Guid token, string instanceName, string oldFolderPath, string newFolderPath)
            {
                Action act = () =>
                {
                    var provider = _fileSystemRepo.GetProviderInstance(instanceName);
                    var folder = provider.GetDirectory(oldFolderPath);
                    var newFolder = provider.GetDirectory(newFolderPath);
                    if (folder.Exists)
                        folder.Rename(newFolder.Name);
                };
                _tokenProvider.Invoke(token, act);
            }

            public FileSystemItemBase GetFileInfo(Guid token, string instanceName, string fullPath)
            {
                FileSystemItemBase result = null;
                Action act = () =>
                {
                    var providerInstance = _fileSystemRepo.GetProviderInstance(instanceName);
                    var folder = providerInstance.GetDirectory(null);
                    var file = folder.GetFile(fullPath);
                    if (file != null && File.Exists(file.FullName))
                    {
                        var fileItem = Mapper.Map<FileItem>(file);
                        if (fileItem != null)
                            result = fileItem;
                    }
                };
                _tokenProvider.Invoke(token, act);
                return result;
            }

            public bool IsExistFile(Guid token, string instanceName, string filePath)
            {
                bool isExist = false;
                Action act = () =>
                {
                    var provider = _fileSystemRepo.GetProviderInstance(instanceName);
                    var folder = provider.GetDirectory(null);
                    var file = folder.GetFile(filePath);
                    isExist = file.Exists;
                };
                _tokenProvider.Invoke(token, act);
                return isExist;
            }

            public void EditFileName(Guid token, string instanceName, string oldFilePath, string newFilePath)
            {
                Action act = () =>
                {
                    var provider = _fileSystemRepo.GetProviderInstance(instanceName);
                    var folder = provider.GetDirectory(null);
                    var file = folder.GetFile(oldFilePath);
                    var newfile = folder.GetFile(newFilePath);
                    if (file.Exists)
                        file.Rename(newfile.Name);
                };
                _tokenProvider.Invoke(token, act);
            }

            private static OpenTiff openTiff = new OpenTiff();
            public byte[] GetImageContent(Guid token, string instanceName, string filePath)
            {
                byte[] result = null;
                Action act = () =>
                {
                    var provider = _fileSystemRepo.GetProviderInstance(instanceName);
                    var rootDir = provider.GetDirectory(null);
                    var file = rootDir.GetFile(filePath);
                    if (!file.Exists)
                    {
                        result = null;
                    }
                    else
                    {
                        if (file.Extension.ToLower().Equals(".tif"))
                        {
                            result = TransformTiff(file.FullName);
                            if (result == null)
                                result = System.IO.File.ReadAllBytes(file.FullName);

                        }
                        else
                        {
                            result = System.IO.File.ReadAllBytes(file.FullName);
                        }
                    }
                };
                _tokenProvider.Invoke(token, act);
                return result;
            }

            private byte[] TransformTiff(string imagepath)
            {
                byte[] bytes = null;
                using (Tiff tif = Tiff.Open(imagepath, "r"))
                {
                    if (tif == null)
                        return null;
                    // Find the width and height of the image
                    FieldValue[] value = tif.GetField(TiffTag.IMAGEWIDTH);
                    int width = value[0].ToInt();

                    value = tif.GetField(TiffTag.IMAGELENGTH);
                    int height = value[0].ToInt();

                    // Read the image into the memory buffer 
                    int[] raster = new int[height * width];
                    if (!tif.ReadRGBAImage(width, height, raster))
                        return null;

                    using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppRgb))
                    {
                        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

                        BitmapData bmpdata = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
                        byte[] bits = new byte[bmpdata.Stride * bmpdata.Height];

                        for (int y = 0; y < bmp.Height; y++)
                        {
                            int rasterOffset = y * bmp.Width;
                            int bitsOffset = (bmp.Height - y - 1) * bmpdata.Stride;

                            for (int x = 0; x < bmp.Width; x++)
                            {
                                int rgba = raster[rasterOffset++];
                                bits[bitsOffset++] = (byte)((rgba >> 16) & 0xff);
                                bits[bitsOffset++] = (byte)((rgba >> 8) & 0xff);
                                bits[bitsOffset++] = (byte)(rgba & 0xff);
                                bits[bitsOffset++] = (byte)((rgba >> 24) & 0xff);

                            }
                        }
                        System.Runtime.InteropServices.Marshal.Copy(bits, 0, bmpdata.Scan0, bits.Length);
                        bmp.UnlockBits(bmpdata);
                        using (System.Drawing.Image image = bmp)
                        {
                            bytes = ComprassImage(imagepath, image);
                            image.Dispose();
                        }
                        bmp.Dispose();
                    }
                    tif.Dispose();
                }
                return bytes;
            }

            private static byte[] ComprassImage(string imagepath, Image image)
            {
                byte[] bb = null;
                FileInfo file = new FileInfo(imagepath);
                //int Width = image.Width * 50 / 100;
                //int Height = image.Height * 50 / 100;
                int Width = image.Width;
                int Height = image.Height;
                using (Bitmap bitmap = new Bitmap(Width, Height))
                {
                    using (Graphics graphic = Graphics.FromImage(bitmap))
                    {
                        using (var stream = new MemoryStream())
                        {
                            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphic.DrawImage(image, new Rectangle(0, 0, Width, Height));
                            //Font font = new Font("宋体", 20);
                            //SolidBrush sbrush = new SolidBrush(Color.Red);
                            PropertyItem[] Propertys = image.PropertyItems;
                            PropertyItem[] Propertys2 = bitmap.PropertyItems;
                            Encoding ascii = Encoding.ASCII;
                            //遍历图像文件元数据，检索所有属性
                            foreach (System.Drawing.Imaging.PropertyItem p in Propertys)
                                bitmap.SetPropertyItem(p);
                            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            bb = new byte[stream.Length];
                            graphic.Dispose();
                            stream.Position = 0;
                            stream.Read(bb, 0, (int)stream.Length);
                        }
                    }
                    bitmap.Dispose();
                }
                return bb;
            }
        }
    }
}
