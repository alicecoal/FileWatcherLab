using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileWatcher
{
    public class LogOptions : Options
    {
        public bool NeedToLog { get; set; } = true;
        public string LogFile { get; set; } = @"F:\FileWatcher\logs.txt";

        public LogOptions()
        { }
    }
}
