using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestOne.Commons
{
	public class Utility
	{
		public class ExJsonResult : JsonResult
		{
			public ExJsonResult(object value) : base(value)
			{
				JsonSerializerSettings setting = new JsonSerializerSettings();
				// 设置日期序列化的格式  
				setting.DateFormatHandling = DateFormatHandling.IsoDateFormat;
				this.SerializerSettings = setting;
			}

			public ExJsonResult(object value, object serializerSettings) : base(value, serializerSettings)
			{

			}
		}

		public static JsonResult GetComResult(object value)
		{
			return new ExJsonResult(value);
		}
	}
}
