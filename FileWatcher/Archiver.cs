using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace FileWatcher
{
    class Archiver
    {
        public static void Compress(string path, string cpath, ArchiveOptions archiveOptions)
        {
            try
            {
                using (FileStream sourceStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (FileStream targetStream = File.Create(cpath))
                    {
                        using (GZipStream compressionStream = new GZipStream(targetStream, archiveOptions.CompressionLevel))
                        {
                            sourceStream.CopyTo(compressionStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static void Decompress(string cpath, string path)
        {
            try
            {
                using (FileStream sourceStream = new FileStream(cpath, FileMode.OpenOrCreate))
                {
                    using (FileStream targetStream = File.Create(path))
                    {
                        using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(targetStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
