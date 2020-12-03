using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    public class FolderOptions : Options
    {
        public string SourceDir { get; set; } = @"F:\FileWatcher\SDir";
        public string TargetDir { get; set; } = @"F:\FileWatcher\TDir";

        public FolderOptions()
        { }
    }
}
