using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileWatcher
{
    class ETLJSONOptions : ETLOptions
    {
        public ETLJSONOptions(string json) : base()
        {
            ETLOptions options = ParserJson.DeserializeJson<ETLOptions>(json);
            ArchiveOptions = options.ArchiveOptions;
            CrypterOptions = options.CrypterOptions;
            FolderOptions = options.FolderOptions;
            LogOptions = options.LogOptions;
        }
    }
}
