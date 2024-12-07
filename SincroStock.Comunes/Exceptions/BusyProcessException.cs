using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincroStock.Comunes.Exceptions
{
    [Serializable]
    public class BusyProcessException : ApplicationException
    {
        public static int ExceptionCode { get { return -4; } }

        public BusyProcessException() { }
        public BusyProcessException(string message) : base(message) { }
        public BusyProcessException(string message, System.Exception inner) : base(message, inner) { }
        protected BusyProcessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
