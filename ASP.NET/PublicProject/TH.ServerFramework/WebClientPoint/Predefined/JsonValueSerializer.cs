namespace TH.ServerFramework.WebClientPoint.Predefined
{
    using Newtonsoft.Json;
    using System;
    using System.Runtime.CompilerServices;

    public class JsonValueSerializer : PrimvateValueSerializer
    {
        public override string Serializer(object obj)
        {
            string result = base.Serializer(RuntimeHelpers.GetObjectValue(obj));
            if (!this.LastSerialized)
            {
                result = JsonConvert.SerializeObject(RuntimeHelpers.GetObjectValue(obj));
                this.SetLastSerialized(true);
            }
            return result;
        }
    }
}

