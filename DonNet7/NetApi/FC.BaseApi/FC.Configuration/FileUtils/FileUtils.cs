using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Utils.FileUtils
{
    public class FileUtils
    {
        public static void CopyFile(string source, string destination)
        {
            FileInfo fi1 = new(source);
            FileInfo fi2 = new(destination);
            var dir = fi2.DirectoryName;
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (File.Exists(destination))
            {
                fi2.Delete();
            }
            fi1.CopyTo(destination);
        }
    }
}
