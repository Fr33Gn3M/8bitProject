namespace TH.ServerFramework.IO.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class ArgumentsBuilder
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private string _RootDirectory;

        public IDictionary<string, string> GetArguments()
        { 
            return DictionaryDeserialize(this);
        }

        public static ArgumentsBuilder Parse(IDictionary<string, string> arguments)
        {
          return  DictionarySerializer(arguments);
        }


        /// <summary>
        /// 系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static ArgumentsBuilder DictionarySerializer(IDictionary<string, string> collection)
        {
            var buider = new ArgumentsBuilder();
            if (collection == null)
                return null;
            var builder = new StringBuilder();
            foreach (var item in collection)
            {
                if (!string.IsNullOrEmpty(builder.ToString()))
                    builder.Append(";");
                builder.Append(item.Key + "," + item.Value);
            }
            if (collection.ContainsKey("RootDirectory"))
                buider.RootDirectory = collection["RootDirectory"];
             return buider;
        }
        /// <summary>
        /// 反系列化
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static Dictionary<string, string> DictionaryDeserialize(ArgumentsBuilder str)
        {
            if (str== null ||string.IsNullOrEmpty(str.RootDirectory))
                return null;
            var arr = str.RootDirectory.Split(';');
            var dic = new Dictionary<string, string>();
            foreach (var item in arr)
            {
                var t = item.Split(',');
                if (t.Length == 2)
                    dic.Add(t[0], t[1]);
            }
            return dic;
        }

        public string RootDirectory
        {
            get
            {
                return this._RootDirectory;
            }
            set
            {
                this._RootDirectory = value;
            }
        }
    }
}

