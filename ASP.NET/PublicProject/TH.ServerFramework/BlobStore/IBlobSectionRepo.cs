using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TH.ServerFramework.BlobStore
{
    public interface IBlobSectionRepo
    {
        string[] GetSectionNames();
        IBlobSection CreateSection(string sectionName);
        bool HasSection(string sectionName);
        IBlobSection GetSection(string sectionName);
        void RemoveSection(string sectionName);
    }

}
