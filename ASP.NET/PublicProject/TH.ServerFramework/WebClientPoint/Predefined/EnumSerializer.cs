namespace TH.ServerFramework.WebClientPoint.Predefined
{
    using TH.ServerFramework.WebClientPoint;
    using System;
    using System.Runtime.CompilerServices;

    public class EnumSerializer : ISerializer
    {
        private bool _lastSerialized;

        public string Serializer(object obj)
        {
            if (obj != null)
            {
                Type objType = obj.GetType();
                if (Enum.IsDefined(objType, RuntimeHelpers.GetObjectValue(obj)))
                {
                    this._lastSerialized = true;
                    return Enum.GetName(objType, RuntimeHelpers.GetObjectValue(obj));
                }
                this._lastSerialized = false;
            }
            return null;
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

