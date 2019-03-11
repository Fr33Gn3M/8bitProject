using System.Collections.Generic;
using KT.ArcGISRest.ApiImports.Geometry;

namespace EsriWkt
{
	/* ESRI's Geometry class has an Internal constructor   */
	/* so we can't subclass it ourselves                   */
	/* this is a placeholder for some later-day workaround */

	public class GeometryCollection : List<AgsGeometryBase>
	{
		public AgsEnvelope Extent
		{
			get { throw new System.NotImplementedException(); }
		}
	}
}
