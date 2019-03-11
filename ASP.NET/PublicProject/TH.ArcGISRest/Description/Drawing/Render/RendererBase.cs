using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Drawing.Render
{
	[KnownType(typeof(ClassBreaksRenderer))]
	[KnownType(typeof(UniqueValueRenderer))]
	[KnownType(typeof(SimpleRenderer))]
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public abstract class RendererBase
	{

		protected RendererBase()
		{
		}
	}
}
