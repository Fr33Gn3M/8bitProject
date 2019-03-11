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
    public interface IValueSerializer
    {
        string Serialize(object value);
        object Deserialize(string serializedValue);
    }
}
