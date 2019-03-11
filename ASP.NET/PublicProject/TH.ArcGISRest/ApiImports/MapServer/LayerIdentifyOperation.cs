using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace TH.ArcGISRest
{
    [Serializable()]
    public class LayerIdentifyOperation
    {
        public LayersOperationOption OperationOption { get; set; }
        public int[] LayerIds { get; set; }
    }
}
