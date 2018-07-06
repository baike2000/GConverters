using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConvertInterfaces.Enum;

namespace ConvertInterfaces
{
    public interface IMyImageConverterWithParams<T> where T : IMyImage
    {
        int ConverterType { get; } 
        string Name { get; }
        int NumberOfParams { get; }
        Type TypeOfParams { get; }
        T Convert(T source, params object[] prms);
    }
}
