using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcher
{
    class Watcher
    {
        FileSystemWatcher watcher;
        bool enabled = true;

        string SDir = "";
        string TDir = "";
        string saveArchive = "";

        OptionsManager optionsManager;
        public Watcher()
        {

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            optionsManager = new OptionsManager(appDirectory);
            ETLOptions options = optionsManager.GetOptions<ETLOptions>() as ETLOptions;
            SDir = options.FolderOptions.SourceDir;
            TDir = options.FolderOptions.TargetDir;
            saveArchive = options.ArchiveOptions.ArchiveDir;
            Logger.loggerOptions = options.LogOptions;
            Logger.loaded = true;
            watcher = new FileSystemWatcher(SDir);
            watcher.Created += Created;
        }
        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (watcher.EnableRaisingEvents)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }

        private void Created(object sender, FileSystemEventArgs e)
        {
            int key = 17; 
            string pathToFile = e.FullPath;
            DateTime date = File.GetLastWriteTime(pathToFile);
            string name = Path.GetFileNameWithoutExtension(pathToFile);
            string extansion = Path.GetExtension(pathToFile);

            CrypterOptions encryptionOptions = optionsManager.GetOptions<CrypterOptions>() as CrypterOptions;
            ArchiveOptions archivationOptions = optionsManager.GetOptions<ArchiveOptions>() as ArchiveOptions;

            FileSystemWatcher watcher = new FileSystemWatcher();
            if (extansion != ".gz" && extansion != "")
            {
                
                string encryptedText=""; 
                string decryptedText="";
                string message = "";
                if (encryptionOptions.NeedToEncrypt)
                {
                    using (StreamReader sr = new StreamReader(pathToFile))
                    {
                        message = sr.ReadToEnd();
                        encryptedText = Crypter.Encrypt(message, key);
                        sr.Close();
                    }
                    using (StreamWriter sw = new StreamWriter(pathToFile, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(encryptedText);
                        sw.Close();
                    }
                }

                string pathToArchive = Path.Combine(SDir, name + ".gz");
                Archiver.Compress(pathToFile, pathToArchive, archivationOptions);


                File.Delete(pathToFile);

                if (!Directory.Exists(saveArchive))
                {
                    Directory.CreateDirectory(saveArchive);
                }

                string newPathToArchive = Path.Combine(saveArchive, name + ".gz");
                if (File.Exists(newPathToArchive))
                {
                    File.Delete(newPathToArchive);
                }
                File.Move(pathToArchive, newPathToArchive);


                string newPathToFile = Path.Combine(TDir, date.Year.ToString(), date.Month.ToString(), date.Day.ToString());
                Directory.CreateDirectory(newPathToFile);

                newPathToFile = Path.Combine(newPathToFile, name + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + extansion);
                Archiver.Decompress(newPathToArchive, newPathToFile);

                if (encryptionOptions.NeedToEncrypt)
                {
                    using (StreamReader sr = new StreamReader(newPathToFile))
                    {
                        string message1 = sr.ReadToEnd();
                        decryptedText = Crypter.Decrypt(encryptedText, key);
                        sr.Close();
                    }
                    using (StreamWriter sw = new StreamWriter(newPathToFile, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(decryptedText);
                        sw.Close();
                    }
                }
            }
        }
    }
}
