using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryManager
{
    //Trieda, ktora pouziva algoritmus BFS na ziskanie a ulozenie dat adresarovej struktury
    class DirectoryData
    {
        public string DirectoryName { get; set; }
        public string FullPath { get; set; }
        public List<FileModel> Files { get; set; } = new List<FileModel>();
        public List<DirectoryData> Directories { get; set; } = new List<DirectoryData>();
        public HashSet<string> FileExtensions { get; set; } = new HashSet<string>();

        //Konstruktor, ktory zabezpecuje ulozenie podzloziek bez pouzitia rekurzie, ktora pri vacsich adresaroch moze sposobit stackoverflow
        private DirectoryData(string providedPath, bool skipProcessing)
        {
            FullPath = providedPath;
            DirectoryName = Path.GetFileName(providedPath) ?? providedPath;
        }
        //Bez parametrovy kostruktor je potrebny na uspesnu deserializaciu
        public DirectoryData()
        {
            Files = new List<FileModel>();
            Directories = new List<DirectoryData>();
            FileExtensions = new HashSet<string>();
        }

        public DirectoryData(string providedPath) : this()
        {
            if (!Directory.Exists(providedPath))
            {

                throw new DirectoryNotFoundException($"Directory not found: {providedPath}");
            }

            FullPath = providedPath;
            DirectoryName = Path.GetFileName(providedPath) ?? providedPath;


            ProcessDirectory();
        }

        private void ProcessDirectory()
        {
            Queue<DirectoryData> queue = new Queue<DirectoryData>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                DirectoryData current = queue.Dequeue();

                try
                {
                    string[] files = Directory.GetFiles(current.FullPath);
                    foreach (string file in files)
                    {
                        string name = Path.GetFileNameWithoutExtension(file);
                        string extension = Path.GetExtension(file);

                        current.Files.Add(new FileModel(name, extension));
                        FileExtensions.Add(extension);

                    }

                    string[] subDirs = Directory.GetDirectories(current.FullPath);
                    foreach (string subDir in subDirs)
                    {

                        DirectoryData subDirData = new DirectoryData(subDir, true);
                        current.Directories.Add(subDirData);
                        queue.Enqueue(subDirData);
                    }
                }
                catch (UnauthorizedAccessException accessException)
                {
                    throw new UnauthorizedAccessException($"Can't access at: {current.FullPath}", accessException);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in {current.DirectoryName}. Details: {ex.Message}");
                }


            }
        }



    }

}