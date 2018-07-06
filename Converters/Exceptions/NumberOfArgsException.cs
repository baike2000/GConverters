using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Converters.Exceptions
{
    [Serializable]
    public class NumberOfArgsException : System.Exception, ISerializable
    {
        public NumberOfArgsException(int numberofargs)
        {
            NumberOfArgs = numberofargs;
        }

        public NumberOfArgsException(int numberofargs, string message):base(message)
        {
            NumberOfArgs = numberofargs;
        }

        public NumberOfArgsException(int numberofargs, string message, System.Exception innerexception) : base(message, innerexception)
        {
            NumberOfArgs = numberofargs;
        }
        protected NumberOfArgsException(SerializationInfo info, StreamingContext context) 
        { 
            NumberOfArgs = (int) info.GetValue("NumberOfArgs", typeof(int));
        }
        public int NumberOfArgs { get; private set; }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("NumberOfArgs", NumberOfArgs, typeof(int));
        }

}
}
