using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TH.ServerFramework.BlobStore
{
    public interface IBlobSection
    {
        string SectionName { get; }
        BlobItem GetBlob(string itemName);
        BlobItemDescription[] FindBlobs(string itemNameRegexPattern, RegexOptions regexOptions);
        BlobItemDescription[] FindBlobs(IDictionary<string, string> properties, bool ignoreCase);
        bool HasBlob(string itemName);
        void SetBlob(BlobItem blobItem);
    }

}
