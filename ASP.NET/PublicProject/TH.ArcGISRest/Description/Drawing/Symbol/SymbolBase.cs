using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.Description.Drawing.Symbol
{
	[KnownType(typeof(ColorSymbol))]
	[KnownType(typeof(PictureFillSymbol))]
	[KnownType(typeof(PictureMarkerSymbol))]
	[KnownType(typeof(SimpleLineSymbol))]
	[KnownType(typeof(SimpleFillSymbol))]
	[KnownType(typeof(SimpleMarkerSymbol))]
	[KnownType(typeof(TextSymbol))]
	[DataContract(Namespace = Namespaces.THArcGISRest)]
	public abstract class SymbolBase
	{

	}
}
