using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Serializers
{
    public interface ISerializer<T>
    {
        string Serialize(T obj);
        T Deserialize(string str);
    }
}
