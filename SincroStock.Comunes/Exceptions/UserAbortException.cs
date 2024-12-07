using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincroStock.Comunes.Exceptions
{
    [Serializable]
    public class UserAbortException : ApplicationException
    {
        public UserAbortException() { }
        public UserAbortException(string message) : base(message) { }
        public UserAbortException(string message, System.Exception inner) : base(message, inner) { }
        protected UserAbortException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
