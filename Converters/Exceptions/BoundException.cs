using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Converters.Exceptions
{
    [Serializable]
    public class BoundException<T> : Exception, ISerializable
    {
        public T Bound { get; private set; }
        public BoundException(T bound)
        {
            Bound = bound;
        }
        public BoundException(T bound, string message):base(message)
        {
            Bound = bound;
        }

        public BoundException(T bound, string message, Exception innerexception) : base(message, innerexception)
        {
            Bound = bound;
        }

        protected BoundException(SerializationInfo info, StreamingContext context)
        {
            Bound = (T)info.GetValue("Bound", typeof(T));
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Bound", Bound, typeof(T));
        }

    }
}
