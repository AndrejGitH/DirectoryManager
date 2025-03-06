using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryManager
{
    class FileModel
    {

        public string Name { get; set; }
        public string Extension { get; set; }

        public FileModel(string name, string extension)
        {
            Name = name;
            Extension = extension;
        }
    }
}
