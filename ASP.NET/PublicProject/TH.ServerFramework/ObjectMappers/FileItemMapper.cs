// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using AutoMapper;
using TH.ServerFramework.IO;


namespace TH.ServerFramework
{
    namespace ObjectMappers
    {
        internal class FileItemMapper : ITypeConverter<IFile, FileItem>
        {

            public FileItem Convert(ResolutionContext context)
            {
                if (context.IsSourceValueNull)
                {
                    return null;
                }
                IFile sourceValue = context.SourceValue as IFile;
                var fileItem = new FileItem();
                fileItem.Name = sourceValue.Name;
                fileItem.Size = sourceValue.Size;
                fileItem.CreatedDate = System.Convert.ToDateTime(sourceValue.CreatedDateUtc);
                fileItem.ModifiedDate = System.Convert.ToDateTime(sourceValue.UpdateDate);
                fileItem.FullPath = sourceValue.FullPath;
                return fileItem;
            }
        }
    }

}
