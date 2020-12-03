using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    public class ETLXMLOptions : ETLOptions
    {
        public ETLXMLOptions(string xml) : base()
        {
            ETLOptions options = ParserXml.DeserializeXml<ETLOptions>(xml);
            ArchiveOptions = options.ArchiveOptions;
            CrypterOptions = options.CrypterOptions;
            FolderOptions = options.FolderOptions;
            LogOptions = options.LogOptions;
        }
    }
}
