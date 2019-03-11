using System.Collections.Generic;
using TH.ArcGISRest.ApiImports.Geometry;

namespace TH.EsriWkt
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
