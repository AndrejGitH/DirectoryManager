using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryManager
{
    class PrintDirectories
    {

        //Vypisanie struktury do konzoly za pomoci DFS algoritmu
        public void PrintDirectory(DirectoryData directoryData)
        {
            if (directoryData == null)
            {

                return;
            }

            Stack<(DirectoryData, int)> stack = new Stack<(DirectoryData, int)>();
            stack.Push((directoryData, 0));

            while (stack.Count > 0)
            {
                (DirectoryData current, int depth) = stack.Pop();
                string indent = new string(' ', depth * 2);
                Console.WriteLine($"{indent}{current.DirectoryName}");

                foreach (var file in current.Files)
                {
                    Console.WriteLine($"{indent}  {file.Name}{file.Extension}");
                }

                Console.WriteLine();
                for (int i = current.Directories.Count - 1; i >= 0; i--)
                {

                    stack.Push((current.Directories[i], depth + 1));
                }
            }
        }

        //osobitna metoda, ktora zobrazi prilohy suborov, aby sme dodrzali citatelny kod a nemiesali funkcionality
        public void PrintUniqueExtensions(DirectoryData directories)
        {
            if (directories == null || directories.FileExtensions.Count == 0 || !directories.FileExtensions.Any())
            {
                Console.WriteLine("No extension found in the provided path");
                return;
            }

            Console.WriteLine("Extensions found in the folder:");
            Console.WriteLine(string.Join(", ", directories.FileExtensions));


        }
    }
}
