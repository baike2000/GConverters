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
        ConverterEnum ConverterType { get; } 
        string Name { get; }
        int NumberOfParams { get; }
        Type TypeOfParams { get; }
        List<string> ParamNames { get; }
        T Convert(T source, params object[] prms);
    }
}
