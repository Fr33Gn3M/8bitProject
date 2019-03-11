namespace TH.ServerFramework.WebEndpoint.RESTServer.WebHandler.Deserializers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SimpleValueArraryDeserializer<TArray, TElem> : IQueryStringDeserializer<TArray>
    {
        private readonly Lazy<TElem[]> _empty;
        private readonly string _spliter;

        public SimpleValueArraryDeserializer(string spliter = ",")
        {
            var list = new List<TElem>().ToArray();
            _empty = new Lazy<TElem[]>(() => { return list; }, true);
            this._spliter = spliter;
        }


        public TArray Deserialize(string raw)
        {
            object obj = null;
            if (string.IsNullOrEmpty(raw))
            {
                obj = this._empty.Value;
                return (TArray)obj;
            }
            string[] parts = raw.Split(this._spliter);
            SimpleValueDeserializer<TElem> simpleValueDeserializer = new SimpleValueDeserializer<TElem>();
            List<TElem> ls = new List<TElem>();
            foreach (string part in parts)
            {
                TElem value = simpleValueDeserializer.Deserialize(part);
                if (value != null)
                {
                    ls.Add(value);
                }
            }
            obj = ls.ToArray();
            return (TArray)obj;
        }

      
    }
}

