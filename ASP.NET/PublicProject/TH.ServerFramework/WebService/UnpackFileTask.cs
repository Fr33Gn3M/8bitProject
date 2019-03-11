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
		internal class UnpackFileTask : ITaskExecutor, ITaskTransaction
		{
			
			private readonly string _instanceName;
			private readonly string _packFile;
			private readonly string _extraToFolder;
			
			private string _tempDirPath;
			
			public UnpackFileTask(string instanceName, string packFile, string extraToFolder)
			{
				_instanceName = instanceName;
				_packFile = packFile;
				_extraToFolder = extraToFolder;
			}
			
			public void OnExecute()
			{
				var repo = ServiceLocator.Current.GetInstance<IFileSystemProviderRepo>();
				var provider = repo.GetProviderInstance(_instanceName);
				var rootDir = provider.GetDirectory(null);
				var packFile = rootDir.GetFile(_packFile);
				_tempDirPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString().Replace("-", ""));
				using (var pfFs = packFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					using (var zf = ZipFile.Read(pfFs))
					{
						zf.ExtractAll(_tempDirPath, ExtractExistingFileAction.OverwriteSilently);
					}
					
				}
				
				var targetDir = rootDir.GetDirectory(_extraToFolder);
				targetDir.LoadFromLocal(_tempDirPath);
				ClearTempFolder();
			}
			
			private void ClearTempFolder()
			{
				Directory.Delete(_tempDirPath, true);
			}
			
			public void OnRollback()
			{
				ClearTempFolder();
			}
		}
	}
}
