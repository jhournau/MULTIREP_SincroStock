using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincroStock.Comunes.Exceptions
{
    [Serializable]
    public class SincroException : Exception
    {
        public SincroException() { }
        public SincroException(string message) : base(message) { }
        public SincroException(string message, System.Exception inner) : base(message, inner) { }
        protected SincroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
