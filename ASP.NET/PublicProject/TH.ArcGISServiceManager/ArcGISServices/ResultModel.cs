using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TH.ArcGISServiceManager
{
    [DataContract]
    public class ResultModel
    {
        public ResultModel()
        {
            IsSuccessed = true;
        }

        //public ResultModel(string errorInfo)
        //{
        //    IsSuccessed = false;
        //    ErrorInfo = errorInfo;
        //}

        public ResultModel(string errorInfo,bool isSuccessed)
        {
            IsSuccessed = isSuccessed;
            ErrorInfo = errorInfo;
        }
        [DataMember]
        public bool IsSuccessed { get; set; }
        [DataMember]
        public string ErrorInfo { get; set; }
        [DataMember]
        public string ServiceUrl { get; set; }
        [DataMember]
        public string FeatureUrl { get; set; }
        public string MxdPath { get; set; }
    }
}
