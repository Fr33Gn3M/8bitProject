// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports


namespace TH.ServerFramework.DictionarySerializer
{

    public class EnumValueSerializer<TEnum> : IValueSerializer where TEnum : struct
    {
        public object Deserialize(string serializedValue)
        {
            return Enum.Parse(typeof(TEnum), serializedValue);
        }

        public string Serialize(object value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }
    }
}
