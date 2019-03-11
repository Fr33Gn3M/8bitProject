// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.ComponentModel;


namespace TH.ServerFramework
{
	namespace ObjectMappers
	{
        [TypeConverter(typeof(FileItemMapper))]
        [TypeConverter(typeof(FolderItemMapper))]
		internal class MaperProfile : ProfileBase
		{
		}
	}
}
