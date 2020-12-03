using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileWatcher
{
    public class CrypterOptions : Options
    {
        public bool NeedToEncrypt { get; set; } = true;
        public CrypterOptions()
        {
        }

    }
}
