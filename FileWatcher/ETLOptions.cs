using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    public class ETLOptions : Options
    {
        public ArchiveOptions ArchiveOptions { get; set; } = new ArchiveOptions();
        public CrypterOptions CrypterOptions { get; set; } = new CrypterOptions();
        public LogOptions LogOptions { get; set; } = new LogOptions();
        public FolderOptions FolderOptions { get; set; } = new FolderOptions();
        public ETLOptions() { }

        public ETLOptions(FolderOptions foldersOptions, LogOptions logOptions,
            ArchiveOptions archiveOptions, CrypterOptions crypterOptions)
        {
            ArchiveOptions = archiveOptions;
            CrypterOptions = crypterOptions;
            LogOptions = logOptions;
            FolderOptions = foldersOptions;
        }
    }
}