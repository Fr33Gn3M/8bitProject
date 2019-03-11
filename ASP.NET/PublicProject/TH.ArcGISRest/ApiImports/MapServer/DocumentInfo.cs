using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace TH.ArcGISRest.ApiImports.MapServices
{
	[Serializable()]
	[JsonObject()]
	public class DocumentInfo : Dictionary<string, string>
	{

		public DocumentInfo()
		{
			Title = string.Empty;
			Author = string.Empty;
			Comments = string.Empty;
			Subject = string.Empty;
			Category = string.Empty;
			Keywords = string.Empty;
		}

		protected DocumentInfo(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public static object Parse(IDictionary<string, string> other)
		{
			var  instance = new DocumentInfo();
			foreach (var kv in other) {
				instance.TrySetValue(kv.Key, kv.Value);
			}
			return instance;
		}

		private const string TileProperty = "Tile";
		private const string AuthorProperty = "Author";
		private const string CommentsProperty = "Comments";
		private const string SubjectProperty = "Subject";
		private const string CategoryProperty = "Category";
		private const string KeywordsProperty = "Keywords";
		private string TryGetValue(string key)
		{
			if (ContainsKey(key)) {
				return this[key];
			} else {
				return null;
			}
		}

		private void TrySetValue(string key, string value)
		{
			if (ContainsKey(key)) {
                this[key] = value;
			} else {
				Add(key, value);
			}
		}

		#region "WellKnownProperties"
		[JsonIgnore()]
		public string Title {
			get { return TryGetValue(TileProperty); }
			set { TrySetValue(TileProperty, value); }
		}
		[JsonIgnore()]
		public string Author {
			get { return TryGetValue(AuthorProperty); }
			set { TrySetValue(AuthorProperty, value); }
		}
		[JsonIgnore()]
		public string Comments {
			get { return TryGetValue(CommentsProperty); }
			set { TrySetValue(CommentsProperty, value); }
		}
		[JsonIgnore()]
		public string Subject {
			get { return TryGetValue(SubjectProperty); }
			set { TrySetValue(SubjectProperty, value); }
		}
		[JsonIgnore()]
		public string Category {
			get { return TryGetValue(CategoryProperty); }
			set { TrySetValue(CategoryProperty, value); }
		}
		[JsonIgnore()]
		public string Keywords {
			get { return TryGetValue(KeywordsProperty); }
			set { TrySetValue(KeywordsProperty, value); }
		}
		#endregion
	}
}
