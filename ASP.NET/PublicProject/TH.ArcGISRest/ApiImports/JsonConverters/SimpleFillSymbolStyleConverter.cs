using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using TH.ArcGISRest.ApiImports.Symbols;
using System.Linq;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class SimpleFillSymbolStyleConverter : JsonConverter
	{

		public override bool CanConvert(System.Type objectType)
		{
			if (typeof(SimpleFillSymbolStyle).Equals(objectType)) {
				return true;
			} else {
				return false;
			}
		}


		public override object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
		{
			string content = serializer.Deserialize(reader).ToString();
			var  arr = content.ToCharArray();
			content = content.Trim();
			var  wff = SimpleLineSymbolStyle.esriSLSSolid.ToString().ToCharArray();
			var  sw = Enum.GetNames(typeof(SimpleFillSymbolStyle));
			//var  m = (string a, string b) => { return a == b; };
            var i = sw.Contains(content, new Comparer());
			var  value = Enum.Parse(typeof(SimpleFillSymbolStyle), content);
			return value;
		}

		public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
		{
            SimpleFillSymbolStyle enumValue = (SimpleFillSymbolStyle)value;
			serializer.Serialize(writer, enumValue.ToString());
		}

		private class Comparer : IEqualityComparer<string>
		{

			public bool Equals1(string x, string y)
			{
				var  result = (x == y);
				var  xarr = x.ToCharArray();
				var  yarr = y.ToCharArray();
				var  u = yarr.Intersect(yarr);
				return result;
			}
			bool System.Collections.Generic.IEqualityComparer<string>.Equals(string x, string y)
			{
				return Equals1(x, y);
			}

			public int GetHashCode1(string obj)
			{
				return obj.GetHashCode();
			}
			int System.Collections.Generic.IEqualityComparer<string>.GetHashCode(string obj)
			{
				return GetHashCode1(obj);
			}
		}
	}

}
