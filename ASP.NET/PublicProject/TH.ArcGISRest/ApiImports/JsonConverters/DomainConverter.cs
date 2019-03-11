using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using TH.ArcGISRest.ApiImports.Domains;
using Newtonsoft.Json.Linq;

namespace TH.ArcGISRest.ApiImports.JsonConverters
{
	[Serializable()]
	public class DomainConverter : JsonConverter
	{

		public override bool CanConvert(Type objectType)
		{
			if (objectType.IsAssignableFrom(typeof(DomainBase))) {
				return true;
			} else {
				return false;
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) {
				return null;
			}
			var  domainJson = serializer.Deserialize<JObject>(reader);
			var  domainJsonStr = domainJson.ToString();
			using (var sr = new StringReader(domainJsonStr)) {
                DomainBase domain = (DomainBase)serializer.Deserialize(sr, typeof(DomainBase));
				using (var sr2 = new StringReader(domainJsonStr)) {
					switch (domain.Type) {
						case DomainType.CodedValue:
                            domain = (CodedValueDomain)serializer.Deserialize(sr2, typeof(CodedValueDomain));
							break;
						//Case DomainType.Inherited
						//    domain = serializer.Deserialize(sr2, GetType(InheritedDomain))
						case DomainType.Range:
                            domain = (RangeDomain)serializer.Deserialize(sr2, typeof(RangeDomain));
							break;
						default:
							throw new NotSupportedException();
					}
				}
				return domain;
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value);
		}
	}
}
