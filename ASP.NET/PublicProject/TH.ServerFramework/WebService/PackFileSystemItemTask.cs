// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using Ionic.Zip;
using Microsoft.Practices.ServiceLocation;
using System.IO;
using TH.ServerFramework.IO;
using TH.ServerFramework.Utility;
using TH.ServerFramework.BackgroundTask;


namespace TH.ServerFramework
{
	namespace WebService
	{
		public class PackFileSystemItemTask : ITaskExecutor, ITaskTransaction
		{
			
			private readonly string _instanceName;
			private readonly string _fullPath;
			private readonly string _archiveFilePath;
			
			private string _tempFile;
			private string _tempFolder;
			
			public PackFileSystemItemTask(string instanceName, string fullPath, string archiveFilePath)
			{
				_instanceName = instanceName;
				_fullPath = fullPath;
				_archiveFilePath = archiveFilePath;
			}
			
			public void OnExecute()
			{
				var repo = ServiceLocator.Current.GetInstance<IFileSystemProviderRepo>();
				var fileSystem = repo.GetProviderInstance(_instanceName);
                var rootDir = fileSystem.GetDirectory(null);
				var archiveFile = rootDir.GetFile(_archiveFilePath);
				var file = rootDir.GetFile(_fullPath);
				var folder = rootDir.GetDirectory(_fullPath);
                if (file.Exists)
				{
					using (var fs = archiveFile.Open(FileMode.Create, FileAccess.Write, FileShare.Write))
					{
						ZipFile(file, fs);
					}
					
				}
				else if (folder.Exists)
				{
					using (var fs = archiveFile.Open(FileMode.Create, FileAccess.Write, FileShare.Write))
					{
						ZipFolder(folder, fs);
					}
					
				}
				ClearTempFileItems();
			}
			
			private void ZipFile(IFile file, Stream targetStream)
			{
				_tempFile = Path.GetTempPath();
                file.SaveToLocal(_tempFile);
				using (var zipFile = new ZipFile())
				{
					zipFile.AddFile(_tempFile);
					zipFile.Save(targetStream);
				}
				
			}
			
			private void ZipFolder(IDirectory folder, Stream targetStream)
			{
				_tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString().Replace("-", ""));
				folder.SaveToLocal(_tempFolder);
				using (var zipFile = new ZipFile())
				{
					var items = Directory.GetFileSystemEntries(_tempFolder);
					foreach (var item in items)
					{
						zipFile.AddItem(item.ToString());
					}
					zipFile.Save(targetStream);
				}
				
			}
			
			private void ClearTempFileItems()
			{
				if (_tempFile != null)
				{
					File.Delete(_tempFile);
					return ;
				}
				if (_tempFolder != null)
				{
					Directory.Delete(_tempFolder, true);
					return ;
				}
			}
			
			public void OnRollback()
			{
				ClearTempFileItems();
			}
		}
	}
}
