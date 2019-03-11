namespace TH.ServerFramework.WebClientPoint.Predefined
{
    using TH.ServerFramework.WebClientPoint;
    using System;
    using System.Linq;

    public class PrimvateValueSerializer : ISerializer
    {
        private bool _lastSerialized;

        public virtual string Serializer(object obj)
        {
            string strValue = null;
            Type[] primvateTypes = new Type[] { typeof(int), typeof(long), typeof(decimal), typeof(double), typeof(bool) };
            if (primvateTypes.Contains<Type>(obj.GetType()))
            {
                strValue = obj.ToString();
                this._lastSerialized = true;
                return strValue;
            }
            if (obj is string)
            {
                strValue = obj.ToString();
                this._lastSerialized = true;
                return strValue;
            }
            this._lastSerialized = false;
            return strValue;
        }

        protected void SetLastSerialized(bool value)
        {
            this._lastSerialized = value;
        }

        public bool LastSerialized
        {
            get
            {
                return this._lastSerialized;
            }
        }

    }
}

