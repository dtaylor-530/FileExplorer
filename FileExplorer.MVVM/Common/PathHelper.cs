using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer.MVVM
{
    public static class PathHelper
    {
        public static string[] GetFilesInSubDirectories(IList<FileSystemInfo> value)
            => value?
            .SelectMany(_ => System.IO.Directory.GetFiles(_.FullName, "*.*", SearchOption.AllDirectories))
            .ToArray();


        public static string FileMap(string path)
            => DateTime
            .FromFileTime(Convert.ToInt64(System.IO.Path.GetFileName(path).Replace(System.IO.Path.GetExtension(path), "")))
            .ToShortDateString();
        
    }
}
