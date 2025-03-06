using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryManager
{
    //Generic interface na dodrzanie ISP principu a moznosti mat viacero serializatorov 
    interface ISerializator<T>
    {
        string Serialize(T data);
        T Deserialize(string data);
    }
}
