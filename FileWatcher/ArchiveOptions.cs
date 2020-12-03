using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace FileWatcher
{
    public class ArchiveOptions : Options
    {
        public string ArchiveDir { get; set; } = @"F:\FileWatcher2\Tdir\Archive";
        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Optimal;

        public ArchiveOptions()
        { }
    }
}
